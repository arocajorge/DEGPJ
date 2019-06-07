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
                            PrecioD = q.PrecioD
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
                            PrecioD = q.PrecioD
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
                        CalificacionD = Entity.CalificacionA,
                        PrecioA = Entity.PrecioA,
                        PrecioB = Entity.PrecioB,
                        PrecioC = Entity.PrecioC,
                        PrecioD = Entity.PrecioD,
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
                        Codigo = info.Codigo
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
                                Descripcion = info.Descripcion,
                                Secuencia = Secuencia++,
                                Minimo = item.Minimo,
                                Maximo = item.Maximo
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
                                Maximo = item.Maximo
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
    }
}