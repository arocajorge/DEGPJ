using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPJ.Models
{
    public class ProveedorProductoModel
    {
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public int Secuencia { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
    }
}