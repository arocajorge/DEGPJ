using DevExpress.Web;
using Newtonsoft.Json;
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
        LogError_Data data_log = new LogError_Data();
        public List<Proveedor_Info> get_list()
        {
            try
            {
                List<Proveedor_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.Proveedor.Select(q => new Proveedor_Info
                    {
                        Tipo = q.Tipo,
                        Codigo = q.Codigo,
                        Nombre = q.Nombre,
                        Ruc = q.Ruc,
                        ConceptoCompra = q.ConceptoCompra
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("get_list: "),
                    IdUsuario = ""
                });
                return new List<Proveedor_Info>();
            }
        }

        public List<Proveedor_Info> get_list_Nexpirion()
        {
            try
            {
                List<Proveedor_Info> Lista;

                using (EntitiesNexpirion db = new EntitiesNexpirion())
                {
                    Lista = db.VWFX_ProveedorMigrado.Select(q => new Proveedor_Info
                    {
                        Tipo = q.tipo,
                        Codigo = q.codigo,
                        Nombre = q.nombre,
                        Ruc = q.ruc
                    }).ToList();                   
                }
                return Lista;
            }
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("get_list_Nexpirion: "),
                    IdUsuario = ""
                });
                return null;
            }
        }

        public Proveedor_Info get_info(string Tipo, string Codigo)
        {
            try
            {
                Proveedor_Info info = new Proveedor_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Context.SetCommandTimeOut(3000);
                    Proveedor Entity = Context.Proveedor.Where(q => q.Tipo == Tipo && q.Codigo == Codigo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Proveedor_Info
                    {
                        Tipo = Entity.Tipo,
                        Codigo = Entity.Codigo,
                        Nombre = Entity.Nombre,
                        Ruc = Entity.Ruc,
                        ConceptoCompra = Entity.ConceptoCompra
                    };
                }

                return info;
            }
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("get_info: "+Tipo + " "+Codigo),
                    IdUsuario = ""
                });
                return null;
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
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("get_info_Nexpirion: "+Tipo+" "+Codigo),
                    IdUsuario = ""
                });
                return null;
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
                        Ruc = info.Ruc,
                        ConceptoCompra = info.ConceptoCompra
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
                    db_nx.FX_ProveedorMigrado.Add(new FX_ProveedorMigrado
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
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("GuardarBD: {0}", JsonConvert.SerializeObject(info)),
                    IdUsuario = ""
                });
                return false;
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
                    entity.ConceptoCompra = info.ConceptoCompra;

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
                    FX_ProveedorMigrado entity_nx = db_nx.FX_ProveedorMigrado.Where(q => q.Tipo == info.Tipo && q.Codigo == info.Codigo).FirstOrDefault();

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
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("Modificar: {0}", JsonConvert.SerializeObject(info)),
                    IdUsuario = ""
                });
                return false;
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
                    var sql_nx = "delete from FX_ProveedorMigrado where Tipo = '" + info.Tipo + "' and Codigo = '" + info.Codigo + "'";

                    db_nx.Database.ExecuteSqlCommand(sql_nx);
                }

                return true;
            }
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("EliminarBD: {0}", JsonConvert.SerializeObject(info)),
                    IdUsuario = ""
                });
                return false;
            }
        }

        public List<Proveedor_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<Proveedor_Info> Lista = new List<Proveedor_Info>();
            Lista = get_list(skip, take, args.Filter);
            return Lista;
        }

        public List<Proveedor_Info> get_list(int skip, int take, string filter)
        {
            try
            {
                List<Proveedor_Info> Lista = new List<Proveedor_Info>();
                EntitiesGeneral Context = new EntitiesGeneral();
                {
                    var lst = Context.Proveedor.Where(q => q.Tipo == "PRV" && (q.Nombre.ToString() + " " + q.Ruc + " " + q.Codigo).Contains(filter)).OrderBy(q => q.Codigo).Skip(skip).Take(take);
                    foreach (var q in lst)
                    {
                        Lista.Add(new Proveedor_Info
                        {
                            Codigo = q.Codigo,
                            Nombre = q.Nombre,
                            Ruc = q.Ruc
                        });
                    }
                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Proveedor_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, string Tipo)
        {
            //La variable args del devexpress ya trae el ID seleccionado en la propiedad Value, se pasa el IdEmpresa porque es un filtro que no tiene
            return get_info(Tipo, args.Value == null ? "" : args.Value.ToString());
        }

        public fcclient_Info get_info_ProveedorNexp(string Codigo)
        {
            try
            {
                fcclient_Info info = new fcclient_Info();
                using (EntitiesNexpirion Context = new EntitiesNexpirion())
                {
                    Context.SetCommandTimeOut(3000);
                    fcclient Entity = Context.fcclient.Where(q => q.codigo.ToString().Trim() == Codigo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new fcclient_Info
                    {
                        tipo = Entity.tipo,
                        codigo = Entity.codigo,
                        nombre = Entity.nombre,
                        tip_aplic = Entity.tip_aplic,
                        tipvta = Entity.tipvta
                    };
                }

                return info;
            }
            catch (Exception EX)
            {
                data_log.GuardarDB(new LogError_Info
                {
                    Controlador = "Proveedor_Data",
                    Error = "Error: " + EX.ToString() + " " + string.Format("get_info_ProveedorNexp: "+Codigo),
                    IdUsuario = ""
                });
                return null;
            }
        }

    }
}