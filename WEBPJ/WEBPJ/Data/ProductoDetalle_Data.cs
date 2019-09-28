using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class ProductoDetalle_Data
    {
        public List<ProductoDetalle_Info> GetList(int IdProducto)
        {
            try
            {
                List<ProductoDetalle_Info> Lista = new List<ProductoDetalle_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.ProductoDetalle.Where(q => q.IdProducto == IdProducto).Select(q => new ProductoDetalle_Info
                    {
                        IdProducto = q.IdProducto,
                        Secuencia = q.Secuencia,
                        Descripcion = q.Descripcion,
                        Minimo = q.Minimo,
                        Maximo = q.Maximo,
                        Ponderacion = q.Ponderacion,
                        EsObligatorio = q.EsObligatorio,
                        PorcentajeMinimo = q.PorcentajeMinimo,
                        ValorOptimo = q.ValorOptimo
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CompraDetalle_Info> GetList_CompraDetalle(int IdProducto)
        {
            try
            {
                List<CompraDetalle_Info> Lista = new List<CompraDetalle_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.ProductoDetalle.Where(q => q.IdProducto == IdProducto).Select(q => new CompraDetalle_Info
                    {
                        Secuencia = q.Secuencia,
                        Descripcion = q.Descripcion,
                        Minimo = q.Minimo,
                        Maximo = q.Maximo,
                        Ponderacion = q.Ponderacion,
                        EsObligatorio = q.EsObligatorio,
                        PorcentajeMinimo = q.PorcentajeMinimo,
                        ValorOptimo = q.ValorOptimo
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}