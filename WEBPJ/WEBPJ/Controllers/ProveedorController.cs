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
    public class ProveedorController : Controller
    {
        #region Variables
        ProveedorProducto_Data data_ProveedorProducto = new ProveedorProducto_Data();
        Proveedor_Data data_Proveedor = new Proveedor_Data();
        Producto_Data data_producto = new Producto_Data();
        ProveedorProducto_List Lista_ProveedorProductoDetalle = new ProveedorProducto_List();
        string mensaje = string.Empty;
        #endregion        

        #region Index
        public ActionResult Index()
        {
            var model = new Proveedor_Info();
            return View(model);
        }

        public ActionResult Index2()
        {
            var model = new Proveedor_Info();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Proveedor()
        {
            List<Proveedor_Info> model = data_Proveedor.get_list();

            return PartialView("_GridViewPartial_Proveedor", model);
        }

        public ActionResult GridViewPartial_ProveedorNexpirion()
        {
            List<Proveedor_Info> model = data_Proveedor.get_list_Nexpirion();

            return PartialView("_GridViewPartial_ProveedorNexpirion", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(string Tipo = "", string Codigo = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Proveedor_Info model = data_Proveedor.get_info_Nexpirion(Tipo, Codigo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lista_ProveedorProductoDetalle.set_list(new List<ProveedorProducto_Info>(), model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(Proveedor_Info model)
        {
            model.ListaProveedorProductoDetalle = Lista_ProveedorProductoDetalle.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            if (!data_Proveedor.GuardarBD(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string Tipo = "", string Codigo = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Proveedor_Info model = data_Proveedor.get_info(Tipo, Codigo);

            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaProveedorProductoDetalle = data_ProveedorProducto.GetList(Convert.ToString(model.Codigo));
            Lista_ProveedorProductoDetalle.set_list(model.ListaProveedorProductoDetalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Proveedor_Info model)
        {
            model.ListaProveedorProductoDetalle = Lista_ProveedorProductoDetalle.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!data_Proveedor.ModificarBD(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(string Tipo ="", string Codigo = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Proveedor_Info model = data_Proveedor.get_info(Tipo, Codigo);
            if (!data_Proveedor.EliminarBD(model))
            {
                cargar_combos();
                ViewBag.mensaje = "No se ha podido eliminar el registro";
            };

            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos
        public void cargar_combos()
        {
            Dictionary<string, string> lst_Concepto = new Dictionary<string, string>();
            lst_Concepto.Add("0009", "INGRESO POR COMPRA");
            lst_Concepto.Add("0012", "IMPORTACION");
            lst_Concepto.Add("0022", "LIQUIDACION DE COMPRAS");
            lst_Concepto.Add("0023", "COMPRAS INTERNAS");
            lst_Concepto.Add("0024", "SUMINISTROS DE FUMIGACION POTREROS");
            ViewBag.lst_Concepto = lst_Concepto;
        }
        public void cargar_combos_detalle()
        {
            var lst_Producto = data_producto.get_list(false);
            ViewBag.lst_Producto = lst_Producto;
        }
        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_ProveedorProductoDetalle()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ProveedorProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProveedorProductoDetalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ProveedorProducto_Info info_det)
        {
            Lista_ProveedorProductoDetalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_ProveedorProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProveedorProductoDetalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ProveedorProducto_Info info_det)
        {
            Lista_ProveedorProductoDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_ProveedorProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProveedorProductoDetalle", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            Lista_ProveedorProductoDetalle.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            Proveedor_Info model = new Proveedor_Info();
            model.ListaProveedorProductoDetalle = Lista_ProveedorProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProveedorProductoDetalle", model.ListaProveedorProductoDetalle);
        }

        private bool Validar(Proveedor_Info i_validar, ref string msg)
        {
            i_validar.ListaProveedorProductoDetalle = Lista_ProveedorProductoDetalle.get_list(i_validar.IdTransaccionSession);

            if (i_validar.ListaProveedorProductoDetalle.Count == 0)
            {
                mensaje = "Debe ingresar al menos un registro en el detalle";
                return false;
            }

            return true;
        }
        #endregion        
    }

    public class ProveedorProducto_List
    {
        string Variable = "ProveedorProducto_Info";
        public List<ProveedorProducto_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ProveedorProducto_Info> list = new List<ProveedorProducto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ProveedorProducto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ProveedorProducto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ProveedorProducto_Info info_det, decimal IdTransaccionSession)
        {
            List<ProveedorProducto_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q => q.IdProducto == info_det.IdProducto).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

                list.Add(info_det);
            }
        }

        public void UpdateRow(ProveedorProducto_Info info_det, decimal IdTransaccionSession)
        {
            ProveedorProducto_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ProveedorProducto_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}