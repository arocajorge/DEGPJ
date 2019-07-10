using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class Proveedor_Data
    {
        public List<Proveedor_Info> get_list(bool MostrarAnulados)
        {
            try
            {
                List<Proveedor_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = db.Proveedor.Select(q => new Proveedor_Info
                        {
                            Tipo = q.Tipo,
                            Codigo = q.Codigo,
                            Nombre = q.Nombre,
                            Ruc = q.Ruc
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.Proveedor.Select(q => new Proveedor_Info
                        {
                            Tipo = q.Tipo,
                            Codigo = q.Codigo,
                            Nombre = q.Nombre,
                            Ruc = q.Ruc
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

        public List<Proveedor_Info> get_list_Nexpirion()
        {
            try
            {
                List<Proveedor_Info> Lista;

                using (EntitiesNexpirion db = new EntitiesNexpirion())
                {
                    Lista = db.vwProveedor.Select(q => new Proveedor_Info
                    {
                        Tipo = q.tipo,
                        Codigo = q.codigo,
                        Nombre = q.nombre,
                        Ruc = q.ruc
                    }).ToList();                   
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Proveedor_Info get_info(string Tipo, string Codigo)
        {
            try
            {
                Proveedor_Info info = new Proveedor_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Proveedor Entity = Context.Proveedor.Where(q => q.Tipo == Tipo && q.Codigo == Codigo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Proveedor_Info
                    {
                        Tipo = Entity.Tipo,
                        Codigo = Entity.Codigo,
                        Nombre = Entity.Nombre,
                        Ruc = Entity.Ruc
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Proveedor_Info get_info_Nexpirion(string Tipo, string Codigo)
        {
            try
            {
                Proveedor_Info info = new Proveedor_Info();
                using (EntitiesNexpirion Context = new EntitiesNexpirion())
                {
                    fcclient Entity = Context.fcclient.Where(q => q.tipo == Tipo && q.codigo.Trim() == Codigo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Proveedor_Info
                    {
                        Tipo = Entity.tipo,
                        Codigo = Entity.codigo,
                        Nombre = Entity.nombre.Trim(),
                        Ruc = Entity.ruc.Trim()
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(Proveedor_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    db.Proveedor.Add(new Proveedor
                    {
                        Tipo = info.Tipo,
                        Codigo = info.Codigo,
                        Nombre = info.Nombre,
                        Ruc = info.Ruc
                    });

                    //detalle
                    if (info.ListaProveedorProductoDetalle != null)
                    {
                        int Secuencia = 1;
                        foreach (var item in info.ListaProveedorProductoDetalle)
                        {
                            db.ProveedorProducto.Add(new ProveedorProducto
                            {
                                Tipo = info.Tipo,
                                Codigo = info.Codigo,
                                Secuencia = Secuencia++,
                                IdProducto = item.IdProducto
                            });

                        }
                    }
                    db.SaveChanges();
                }

                using (EntitiesNexpirion db_nx = new EntitiesNexpirion())
                {
                    db_nx.Proveedor.Add(new Proveedor
                    {
                        Tipo = info.Tipo,
                        Codigo = info.Codigo,
                        Nombre = info.Nombre,
                        Ruc = info.Ruc
                    });
                   
                    db_nx.SaveChanges();
                }

                return true;
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public bool ModificarBD(Proveedor_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Proveedor entity = db.Proveedor.Where(q => q.Tipo == info.Tipo && q.Codigo == info.Codigo).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Tipo = info.Tipo;
                    entity.Codigo = info.Codigo;
                    entity.Ruc = info.Ruc;

                    var lst_detalle = db.ProveedorProducto.Where(q => q.Tipo == info.Tipo && q.Codigo == info.Codigo).ToList();
                    db.ProveedorProducto.RemoveRange(lst_detalle);

                    if (info.ListaProveedorProductoDetalle != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.ListaProveedorProductoDetalle)
                        {
                            db.ProveedorProducto.Add(new ProveedorProducto
                            {
                                Tipo = info.Tipo,
                                Codigo = info.Codigo,
                                Secuencia = Secuencia++,
                                IdProducto = item.IdProducto
                            });
                        }
                    }
                    db.SaveChanges();
                }

                using (EntitiesNexpirion db_nx = new EntitiesNexpirion())
                {
                    Proveedor entity_nx = db_nx.Proveedor.Where(q => q.Tipo == info.Tipo && q.Codigo == info.Codigo).FirstOrDefault();

                    if (entity_nx == null)
                    {
                        return false;
                    }

                    entity_nx.Tipo = info.Tipo;
                    entity_nx.Codigo = info.Codigo;
                    entity_nx.Ruc = info.Ruc;

                    db_nx.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EliminarBD(Proveedor_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var sql_det = "delete from ProveedorProducto where Tipo = '" + info.Tipo + "' and Codigo = '" + info.Codigo + "'";
                    db.Database.ExecuteSqlCommand(sql_det);

                    var sql = "delete from Proveedor where Tipo = '" + info.Tipo + "' and Codigo = '" + info.Codigo + "'";
                    db.Database.ExecuteSqlCommand(sql);
                }

                using (EntitiesNexpirion db_nx = new EntitiesNexpirion())
                {
                    var sql_nx = "delete from Proveedor where Tipo = '" + info.Tipo + "' and Codigo = '" + info.Codigo + "'";

                    db_nx.Database.ExecuteSqlCommand(sql_nx);
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