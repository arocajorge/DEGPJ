using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class Producto_Data
    {
        public List<Producto_Info> get_list(bool MostrarAnulados)
        {
            try
            {
                List<Producto_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = db.Producto.Where(q => q.Estado == true).Select(q => new Producto_Info
                        {
                            IdProducto = q.IdProducto,
                            Descripcion = q.Descripcion,
                            Codigo = q.Codigo,
                            Estado = q.Estado,
                            CalificacionA = q.CalificacionA,
                            CalificacionB = q.CalificacionB,
                            CalificacionC = q.CalificacionC,
                            CalificacionD = q.CalificacionD,
                            PrecioA = q.PrecioA,
                            PrecioB = q.PrecioB,
                            PrecioC = q.PrecioC,
                            PrecioD = q.PrecioD,
                            EscogerPrecioPor = q.EscogerPrecioPor
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.Producto.Select(q => new Producto_Info
                        {
                            IdProducto = q.IdProducto,
                            Descripcion = q.Descripcion,
                            Codigo = q.Codigo,
                            Estado = q.Estado,
                            CalificacionA = q.CalificacionA,
                            CalificacionB = q.CalificacionB,
                            CalificacionC = q.CalificacionC,
                            CalificacionD = q.CalificacionD,
                            PrecioA = q.PrecioA,
                            PrecioB = q.PrecioB,
                            PrecioC = q.PrecioC,
                            PrecioD = q.PrecioD,
                            EscogerPrecioPor = q.EscogerPrecioPor
                        }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Producto_Info get_info(int IdProducto)
        {
            try
            {
                Producto_Info info = new Producto_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Producto Entity = Context.Producto.Where(q => q.IdProducto == IdProducto).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Producto_Info
                    {
                        IdProducto = Entity.IdProducto,
                        Descripcion = Entity.Descripcion,
                        Codigo = Entity.Codigo,
                        CalificacionA = Entity.CalificacionA,
                        CalificacionB = Entity.CalificacionB,
                        CalificacionC = Entity.CalificacionC,
                        CalificacionD = Entity.CalificacionD,
                        PrecioA = Entity.PrecioA,
                        PrecioB = Entity.PrecioB,
                        PrecioC = Entity.PrecioC,
                        PrecioD = Entity.PrecioD,
                        Estado = Entity.Estado,
                        EscogerPrecioPor = Entity.EscogerPrecioPor
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int get_id()
        {

            try
            {
                int ID = 1;
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Lista = db.Producto.Select(q => q.IdProducto);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarBD(Producto_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    db.Producto.Add(new Producto
                    {
                        IdProducto = info.IdProducto = get_id(),
                        Descripcion = info.Descripcion,
                        CalificacionA = info.CalificacionA,
                        CalificacionB = info.CalificacionB,
                        CalificacionC = info.CalificacionC,
                        CalificacionD = info.CalificacionD,
                        PrecioA = info.PrecioA,
                        PrecioB = info.PrecioB,
                        PrecioC = info.PrecioC,
                        PrecioD = info.PrecioD,
                        Estado = true,
                        Codigo = info.Codigo,
                        EscogerPrecioPor = info.EscogerPrecioPor
                    });

                    //detalle
                    if (info.ListaProductoDetalle != null)
                    {
                        int Secuencia = 1;
                        foreach (var item in info.ListaProductoDetalle)
                        {
                            db.ProductoDetalle.Add(new ProductoDetalle
                            {
                                IdProducto = info.IdProducto,
                                Descripcion = item.Descripcion,
                                Secuencia = Secuencia++,
                                Minimo = item.Minimo,
                                Maximo = item.Maximo,
                                Ponderacion = item.Ponderacion,
                                EsObligatorio = item.EsObligatorio,
                                PorcentajeMinimo = item.PorcentajeMinimo,
                                ValorOptimo = item.ValorOptimo
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

        public bool ModificarBD(Producto_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Producto entity = db.Producto.Where(q => q.IdProducto == info.IdProducto).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Descripcion = info.Descripcion;
                    entity.Codigo = info.Codigo;
                    entity.CalificacionA = info.CalificacionA;
                    entity.CalificacionB = info.CalificacionB;
                    entity.CalificacionC = info.CalificacionC;
                    entity.CalificacionD = info.CalificacionD;
                    entity.PrecioA = info.PrecioA;
                    entity.PrecioB = info.PrecioB;
                    entity.PrecioC = info.PrecioC;
                    entity.PrecioD = info.PrecioD;
                    entity.EscogerPrecioPor = info.EscogerPrecioPor;

                    var lst_producto_detalle = db.ProductoDetalle.Where(q => q.IdProducto == info.IdProducto).ToList();
                    db.ProductoDetalle.RemoveRange(lst_producto_detalle);

                    if (info.ListaProductoDetalle != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.ListaProductoDetalle)
                        {
                            db.ProductoDetalle.Add(new ProductoDetalle
                            {
                                IdProducto = info.IdProducto,
                                Secuencia = Secuencia++,
                                Descripcion = item.Descripcion,
                                Minimo = item.Minimo,
                                Maximo = item.Maximo,
                                Ponderacion = item.Ponderacion,
                                EsObligatorio = item.EsObligatorio,
                                PorcentajeMinimo = item.PorcentajeMinimo,
                                ValorOptimo = item.ValorOptimo
                            });
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(Producto_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Producto entity = db.Producto.Where(q => q.IdProducto == info.IdProducto).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = false;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fcproduc_Info get_info_ProductoNexp(string Codigo)
        {
            try
            {
                fcproduc_Info info = new fcproduc_Info();
                using (EntitiesNexpirion Context = new EntitiesNexpirion())
                {
                    fcproduc Entity = Context.fcproduc.Where(q => q.codigo.ToString().Trim() == Codigo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new fcproduc_Info
                    {
                        codigo = Entity.codigo,
                        nombre = Entity.nombre,
                        tipoitm = Entity.tipoitm,
                        peso = Entity.peso
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fcexipro_Info get_info_BodegaNexp(string Codigo)
        {
            try
            {
                fcexipro_Info info = new fcexipro_Info();
                using (EntitiesNexpirion Context = new EntitiesNexpirion())
                {
                    fcexipro Entity = Context.fcexipro.Where(q => q.producto.ToString().Trim() == Codigo && q.bodega_int!="").FirstOrDefault();

                    if (Entity == null) return null;
                    info = new fcexipro_Info
                    {
                        producto = Entity.producto,
                        bodega = Entity.bodega,
                        bodega_int = Entity.bodega_int
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}