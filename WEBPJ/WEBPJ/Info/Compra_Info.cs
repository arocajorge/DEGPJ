using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class Compra_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public decimal IdCompra { get; set; }
        public string ProvCedulaRuc { get; set; }
        public string ProvNombre { get; set; }
        public string ProvTipo { get; set; }
        public string ProvCodigo { get; set; }
        [Required(ErrorMessage = ("el campo usuario es obligatorio"))]
        public string IdUsuario { get; set; }
        public string Codigo { get; set; }
        [Required(ErrorMessage = ("el campo producto es obligatorio"))]
        public int IdProducto { get; set; }
        [Required(ErrorMessage = ("el campo calificación es obligatorio"))]
        public double Calificacion { get; set; }
        [Required(ErrorMessage = ("el campo fecha es obligatorio"))]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = ("el campo precio es obligatorio"))]
        public double Precio { get; set; }
        [Required(ErrorMessage = ("el campo cantidad es obligatorio"))]
        public double Cantidad { get; set; }
        public double Total { get; set; }
        [Required(ErrorMessage = ("el campo comentario es obligatorio"))]
        public string Comentario { get; set; }
        [Required(ErrorMessage = ("el campo estado es obligatorio"))]
        public string Estado { get; set; }


        #region Campos que no existen en la tabla
        public string NomProducto { get; set; }
        public string NomUsuario { get; set; }
        public List<CompraDetalle_Info> lst_CompraDetProducto { get; set; }
        #endregion
    }
}