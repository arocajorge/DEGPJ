using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class Compra_Data
    {
        public List<Compra_Info> get_list(DateTime fecha_ini, DateTime fecha_fin, string IdUsuario, string Estado)
        {
            try
            {
                List<Compra_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    if ( Estado == "" || Estado == null)
                    {
                        if (IdUsuario == "" || IdUsuario == null)
                        {
                            Lista = db.vwCompra.Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario,
                                Estado = q.Estado,
                                NomProducto = q.NomProducto,
                                NomUsuario = q.NomUsuario
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.vwCompra.Where(q=>q.IdUsuario == IdUsuario).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario,
                                Estado = q.Estado,
                                NomProducto = q.NomProducto,
                                NomUsuario = q.NomUsuario
                            }).ToList();
                        }                       
                    }
                    else
                    {
                        if (IdUsuario == "" || IdUsuario == null)
                        {
                            Lista = db.vwCompra.Where(q => q.Estado == Estado).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario,
                                Estado = q.Estado,
                                NomProducto = q.NomProducto,
                                NomUsuario = q.NomUsuario
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.vwCompra.Where(q => q.Estado == Estado && q.IdUsuario == IdUsuario).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario,
                                Estado = q.Estado,
                                NomProducto = q.NomProducto,
                                NomUsuario = q.NomUsuario
                            }).ToList();
                        }
                       
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Compra_Info get_info(int IdCompra)
        {
            try
            {
                Compra_Info info = new Compra_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Compra Entity = Context.Compra.Where(q => q.IdCompra == IdCompra).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Compra_Info
                    {
                        IdCompra = Entity.IdCompra,
                        ProvCedulaRuc = Entity.ProvCedulaRuc,
                        ProvNombre = Entity.ProvNombre,
                        ProvCodigo = Entity.ProvCodigo,
                        IdUsuario = Entity.IdUsuario,
                        IdProducto = Entity.IdProducto,
                        Calificacion = Entity.Calificacion,
                        Fecha = Entity.Fecha,
                        Precio = Entity.Precio,
                        Cantidad = Entity.Cantidad,
                        Total = Entity.Total,
                        Comentario = Entity.Comentario,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool ModificarBD(List<Compra_Info> Lista)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    foreach (var item in Lista)
                    {
                        Compra entity = db.Compra.Where(q => q.IdCompra == item.IdCompra).FirstOrDefault();

                        if (entity != null)
                        {
                            entity.IdProducto = item.IdProducto;
                            entity.IdUsuario = item.IdUsuario;
                            entity.Calificacion = item.Calificacion;
                            entity.Fecha = item.Fecha;
                            entity.Precio = item.Precio;
                            entity.Cantidad = item.Cantidad;
                            entity.Total = item.Total;
                            entity.Estado = item.Estado;
                            entity.Comentario = item.Comentario;
                        }                       
                    }
                    

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        public bool AnularBD(Compra_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Compra entity = db.Compra.Where(q => q.IdCompra == info.IdCompra).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = "I";

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}