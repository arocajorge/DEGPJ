using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class ProveedorProducto_Info
    {       
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = ("el campo producto es obligatorio"))]
        public int IdProducto { get; set; }        
    }
}