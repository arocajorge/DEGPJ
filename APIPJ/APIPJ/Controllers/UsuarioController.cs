using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Models;
    using APIPJ.Bases;
    public class UsuarioController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();
        public List<UsuarioModel> GET(string IdUsuario = "")
        {
            try
            {
                List<UsuarioModel> Lista = db.Usuario.Where(q=> q.Estado == true).Select(q => new UsuarioModel
                {
                    IdUsuario = q.IdUsuario,
                    Clave = q.Clave,
                    Estado = q.Estado,
                    Nombre = q.Nombre
                }).ToList();

                return Lista;
            }
            catch (Exception ex)
            {
                using (EntitiesGeneral error = new EntitiesGeneral())
                {
                    decimal ID = error.LogError.Count() == 0 ? 1 : (error.LogError.Max(q => q.ID) + 1);
                    error.LogError.Add(new LogError
                    {
                        ID = ID,
                        Controlador = "Usuario/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new List<UsuarioModel>();
            }
        }
    }
}
