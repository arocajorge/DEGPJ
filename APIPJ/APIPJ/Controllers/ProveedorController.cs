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
        EntitiesGeneral db = new EntitiesGeneral();
        public List<ProveedorModel> GET(string IdUsuario = "")
        {
            try
            {
                List<ProveedorModel> Lista = db.Proveedor.Where(q=> q.Tipo == "PRV").Select(q => new ProveedorModel
                {
                    Codigo = q.Codigo,
                    Tipo = q.Tipo,
                    Nombre = q.Nombre,
                    RUC = q.Ruc
                }).ToList();
                Lista.ForEach(q => { q.Codigo = q.Codigo.Trim(); q.RUC = q.RUC.Trim(); q.Nombre = q.Nombre.Trim(); });
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
