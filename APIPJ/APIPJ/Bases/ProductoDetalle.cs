//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIPJ.Bases
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductoDetalle
    {
        public int IdProducto { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Ponderacion { get; set; }
        public bool EsObligatorio { get; set; }
        public double PorcentajeMinimo { get; set; }
        public string ValorOptimo { get; set; }
        public string EscogerPrecioPor { get; set; }
    
        public virtual Producto Producto { get; set; }
    }
}
