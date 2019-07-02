using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Bases;
    using APIPJ.Models;
    public class ProductoController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();

        public List<ProductoModel> GET(string IdUsuario = "")
        {
            try
            {
                List<ProductoModel> Lista = db.Producto.Where(q => q.Estado == true).Select(q => new ProductoModel
                {
                    IdProducto = q.IdProducto
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
                        Controlador = "Producto/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new List<ProductoModel>();
            }
        }
    }
}
