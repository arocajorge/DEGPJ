using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPJ.Data;
using WEBPJ.Helps;
using WEBPJ.Info;
using WEBPJ.Reportes;

namespace WEBPJ.Controllers
{
    public class ReportesController : Controller
    {
        #region Variables
        Proveedor_Data data_proveedor = new Proveedor_Data();
        Producto_Data data_producto = new Producto_Data();
        Usuario_Data data_usuario = new Usuario_Data();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProveedor_Rpt()
        {
            Compra_Info model = new Compra_Info();
            return PartialView("_CmbProveedor_Rpt", model);
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

        #region Metodos
        public void cargar_combos()
        {
            var lst_productos = data_producto.get_list(false);
            if (lst_productos == null)
            {
                lst_productos = new List<Producto_Info>();
            }
            lst_productos.Add(new Producto_Info
            {
                IdProducto = 0,
                Descripcion = "TODOS"
            });
            ViewBag.lst_productos = lst_productos;

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
        #endregion


        public ActionResult Rpt_001()
        {
            Filtros_Info model = new Filtros_Info
            {
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now.Date,
                IdUsuario = "",
                IdProducto = 0,
                ProvCodigo = "",
                Estado =""
            };
            cargar_combos();
            Rpt_001 report = new Rpt_001();
            report.p_ProvCodigo.Value = model.ProvCodigo;
            report.p_IdProducto.Value = model.IdProducto;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_Estado.Value = model.Estado;
            report.usuario = SessionFixed.IdUsuario;
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult Rpt_001(Filtros_Info model)
        {
            Rpt_001 report = new Rpt_001();
            report.p_ProvCodigo.Value = model.ProvCodigo;
            report.p_IdProducto.Value = model.IdProducto;
            report.p_Estado.Value = model.Estado;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.usuario = SessionFixed.IdUsuario;
            cargar_combos();

            ViewBag.Report = report;
            return View(model);
        }
    }
}