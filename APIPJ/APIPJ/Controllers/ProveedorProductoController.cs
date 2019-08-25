using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Bases;
    using APIPJ.Models;
    public class ProveedorProductoController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();

        public List<ProveedorProductoModel> GET(string IdUsuario = "")
        {
            try
            {
                List<ProveedorProductoModel> Lista = db.ProveedorProducto.Include("Producto").Select(q => new ProveedorProductoModel
                {
                    Tipo = q.Tipo,
                    Codigo = q.Codigo,
                    Secuencia = q.Secuencia,
                    IdProducto = q.IdProducto,
                    Descripcion = q.Producto.Descripcion
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
                        Controlador = "ProveedorProducto/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new List<ProveedorProductoModel>();
            }
        }
    }
}
