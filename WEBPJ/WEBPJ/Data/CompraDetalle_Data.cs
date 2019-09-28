using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class CompraDetalle_Data
    {
        public List<CompraDetalle_Info> get_list(int IdCompra)
        {
            try
            {
                List<CompraDetalle_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.CompraDetalle.Where(q => q.IdCompra == IdCompra).Select(q => new CompraDetalle_Info
                    {
                        IdCompra = q.IdCompra,
                        Secuencia = q.Secuencia,
                        Descripcion = q.Descripcion,
                        Minimo = q.Minimo,
                        Maximo = q.Maximo,
                        Ponderacion = q.Ponderacion,
                        EsObligatorio = q.EsObligatorio,
                        PorcentajeMinimo = q.PorcentajeMinimo,
                        ValorOptimo = q.ValorOptimo,
                        Valor = q.Valor
                    }).ToList();
                        
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}