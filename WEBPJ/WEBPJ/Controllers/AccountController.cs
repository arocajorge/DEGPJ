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
    public class AccountController : Controller
    {
        Usuario_Data data_usuario = new Usuario_Data();
        [AllowAnonymous]
        public ActionResult Login()
        {
            Usuario_Info model = new Usuario_Info();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Usuario_Info model)
        {
            bool usuario_clave_exist = data_usuario.validar_login(model.IdUsuario, model.Clave);
            if (usuario_clave_exist)
            {
                Usuario_Info info_usuario = data_usuario.get_info(model.IdUsuario);
                SessionFixed.IdUsuario = model.IdUsuario;
                SessionFixed.TipoUsuario = info_usuario.TipoUsuario;

                SessionFixed.IdTransaccionSession = 1 + "000000000";
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                return RedirectToAction("Index", "Usuario", new { @Area = "General" });
            }

            ViewBag.mensaje = "Credenciales incorrectas";
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}