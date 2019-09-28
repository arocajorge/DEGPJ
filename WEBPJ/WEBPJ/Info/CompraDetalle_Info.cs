using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class CompraDetalle_Info
    {
        public decimal IdCompra { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Ponderacion { get; set; }
        public bool EsObligatorio { get; set; }
        public double PorcentajeMinimo { get; set; }
        public string ValorOptimo { get; set; }
        public double Valor { get; set; }

        public double PonderacionFinal { get; set; }
    }
}