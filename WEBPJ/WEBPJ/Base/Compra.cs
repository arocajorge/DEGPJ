//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEBPJ.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compra
    {
        public decimal IdCompra { get; set; }
        public string ProvCedulaRuc { get; set; }
        public string ProvNombre { get; set; }
        public string ProvTipo { get; set; }
        public string ProvCodigo { get; set; }
        public string IdUsuario { get; set; }
        public string Codigo { get; set; }
        public int IdProducto { get; set; }
        public double Calificacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Precio { get; set; }
        public double Cantidad { get; set; }
        public double Total { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public int SecuenciaDispositivoComp { get; set; }
        public string Dispositivo { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
