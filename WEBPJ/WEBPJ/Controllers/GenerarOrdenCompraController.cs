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
    public class GenerarOrdenCompraController : Controller
    {
        #region Variables
        Compra_Data data_compra = new Compra_Data();
        CompraDetalle_Data data_compra_det = new CompraDetalle_Data();
        Usuario_Data data_usuario = new Usuario_Data();
        Producto_Data data_producto = new Producto_Data();
        GenerarOrdenCompra_List ListaOrdenCompra = new GenerarOrdenCompra_List();
        List<Compra_Info> lst_Compra = new List<Compra_Info>();
        string mensaje = string.Empty;
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
                Estado = "P",
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now.Date
            };

            lst_Compra = data_compra.get_list(model.fecha_ini, model.fecha_fin, model.IdUsuario, model.Estado);
            ListaOrdenCompra.set_list(lst_Compra, model.IdTransaccionSession);
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Filtros_Info model)
        {
            model.Estado = "P";
            lst_Compra = data_compra.get_list(model.fecha_ini, model.fecha_fin, model.IdUsuario, model.Estado);
            ListaOrdenCompra.set_list(lst_Compra, model.IdTransaccionSession);
            cargar_combos_consulta();
            return View(model);
        }

        public ActionResult GridViewPartial_OrdenCompra(DateTime? Fecha_ini, DateTime? Fecha_fin, string IdUsuario = "")
        {
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdUsuario = IdUsuario == "" ? "" : Convert.ToString(IdUsuario);
            ViewBag.Estado = "P";

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            lst_Compra = ListaOrdenCompra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_OrdenCompra", lst_Compra);
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
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region json
        public JsonResult GenerarOrdenCompra(string Ids)
        {
            string[] array = Ids.Split(',');
            var output = array.GroupBy(q => q).ToList();
            foreach (var item in output)
            {
                if (item.Key != "")
                {
                    var info = ListaOrdenCompra.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(v => v.IdCompra == Convert.ToDecimal(item.Key)).FirstOrDefault();
                    Compra_Info info_compra = new Compra_Info();
                    //info_compra.lst_CompraDetProducto = new List<CompraDetalle_Info>();

                    info_compra = data_compra.get_info(Convert.ToInt32(item.Key));
                    var info_producto = data_producto.get_info(info_compra.IdProducto);
                    info_compra.Codigo = info_producto.Codigo;
                    info_compra.NomProducto = info_producto.Descripcion;
                    info_compra.lst_CompraDetProducto = data_compra_det.get_list(Convert.ToInt32(item.Key));
                    if (info_compra != null)
                    {
                        info_compra.Estado = "A";
                        if (data_compra.GuardarOrdenCompraBD(info_compra))
                        {
                            data_compra.ActualizarEstadoBD(info_compra);
                        }
                    }
                        
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class GenerarOrdenCompra_List
    {
        string Variable = "GenerarOrdenCompra_List";
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
    }
}