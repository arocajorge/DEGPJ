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
        [Required(ErrorMessage = ("el campo calificacion A es obligatorio"))]
        public double CalificacionA { get; set; }
        [Required(ErrorMessage = ("el campo calificacion B es obligatorio"))]
        public double CalificacionB { get; set; }
        [Required(ErrorMessage = ("el campo calificacion C es obligatorio"))]
        public double CalificacionC { get; set; }
        [Required(ErrorMessage = ("el campo calificacion D es obligatorio"))]
        public double CalificacionD { get; set; }
        [Required(ErrorMessage = ("el campo precio A es obligatorio"))]
        public double PrecioA { get; set; }
        [Required(ErrorMessage = ("el campo precio B es obligatorio"))]
        public double PrecioB { get; set; }
        [Required(ErrorMessage = ("el campo precio C es obligatorio"))]
        public double PrecioC { get; set; }
        [Required(ErrorMessage = ("el campo precio C es obligatorio"))]
        public double PrecioD { get; set; }
        public bool Estado { get; set; }


        #region Campos que no existen en la tabla
        public List<ProductoDetalle_Info> ListaProductoDetalle { get; set; }
        #endregion
    }
}