using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPJ.Data;
using WEBPJ.Info;

namespace WEBPJ.Controllers
{
    public class DispositivoController : Controller
    {
        #region Variables
        Dispositivo_Data data_dispositivo = new Dispositivo_Data();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Dispositivo()
        {
            List<Dispositivo_Info> model = data_dispositivo.get_list();

            return PartialView("_GridViewPartial_Dispositivo", model);
        }
        #endregion

        #region Acciones
        public ActionResult Modificar(int IdDispositivo = 0)
        {
            Dispositivo_Info model = data_dispositivo.get_info_by_ID(IdDispositivo);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Dispositivo_Info model)
        {
            if (!data_dispositivo.ModificarBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}