using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Bases;
    using APIPJ.Models;
    public class ProveedorController : ApiController
    {
        EntitiesNexpirion db = new EntitiesNexpirion();
        public List<ProveedorModel> GET(string IdUsuario = "")
        {
            try
            {
                List<ProveedorModel> Lista = db.fcclient.Select(q => new ProveedorModel
                {
                    Codigo = q.codigo,
                    Tipo = q.tipo,
                    Nombre = q.nombre,
                    RUC = q.ruc
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
                        Controlador = "Proveedor/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new List<ProveedorModel>();
            }
        }
    }
}
