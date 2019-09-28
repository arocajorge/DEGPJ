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
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                List<Compra_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    if ( Estado == "" || Estado == null)
                    {
                        if (IdUsuario == "" || IdUsuario == null)
                        {
                            Lista = db.vwCompra.Where(q=> fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                Calificacion = q.Calificacion,
                                Fecha = DateTime.Now,
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
                            Lista = db.vwCompra.Where(q=>q.IdUsuario == IdUsuario && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).Select(q => new Compra_Info
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
                            Lista = db.vwCompra.Where(q => q.Estado == Estado && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).Select(q => new Compra_Info
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
                            Lista = db.vwCompra.Where(q => q.Estado == Estado && q.IdUsuario == IdUsuario && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).Select(q => new Compra_Info
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

        private decimal get_id()
        {
            try
            {
                decimal Id = 1;
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var lst = from q in Context.Compra
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdCompra) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(Compra_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Compra entity = db.Compra.Where(q => q.IdCompra == info.IdCompra).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.IdProducto = info.IdProducto;
                        entity.IdUsuario = info.IdUsuario;
                        entity.Calificacion = info.Calificacion;
                        entity.Fecha = info.Fecha;
                        entity.Precio = info.Precio;
                        entity.Cantidad = info.Cantidad;
                        entity.Total = info.Total;
                        entity.Estado = info.Estado;
                        entity.Comentario = info.Comentario;
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

        public bool GuardarBD(Compra_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    db.Compra.Add(new Compra
                    {
                        IdCompra = info.IdCompra = get_id(),
                        ProvCedulaRuc = info.ProvCedulaRuc,
                        ProvNombre = info.ProvNombre,
                        ProvCodigo = info.ProvCodigo,
                        ProvTipo = info.ProvTipo,
                        IdUsuario = info.IdUsuario,
                        Codigo = info.Codigo,
                        IdProducto = info.IdProducto,
                        Calificacion = info.Calificacion,
                        Precio = info.Precio,
                        Fecha = DateTime.Now.Date,
                        Cantidad = info.Cantidad,
                        Total = info.Total,
                        Comentario = "COMPRA "+info.IdCompra,
                        Estado = "PENDIENTE"
                    });

                    if (info.lst_CompraDetProducto.Count > 0)
                    {
                        int Secuencia = 1;
                        foreach (var item in info.lst_CompraDetProducto)
                        {
                            db.CompraDetalle.Add(new CompraDetalle
                            {
                                IdCompra = info.IdCompra,
                                Descripcion = item.Descripcion,
                                Secuencia = Secuencia++,
                                Minimo = item.Minimo,
                                Maximo = item.Maximo,
                                Ponderacion = item.Ponderacion,
                                EsObligatorio = item.EsObligatorio,
                                PorcentajeMinimo = item.PorcentajeMinimo,
                                ValorOptimo = item.ValorOptimo,
                                Valor = item.Valor
                            });
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

                    entity.Estado = info.Estado;

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