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
    
    public partial class PrecioJusto
    {
        public int IdPrecioJusto { get; set; }
        public string CedulaProveedor { get; set; }
        public string IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public decimal Calificacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
