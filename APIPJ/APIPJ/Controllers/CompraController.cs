using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Bases;
    using APIPJ.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompraController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();

        public CompraModel GET(string IdUsuario)
        {
            try
            {
                int Cont = db.Compra.Where(q => q.IdUsuario == IdUsuario).Count();
                return new CompraModel { IdCompra = Cont, IdUsuario = IdUsuario};
            }
            catch (Exception ex)
            {
                using (EntitiesGeneral error = new EntitiesGeneral())
                {
                    decimal ID = error.LogError.Count() == 0 ? 1 : (error.LogError.Max(q => q.ID) + 1);
                    error.LogError.Add(new LogError
                    {
                        ID = ID,
                        Controlador = "Compra/GET",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return new CompraModel();
            }
        }
    }
}
