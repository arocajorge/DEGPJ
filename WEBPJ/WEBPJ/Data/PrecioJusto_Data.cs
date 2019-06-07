using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class PrecioJusto_Data
    {
        public List<PrecioJusto_Info> get_list(DateTime fecha_ini, DateTime fecha_fin, string IdUsuario, string Estado)
        {
            try
            {
                List<PrecioJusto_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    if (Estado == "")
                    {
                        if (IdUsuario == "")
                        {
                            Lista = db.vwPrecioJusto.Select(q => new PrecioJusto_Info
                            {
                                IdPrecioJusto = q.IdPrecioJusto,
                                IdProducto = q.IdProducto,
                                Descripcion = q.Descripcion,
                                Nombre = q.Nombre,
                                IdUsuario = q.IdUsuario,
                                Estado = q.Estado,
                                CedulaProveedor = q.CedulaProveedor,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.vwPrecioJusto.Where(q=>q.IdUsuario == IdUsuario).Select(q => new PrecioJusto_Info
                            {
                                IdPrecioJusto = q.IdPrecioJusto,
                                IdProducto = q.IdProducto,
                                IdUsuario = q.IdUsuario,
                                Descripcion = q.Descripcion,
                                Nombre = q.Nombre,
                                Estado = q.Estado,
                                CedulaProveedor = q.CedulaProveedor,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario
                            }).ToList();
                        }                       
                    }
                    else
                    {
                        if (IdUsuario == "")
                        {
                            Lista = db.vwPrecioJusto.Where(q => q.Estado == Estado).Select(q => new PrecioJusto_Info
                            {
                                IdPrecioJusto = q.IdPrecioJusto,
                                IdProducto = q.IdProducto,
                                IdUsuario = q.IdUsuario,
                                Descripcion = q.Descripcion,
                                Nombre = q.Nombre,
                                Estado = q.Estado,
                                CedulaProveedor = q.CedulaProveedor,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.vwPrecioJusto.Where(q => q.Estado == Estado && q.IdUsuario == IdUsuario).Select(q => new PrecioJusto_Info
                            {
                                IdPrecioJusto = q.IdPrecioJusto,
                                IdProducto = q.IdProducto,
                                IdUsuario = q.IdUsuario,
                                Descripcion = q.Descripcion,
                                Nombre = q.Nombre,
                                Estado = q.Estado,
                                CedulaProveedor = q.CedulaProveedor,
                                Calificacion = q.Calificacion,
                                Fecha = q.Fecha,
                                Precio = q.Precio,
                                Cantidad = q.Cantidad,
                                Total = q.Total,
                                Comentario = q.Comentario
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

        public PrecioJusto_Info get_info(int IdPrecioJusto)
        {
            try
            {
                PrecioJusto_Info info = new PrecioJusto_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    PrecioJusto Entity = Context.PrecioJusto.Where(q => q.IdPrecioJusto == IdPrecioJusto).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new PrecioJusto_Info
                    {
                        IdPrecioJusto = Entity.IdPrecioJusto,
                        CedulaProveedor = Entity.CedulaProveedor,
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
        
        public bool ModificarBD(List<PrecioJusto_Info> Lista)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    foreach (var item in Lista)
                    {
                        PrecioJusto entity = db.PrecioJusto.Where(q => q.IdPrecioJusto == item.IdPrecioJusto).FirstOrDefault();

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

        public bool AnularBD(PrecioJusto_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    PrecioJusto entity = db.PrecioJusto.Where(q => q.IdPrecioJusto == info.IdPrecioJusto).FirstOrDefault();

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