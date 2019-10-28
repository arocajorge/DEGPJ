using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class Proveedor_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        [Required(ErrorMessage = ("el campo nombre es obligatorio"))]
        public string Nombre { get; set; }
        [Required(ErrorMessage = ("el campo ruc es obligatorio"))]
        public string Ruc { get; set; }
        public string ConceptoCompra { get; set; }

        #region Campos que no existen en la tabla
        public List<ProveedorProducto_Info> ListaProveedorProductoDetalle { get; set; }
        #endregion
    }
}