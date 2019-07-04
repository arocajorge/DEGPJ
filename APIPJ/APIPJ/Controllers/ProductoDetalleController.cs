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
    public class ProductoDetalleController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();

        public List<ProductoDetalleModel> GET(string IdUsuario = "")
        {
            try
            {
                List<ProductoDetalleModel> Lista = db.ProductoDetalle.Select(q => new ProductoDetalleModel
                {
                    IdProducto = q.IdProducto,
                    Secuencia = q.Secuencia,
                    Descripcion = q.Descripcion,
                    Minimo = q.Minimo,
                    Maximo = q.Maximo
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
                        Controlador = "ProductoDetalle/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new List<ProductoDetalleModel>();
            }
        }
    }
}
