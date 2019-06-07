using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class Producto_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdProducto { get; set; }
        [Required(ErrorMessage = ("el campo descripcion es obligatorio"))]
        public string Descripcion { get; set; }
        public string Codigo { get; set; }        
        [Required(ErrorMessage = ("el campo calificación es obligatorio"))]
        public decimal CalificacionA { get; set; }
        [Required(ErrorMessage = ("el campo calificación es obligatorio"))]
        public decimal CalificacionB { get; set; }
        [Required(ErrorMessage = ("el campo calificación es obligatorio"))]
        public decimal CalificacionC { get; set; }
        [Required(ErrorMessage = ("el campo calificación es obligatorio"))]
        public decimal CalificacionD { get; set; }
        [Required(ErrorMessage = ("el campo precio es obligatorio"))]
        public decimal PrecioA { get; set; }
        [Required(ErrorMessage = ("el campo precio es obligatorio"))]
        public decimal PrecioB { get; set; }
        [Required(ErrorMessage = ("el campo precio es obligatorio"))]
        public decimal PrecioC { get; set; }
        [Required(ErrorMessage = ("el campo precio es obligatorio"))]
        public decimal PrecioD { get; set; }
        public bool Estado { get; set; }

        #region Campos que no existen en la tabla
        public List<ProductoDetalle_Info> ListaProductoDetalle { get; set; }
        #endregion
    }
}