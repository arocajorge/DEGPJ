﻿using DevExpress.Web.Mvc;
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
    public class ProductoController : Controller
    {
        #region Variables
        Producto_Data data_producto = new Producto_Data();
        ProductoDetalle_Data data_producto_detalle = new ProductoDetalle_Data();
        ProductoDetalle_List Lista_ProductoDetalle = new ProductoDetalle_List();
        Proveedor_Data data_proveedor = new Proveedor_Data();
        ProveedorProducto_Data data_proveedor_producto = new ProveedorProducto_Data();
        string mensaje = string.Empty;
        #endregion        

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Producto()
        {
            List<Producto_Info> model = data_producto.get_list(true);

            return PartialView("_GridViewPartial_Producto", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Producto_Info model = new Producto_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
            };

            Lista_ProductoDetalle.set_list(new List<ProductoDetalle_Info>(), model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(Producto_Info model)
        {
            model.ListaProductoDetalle = Lista_ProductoDetalle.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            if (!data_producto.GuardarBD(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdProducto = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Producto_Info model = data_producto.get_info(IdProducto);

            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaProductoDetalle = data_producto_detalle.GetList(Convert.ToInt32(model.IdProducto));
            Lista_ProductoDetalle.set_list(model.ListaProductoDetalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Producto_Info model)
        {
            model.ListaProductoDetalle = Lista_ProductoDetalle.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!data_producto.ModificarBD(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdProducto = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Producto_Info model = data_producto.get_info(Convert.ToInt32(IdProducto));
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaProductoDetalle = data_producto_detalle.GetList(Convert.ToInt32(model.IdProducto));
            Lista_ProductoDetalle.set_list(model.ListaProductoDetalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(Producto_Info model)
        {
            if (!data_producto.AnularBD(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.ListaProductoDetalle = data_producto_detalle.GetList(Convert.ToInt32(model.IdProducto));
                Lista_ProductoDetalle.set_list(model.ListaProductoDetalle, model.IdTransaccionSession);
                cargar_combos();
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos
        public void cargar_combos_detalle()
        {
            Dictionary<string, string> lst_ValorOptimo = new Dictionary<string, string>();
            lst_ValorOptimo.Add(@WEBPJ.Info.Enumeradores.eValorOptimo.MINIMO.ToString(), "VALOR MINIMO");
            lst_ValorOptimo.Add(@WEBPJ.Info.Enumeradores.eValorOptimo.MAXIMO.ToString(), "VALOR MAXIMO");            
            ViewBag.lst_ValorOptimo = lst_ValorOptimo;
        }

        public  void cargar_combos()
        {
            Dictionary<string, string> lst_EscogerPrecioPor = new Dictionary<string, string>();
            lst_EscogerPrecioPor.Add(@WEBPJ.Info.Enumeradores.eEscogerPrecioPor.MASALTO.ToString(), "VALOR MAS ALTO");
            lst_EscogerPrecioPor.Add(@WEBPJ.Info.Enumeradores.eEscogerPrecioPor.MASBAJO.ToString(), "VALOR MAS BAJO");
            lst_EscogerPrecioPor.Add(@WEBPJ.Info.Enumeradores.eEscogerPrecioPor.CERCANO.ToString(), "VALOR CERCANO");
            ViewBag.lst_EscogerPrecioPor = lst_EscogerPrecioPor;
        }
        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_ProductoDetalle()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProductoDetalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ProductoDetalle_Info info_det)
        {
            Lista_ProductoDetalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_ProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProductoDetalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ProductoDetalle_Info info_det)
        {
            Lista_ProductoDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_ProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProductoDetalle", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            Lista_ProductoDetalle.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            Producto_Info model = new Producto_Info();
            model.ListaProductoDetalle = Lista_ProductoDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ProductoDetalle", model.ListaProductoDetalle);
        }

        private bool Validar(Producto_Info i_validar, ref string msg)
        {
            i_validar.ListaProductoDetalle = Lista_ProductoDetalle.get_list(i_validar.IdTransaccionSession);

            if (i_validar.ListaProductoDetalle.Count == 0)
            {
                mensaje = "Debe ingresar al menos un registro en el detalle";
                return false;
            }

            if (i_validar.ListaProductoDetalle.Sum(q=> q.Ponderacion) != 100)
            {
                mensaje = "La ponderación debe sumar 100%";
                return false;
            }

            return true;
        }
        #endregion

        #region Json
        public JsonResult cargar_productos_x_proveedor(string IdProveedor = "")
        {
            List<Producto_Info> lst_productos = data_proveedor_producto.GetList_Combo(IdProveedor);

            return Json(lst_productos, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class ProductoDetalle_List
    {
        string Variable = "ProductoDetalle_Info";
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

        public void AddRow(ProductoDetalle_Info info_det, decimal IdTransaccionSession)
        {
            List<ProductoDetalle_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q => q.Descripcion == info_det.Descripcion).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

                list.Add(info_det);
            }
        }

        public void UpdateRow(ProductoDetalle_Info info_det, decimal IdTransaccionSession)
        {
            ProductoDetalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.Maximo = info_det.Maximo;
            edited_info.Minimo = info_det.Minimo;
            edited_info.Descripcion = info_det.Descripcion;
            edited_info.Ponderacion = info_det.Ponderacion;
            edited_info.EsObligatorio = info_det.EsObligatorio;
            edited_info.PorcentajeMinimo = info_det.PorcentajeMinimo;
            edited_info.ValorOptimo = info_det.ValorOptimo;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ProductoDetalle_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}