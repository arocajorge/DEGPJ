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
    public class UsuarioController : Controller
    {
        Usuario_Data data_usuario = new Usuario_Data();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Usuario()
        {
            List<Usuario_Info> model = data_usuario.get_list(true);
            return PartialView("_GridViewPartial_Usuario", model);
        }

        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            Usuario_Info model = new Usuario_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
            };

            Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
            ViewBag.lst_TipoUsuario = lst_TipoUsuario;

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(Usuario_Info model)
        {
            if (data_usuario.validar_existe_usuario(model.IdUsuario))
            {
                ViewBag.mensaje = "Usuario ya se encuentra registrado";
                return View(model);
            }

            if (!data_usuario.guardarDB(model))
            {
                Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
                ViewBag.lst_TipoUsuario = lst_TipoUsuario;

                return View(model);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
            ViewBag.lst_TipoUsuario = lst_TipoUsuario;

            Usuario_Info model = data_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(Usuario_Info model)
        {
            if (!data_usuario.modificarDB(model))
            {
                Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
                ViewBag.lst_TipoUsuario = lst_TipoUsuario;

                return View(model);
            }                

            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
            lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
            ViewBag.lst_TipoUsuario = lst_TipoUsuario;

            Usuario_Info model = data_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);           
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(Usuario_Info model)
        {
            if (!data_usuario.anularDB(model))
            {
                Dictionary<string, string> lst_TipoUsuario = new Dictionary<string, string>();
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.ADMINISTRADOR.ToString());
                lst_TipoUsuario.Add(@WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString(), @WEBPJ.Info.Enumeradores.eTipoUsuario.USUARIO.ToString());
                ViewBag.lst_TipoUsuario = lst_TipoUsuario;

                return View(model);
            }
                
            return RedirectToAction("Index");
        }

    }
}