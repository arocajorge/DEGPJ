using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPJ.Data;
using WEBPJ.Helps;
using WEBPJ.Info;

namespace WEBPJ.Controllers
{
    public class CompraController : Controller
    {
        #region Variables
        Compra_Data data_compra = new Compra_Data();
        Producto_Data data_producto = new Producto_Data();
        Usuario_Data data_usuario = new Usuario_Data();
        Proveedor_Data data_proveedor = new Proveedor_Data();
        ProveedorProducto_Data data_proveedor_producto = new ProveedorProducto_Data();
        List<Compra_Info> lst_Compra = new List<Compra_Info>();
        Compra_List Lista_Compra = new Compra_List();
        ProductoDet_List Lista_ProductoDet = new ProductoDet_List();
        ProductoDetalle_Data data_producto_detalle = new ProductoDetalle_Data();
        string mensaje = string.Empty;
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProveedor()
        {
            Compra_Info model = new Compra_Info();
            return PartialView("_CmbProveedor", model);
        }
        public List<Proveedor_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return data_proveedor.get_list_bajo_demanda(args);
        }
        public Proveedor_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return data_proveedor.get_info_bajo_demanda(args, "PRV");
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;

            Filtros_Info model = new Filtros_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdUsuario = "",
                Estado = "",
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now.Date
            };

            lst_Compra = data_compra.get_list(model.fecha_ini, model.fecha_fin, model.IdUsuario, model.Estado);
            Lista_Compra.set_list(lst_Compra, model.IdTransaccionSession);
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Filtros_Info model)
        {
            lst_Compra = data_compra.get_list(model.fecha_ini, model.fecha_fin, model.IdUsuario, model.Estado);
            Lista_Compra.set_list(lst_Compra, model.IdTransaccionSession);
            cargar_combos_consulta();
            return View(model);
        }

        public ActionResult GridViewPartial_Compra(DateTime? Fecha_ini, DateTime? Fecha_fin, string IdUsuario = "", string Estado = "")
        {
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdUsuario = IdUsuario == "" ? "" : Convert.ToString(IdUsuario);
            ViewBag.Estado = Estado == "" ? "" : Convert.ToString(Estado);

            cargar_combos_grid();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            lst_Compra = Lista_Compra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Compra", lst_Compra);
        }

        public void cargar_combos(Compra_Info model)
        {
            var lst_productos = data_proveedor_producto.GetList_Combo(model.ProvCodigo);
            if (lst_productos == null)
            {
                lst_productos = new List<Producto_Info>();
            }
            ViewBag.lst_productos = lst_productos;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Compra_Info model = new Compra_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
            };

            model.lst_ProductoDet = new List<ProductoDetalle_Info>();
            Lista_ProductoDet.set_list(model.lst_ProductoDet, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(Compra_Info model)
        {
            model.lst_ProductoDet = Lista_ProductoDet.get_list(model.IdTransaccionSession);
            var info_proveedor = data_proveedor.get_info("PRV", model.Codigo);
            var info_producto = data_producto.get_info(model.IdProducto);

            model.ProvCedulaRuc = info_proveedor.Ruc;
            model.ProvNombre = info_proveedor.Nombre;
            model.ProvTipo = info_proveedor.Tipo;
            model.IdUsuario = SessionFixed.IdUsuario;
            model.Codigo = info_producto.Codigo;
            model.Comentario = "";
            model.Estado = "PENDIENTE";
            model.Fecha = DateTime.Now;
            model.Calificacion = 0;
            model.Precio = 0;
            model.Total = 0;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model);
                return View(model);
            }

            if (!data_compra.GuardarBD(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
        //public JsonResult GuardarLista(decimal IdTransaccionSession = 0)
        //{
        //    var resultado = false;

        //    if (IdTransaccionSession > 0)
        //    {
        //        SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
        //        lst_Compra = Lista_Compra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
        //        resultado = data_compra.ModificarBD(lst_Compra);

        //    }
        //    return Json(resultado, JsonRequestBehavior.AllowGet);
        //}

        private void cargar_combos_consulta()
        {
            try
            {
                var lst_Usuarios = data_usuario.get_list(false);
                lst_Usuarios.Add(new Usuario_Info
                {
                    IdUsuario = "",
                    Nombre = "TODOS"
                });
                ViewBag.lst_Usuarios = lst_Usuarios;

                Dictionary<string, string> lst_Estado = new Dictionary<string, string>();
                lst_Estado.Add("A", "PROCESADO");
                lst_Estado.Add("P", "PENDIENTE");
                lst_Estado.Add("I", "NO PROCESADO");
                lst_Estado.Add("X", "ANULADOS");
                lst_Estado.Add("", "TODOS");
                ViewBag.lst_Estado = lst_Estado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Combos Grid
        private void cargar_combos_grid()
        {
            var lst_Usuarios = data_usuario.get_list(false);
            ViewBag.lst_Usuarios = lst_Usuarios;

            var lst_Productos = data_producto.get_list(false);
            ViewBag.lst_Productos = lst_Productos;

            Dictionary<string, string> lst_Estado = new Dictionary<string, string>();
            lst_Estado.Add("A", "PROCESADO");
            lst_Estado.Add("P", "PENDIENTE");
            lst_Estado.Add("I", "NO PROCESADO");
            lst_Estado.Add("X", "ANULADOS");
            ViewBag.lst_Estado = lst_Estado;
        }
        #endregion

        #region Metodos del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Compra_Info info_det)
        {
            Lista_Compra.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Compra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if(model!= null)
            {
                if (ModelState.IsValid == true)
                {
                    data_compra.ModificarBD(model.Where(q => q.IdCompra == info_det.IdCompra).FirstOrDefault());
                }
            }
                            
            cargar_combos_grid();
            return PartialView("_GridViewPartial_Compra", model);
        }

        public ActionResult EditingAnular([ModelBinder(typeof(DevExpressEditorsBinder))] Compra_Info info_det)
        {
            Lista_Compra.AnularRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Compra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if (model != null)
                data_compra.AnularBD(model.Where(q => q.IdCompra == info_det.IdCompra).FirstOrDefault());

            cargar_combos_grid();
            return PartialView("_GridViewPartial_Compra", model);
        }

        #endregion

        #region Validar
        private bool validar(Compra_Info i_validar, ref string msg)
        {
            if (i_validar.Cantidad == 0)
            {
                msg = "Debe ingresar cantidad";
                return false;
            }

            if (i_validar.IdProducto == 0)
            {
                msg = "Debe ingresar producto";
                return false;
            }

            if (i_validar.IdProducto == 0)
            {
                msg = "Debe ingresar proveedor";
                return false;
            }

            return true;
        }
        #endregion

        #region Json
        public JsonResult CargarProductoDetalle(decimal IdTransaccionSession = 0, int IdProducto = 0)
        {
            List<ProductoDetalle_Info> lst_producto_detalle = data_producto_detalle.GetList(IdProducto);
            Lista_ProductoDet.set_list(lst_producto_detalle, IdTransaccionSession);

            return Json(lst_producto_detalle, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult CalificarProducto(decimal IdTransaccionSession = 0, int IdProducto = 0, double Cantidad = 0)
        {
            var resultado = "";

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ProductoDetalle
        public ActionResult GridViewPartial_CompraProductoDetalle()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ProductoDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_CompraProductoDetalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_ProductoDetalle([ModelBinder(typeof(DevExpressEditorsBinder))] ProductoDetalle_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_ProductoDet.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var model = Lista_ProductoDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_CompraProductoDetalle", model);
        }
        #endregion
    }

    public class Compra_List
    {
        string Variable = "Compra_List_Info";
        Producto_Data data_producto = new Producto_Data();
        Usuario_Data data_usuario = new Usuario_Data();

        public List<Compra_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<Compra_Info> list = new List<Compra_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<Compra_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<Compra_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(Compra_Info info_det, decimal IdTransaccionSession)
        {
            Compra_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdCompra == info_det.IdCompra).FirstOrDefault();

            var info_producto = data_producto.get_info(edited_info.IdProducto);
            var info_usuario = data_usuario.get_info(edited_info.IdUsuario);

            edited_info.IdUsuario = info_det.IdUsuario;
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.NomUsuario = info_usuario.Nombre;
            edited_info.NomProducto = info_producto.Descripcion;
            edited_info.Cantidad = info_det.Cantidad;
            edited_info.Precio = info_det.Precio;
            edited_info.Calificacion = info_det.Calificacion;
            edited_info.Fecha = info_det.Fecha.Date;
            edited_info.Total = info_det.Precio * info_det.Cantidad;
            edited_info.Comentario = info_det.Comentario;
            edited_info.Estado = info_det.Estado;
        }

        public void AnularRow(Compra_Info info_det, decimal IdTransaccionSession)
        {
            Compra_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdCompra == info_det.IdCompra).FirstOrDefault();
            edited_info.Estado = "X";
        }
    }

    public class ProductoDet_List
    {
        string Variable = "ProductoDetalle_Info_List";
        public List<ProductoDetalle_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ProductoDetalle_Info> list = new List<ProductoDetalle_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ProductoDetalle_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ProductoDetalle_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ProductoDetalle_Info info_det, decimal IdTransaccionSession)
        {
            ProductoDetalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            edited_info.CalificacionCompra = info_det.CalificacionCompra;
        }
    }
}