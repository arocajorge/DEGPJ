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
        Dispositivo_Data data_dispositivo = new Dispositivo_Data();
        Producto_Data data_producto = new Producto_Data();
        Proveedor_Data data_proveedor = new Proveedor_Data();

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
                            Lista = db.vwCompra.Where(q=> fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).OrderByDescending(q => q.IdCompra).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                CodProducto = q.Codigo,
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
                            Lista = db.vwCompra.Where(q=>q.IdUsuario == IdUsuario && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).OrderByDescending(q => q.IdCompra).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                CodProducto = q.Codigo,
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
                            Lista = db.vwCompra.Where(q => q.Estado == Estado && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).OrderByDescending(q => q.IdCompra).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                CodProducto = q.Codigo,
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
                            Lista = db.vwCompra.Where(q => q.Estado == Estado && q.IdUsuario == IdUsuario && fecha_ini <= q.Fecha && q.Fecha <= fecha_fin).OrderByDescending(q=>q.IdCompra).Select(q => new Compra_Info
                            {
                                IdCompra = q.IdCompra,
                                ProvCedulaRuc = q.ProvCedulaRuc,
                                ProvNombre = q.ProvNombre,
                                ProvCodigo = q.ProvCodigo,
                                IdUsuario = q.IdUsuario,
                                IdProducto = q.IdProducto,
                                CodProducto = q.Codigo,
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
                Lista.ForEach(q=> q.EstadoCompra = ( q.Estado=="A"? "PROCESADO": (q.Estado == "P" ? "PENDIENTE": (q.Estado == "I" ? "NO PROCESADO": "ANULADO"))));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Compra_Info get_info(decimal IdCompra)
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
                        Estado = Entity.Estado,
                        Codigo= Entity.Codigo
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

        private int get_SecuencialDispositivo(string IdUsuario, string Dispositivo)
        {
            try
            {
                int Secuencia = 1;
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var lst = from q in Context.Compra
                              where q.IdUsuario == IdUsuario && q.Dispositivo== Dispositivo
                              select q;
                    if (lst.Count() > 0)
                        Secuencia = lst.Max(q => q.SecuenciaDispositivoComp) + 1;
                }
                return Secuencia;
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

        public bool GuardarBD(Compra_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var info_dispositivo = data_dispositivo.get_info(info.Dispositivo);
                    if (info_dispositivo == null )
                    {
                        db.Dispositivo.Add(new Dispositivo
                        {
                            IdDispositivo = data_dispositivo.get_id(),
                            Dispositivo1 = info.Dispositivo,
                            di_Descripcion = ""
                        });
                    }
                    
                    db.Compra.Add(new Compra
                    {
                        IdCompra = info.IdCompra = get_id(),
                        ProvCedulaRuc = info.ProvCedulaRuc,
                        ProvNombre = info.ProvNombre,
                        ProvCodigo = info.ProvCodigo,
                        ProvTipo = info.ProvTipo,
                        IdUsuario = info.IdUsuario,
                        IdProducto = info.IdProducto,
                        Calificacion = info.Calificacion,
                        Precio = info.Precio,
                        Fecha = info.Fecha.Date,
                        Cantidad = info.Cantidad,
                        Total = info.Total,
                        Comentario = info.Comentario,
                        Dispositivo = info.Dispositivo,
                        SecuenciaDispositivoComp = info.SecuenciaDispositivoComp = get_SecuencialDispositivo(info.IdUsuario, info.Dispositivo),
                        Codigo = info.SecuenciaDispositivoComp.ToString().PadLeft(10, '0'),
                        Estado = info.Estado
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

        public bool ModificarBD(Compra_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Compra entity = db.Compra.Where(q => q.IdCompra == info.IdCompra).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.ProvCodigo = info.ProvCodigo;
                        entity.ProvCedulaRuc = info.ProvCedulaRuc;
                        entity.ProvNombre = info.ProvNombre;
                        entity.ProvTipo = info.ProvTipo;
                        entity.Fecha = info.Fecha.Date;
                        entity.Calificacion = info.Calificacion;
                        entity.Precio = info.Precio;
                        entity.Cantidad = info.Cantidad;
                        entity.Total = info.Total;
                        entity.Estado = info.Estado;
                        entity.Comentario = info.Comentario;
                    }

                    var lst_compra_detalle = db.CompraDetalle.Where(q => q.IdCompra == info.IdCompra).ToList();
                    db.CompraDetalle.RemoveRange(lst_compra_detalle);

                    if (info.lst_CompraDetProducto != null)
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

        public bool ActualizarEstadoBD(Compra_Info info)
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

        public bool GuardarOrdenCompraBD(Compra_Info info)
        {
            try
            {
                using (EntitiesNexpirion db = new EntitiesNexpirion())
                {
                    var info_producto = data_producto.get_info_ProductoNexp(info.CodProducto.ToString().Trim());
                    var info_proveedor_nx = data_proveedor.get_info_ProveedorNexp(info.ProvCodigo.ToString().Trim());
                    var info_proveedor = data_proveedor.get_info("PRV",info.ProvCodigo.ToString().Trim());
                    var info_bodega = data_producto.get_info_BodegaNexp(info.CodProducto.ToString().Trim());
                    var nombre = info_producto.nombre.ToString().Substring(0,59);
                    dbultnum entity = db.dbultnum.Where(q => q.tipo == "CO").FirstOrDefault();
                    var num_compra = entity.numero + 1;

                    db.indocume.Add(new indocume
                    {
                        tipo = "CO",
                        numero = num_compra,
                        pedido = 0,
                        fecha = info.Fecha,
                        fecha_fac = info.Fecha,
                        fecha_com = info.Fecha,
                        producto = info_producto.codigo,
                        nombre = nombre,
                        cantidad = Convert.ToDecimal(info.Cantidad),
                        proveedor = info_proveedor_nx.codigo,
                        concepto = info_proveedor.ConceptoCompra,
                        plazo = 0,
                        porc_desc = 0,
                        factor = 0,
                        total = Convert.ToDecimal(info.Total),
                        costo = Convert.ToDecimal(info.Precio),
                        fob = 0,
                        orden = "",
                        comentario = info.Comentario,
                        tip_aplic = "",
                        num_aplic = 0,
                        tip_aplix = "",
                        num_aplix = 0,
                        bloqueado = false,
                        aprobado = false,
                        usuario = "",
                        digitado = DateTime.Now.Date,
                        fecha_apr = DateTime.Now.Date,
                        cantidad_op = 0,
                        impreso = false,
                        eliminado = false,
                        bodega_int = info_bodega.bodega_int,
                        bodega_orig = "",
                        solicita = false,
                        aceptado = false,
                        fecha_soli = DateTime.Now.Date,
                        fecha_recep = DateTime.Now.Date,
                        fecha_acep = DateTime.Now.Date,
                        recibido = false,
                        lote = "",
                        num_recibo = info.Codigo,
                        usr_agr = "",
                        usr_cor = "",
                        centro = ""
                    });

                    db.fcmovinv.Add(new fcmovinv
                    {
                        tipo = "CO",
                        numero = num_compra,
                        numreg = 1,
                        fecha = info.Fecha,
                        producto = info_producto.codigo,
                        nombre = nombre,
                        bodega = "",
                        fra = Convert.ToDecimal(info.Precio),
                        peso = info_producto.peso,
                        und = Convert.ToDecimal(info.Cantidad),
                        cantidad = Convert.ToDecimal(info.Cantidad),
                        stock = 0,
                        tip_ped ="",
                        pedido = 0,
                        tipreg = 1,
                        descuento = 0,
                        precio_vta = Convert.ToDecimal(info.Precio),
                        precio_lst = Convert.ToDecimal(info.Precio),
                        subtotal = Convert.ToDecimal(info.Total),
                        costo_und = Convert.ToDecimal(info.Precio),
                        costo = Convert.ToDecimal(info.Precio),
                        promedio =0,
                        tip_prec = 0,
                        tip_produc = info_producto.tipoitm,
                        porc_desc = 0,
                        sucursal = "",
                        cliente = "",
                        vendedor = "",
                        servicio = false,
                        ubicacion = "",
                        motivo = "",
                        eliminado= false,
                        usuario = "",
                        digitado =DateTime.Now.Date,
                        concepto = "",
                        comentario= info.Comentario,
                        bodega_int = info_bodega.bodega_int,
                        lote = "",
                        usr_agr = "",
                        usr_cor = ""
                    });

                    entity.numero = num_compra;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public List<Compra_Info> get_list_rpt(string ProvCodigo, string IdUsuario, int IdProducto, string Estado, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                List<Compra_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.vwCompra.Where(q => fecha_ini <= q.Fecha && q.Fecha <= fecha_fin
                    && q.ProvCodigo == (ProvCodigo=="" ? q.ProvCodigo : ProvCodigo)
                    && q.IdUsuario == (IdUsuario == "" ? q.IdUsuario : IdUsuario)
                    && q.IdProducto == (IdProducto == 0 ? q.IdProducto : IdProducto)
                    && q.Estado == (Estado == "" ? q.Estado : Estado)
                    ).OrderByDescending(q => q.IdCompra).Select(q => new Compra_Info
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

                Lista.ForEach(q => q.EstadoCompra = (q.Estado == "A" ? "PROCESADO" : (q.Estado == "P" ? "PENDIENTE" : (q.Estado == "I" ? "NO PROCESADO" : "ANULADO"))));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}