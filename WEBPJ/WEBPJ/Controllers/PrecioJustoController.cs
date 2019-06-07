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
    public class PrecioJustoController : Controller
    {
        #region Variables
        PrecioJusto_Data data_preciojusto = new PrecioJusto_Data();
        Producto_Data data_producto = new Producto_Data();
        Usuario_Data data_usuario = new Usuario_Data();
        List<PrecioJusto_Info> lst_PrecioJusto = new List<PrecioJusto_Info>();
        PrecioJusto_List Lista_PrecioJusto = new PrecioJusto_List();
        #endregion  

        #region Index
        public ActionResult Index()
        {
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;

            Filtros_Info model = new Filtros_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdUsuario = SessionFixed.IdUsuario,
                Estado = ""
            };

            Lista_PrecioJusto.set_list(new List<PrecioJusto_Info>(), model.IdTransaccionSession);        
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Filtros_Info model)
        {
            Lista_PrecioJusto.get_list(model.IdTransaccionSession);
            cargar_combos_consulta();
            return View(model);
        }
        #endregion

        public ActionResult GridViewPartial_PrecioJusto(DateTime? Fecha_ini, DateTime? Fecha_fin, string IdUsuario = "", string Estado="")
        {
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdUsuario = IdUsuario == "" ? "" : Convert.ToString(IdUsuario);
            ViewBag.Estado = Estado == "" ? "" : Convert.ToString(Estado);

            cargar_combos_grid();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<PrecioJusto_Info> lst_PrecioJusto = data_preciojusto.get_list(ViewBag.Fecha_ini, ViewBag.Fecha_fin, IdUsuario, Estado);
            Lista_PrecioJusto.set_list(lst_PrecioJusto, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PrecioJusto", lst_PrecioJusto);
        }

        public JsonResult GuardarLista(decimal IdTransaccionSession = 0)
        {
            var resultado = false;

            if (IdTransaccionSession > 0)
            {
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
                List<PrecioJusto_Info> lst_PrecioJusto = Lista_PrecioJusto.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                resultado = data_preciojusto.ModificarBD(lst_PrecioJusto);
                
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

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
            ViewBag.lst_Estado = lst_Estado;
        }
        #endregion

        #region Metodos del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] PrecioJusto_Info info_det)
        {
            Lista_PrecioJusto.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_PrecioJusto.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_grid();

            return PartialView("_GridViewPartial_PrecioJusto", model);
        }

        public ActionResult EditingAnular([ModelBinder(typeof(DevExpressEditorsBinder))] PrecioJusto_Info info_det)
        {
            Lista_PrecioJusto.AnularRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_PrecioJusto.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_grid();
            return PartialView("_GridViewPartial_PrecioJusto", model);
        }

        #endregion        
    }

    public class PrecioJusto_List
    {
        string Variable = "PrecioJusto_Info";
        Producto_Data data_producto = new Producto_Data();
        Usuario_Data data_usuario = new Usuario_Data();

        public List<PrecioJusto_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<PrecioJusto_Info> list = new List<PrecioJusto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<PrecioJusto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<PrecioJusto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(PrecioJusto_Info info_det, decimal IdTransaccionSession)
        {
            List<PrecioJusto_Info> edited_info1 = get_list(IdTransaccionSession);
            PrecioJusto_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdPrecioJusto == info_det.IdPrecioJusto).First();

            var info_producto = data_producto.get_info(edited_info.IdProducto);
            var info_usuario = data_usuario.get_info(edited_info.IdUsuario);
            edited_info.IdUsuario = info_det.IdUsuario;
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.Nombre = info_usuario.Nombre;
            edited_info.Descripcion = info_producto.Descripcion;
            edited_info.Cantidad = info_det.Cantidad;
            edited_info.Precio = info_det.Precio;
            edited_info.Calificacion = info_det.Calificacion;
            edited_info.Fecha = info_det.Fecha;
            edited_info.Total = info_det.Precio * info_det.Cantidad;
            edited_info.Comentario = info_det.Comentario;
            edited_info.Estado = info_det.Estado;
        }

        public void AnularRow(PrecioJusto_Info info_det, decimal IdTransaccionSession)
        {
            PrecioJusto_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdPrecioJusto == info_det.IdPrecioJusto).First();
            edited_info.Estado = "I";
        }
    }
}