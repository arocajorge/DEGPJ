using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class ProductoDetalle_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdProducto { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
    }
}