using System.Web.Http;

namespace APIPJ.Controllers
{
    using APIPJ.Bases;
    using APIPJ.Models;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompraController : ApiController
    {
        EntitiesGeneral db = new EntitiesGeneral();
        Metodos data = new Metodos();

        public CompraModel GET(string IdUsuario = "", string Dispositivo = "")
        {
            try
            {
                int Cont = data.GetIdDispositivo(IdUsuario, Dispositivo);
                return new CompraModel { IdCompra = Cont, IdUsuario = IdUsuario };
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

        public void Post([FromBody]List<CompraModel> value)
        {
            string IdUsuario = value.First().IdUsuario;
            string Dispositivo = value.First().Dispositivo;

            decimal IdCompra = data.GetIdCompra(IdUsuario);
            int SecuenciaDispositivoComp = data.GetIdDispositivo(IdUsuario, Dispositivo);
            try
            {                
                foreach (var item in value)
                {
                    db.Compra.Add(new Compra
                    {
                        IdCompra = item.IdCompra = IdCompra++,
                        ProvCedulaRuc = item.ProvCedulaRuc,
                        ProvNombre = item.ProvNombre,
                        ProvTipo = item.ProvTipo,
                        ProvCodigo = item.ProvCodigo,
                        IdUsuario = item.IdUsuario,
                        Codigo = item.Codigo,
                        IdProducto = item.IdProducto,
                        Calificacion = item.Calificacion,
                        Fecha = item.Fecha,
                        Precio = item.Precio,
                        Cantidad = item.Cantidad,
                        Total = item.Total,
                        Comentario = item.Comentario,
                        Estado = item.Estado = "P",
                        Dispositivo = item.Dispositivo,
                        SecuenciaDispositivoComp = item.SecuenciaDispositivoComp = SecuenciaDispositivoComp++
                    });
                    int Secuencia = 1;
                    foreach (var det in item.ListaDetalle)
                    {
                        db.CompraDetalle.Add(new CompraDetalle
                        {
                            IdCompra = det.IdCompra = item.IdCompra,
                            Secuencia = det.Secuencia = Secuencia++,
                            Descripcion = det.Descripcion,
                            Minimo = det.Minimo,
                            Maximo = det.Maximo,
                            Ponderacion = det.Ponderacion,
                            EsObligatorio = det.EsObligatorio,
                            PorcentajeMinimo = det.PorcentajeMinimo,
                            ValorOptimo = det.ValorOptimo,
                            Valor = det.Valor
                        });
                    }
                }

                var Dis = db.Dispositivo.Where(q => q.Dispositivo1 == Dispositivo).FirstOrDefault();
                if (Dis == null)
                {
                    var lstD = db.Dispositivo.ToList();
                    int IdDispositivo = lstD.Count > 0 ? (lstD.Max(q => q.IdDispositivo) + 1) : 1;

                    db.Dispositivo.Add(new Dispositivo
                    {
                        IdDispositivo = IdDispositivo,
                        Dispositivo1 = Dispositivo
                    });
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                using (EntitiesGeneral error = new EntitiesGeneral())
                {
                    decimal ID = error.LogError.Count() == 0 ? 1 : (error.LogError.Max(q => q.ID) + 1);
                    error.LogError.Add(new LogError
                    {
                        ID = ID,
                        Controlador = "Compra/POST",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = value == null ? "" : (IdUsuario ?? "")
                    });
                }
            }
        }

        
    }
}
