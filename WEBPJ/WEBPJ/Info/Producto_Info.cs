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

        public double CalificacionA { get; set; }
        public double CalificacionB { get; set; }
        public double CalificacionC { get; set; }
        public double CalificacionD { get; set; }
        public double PrecioA { get; set; }
        public double PrecioB { get; set; }
        public double PrecioC { get; set; }
        public double PrecioD { get; set; }
        public bool Estado { get; set; }


        #region Campos que no existen en la tabla
        public List<ProductoDetalle_Info> ListaProductoDetalle { get; set; }
        #endregion
    }
}