using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class LogError_Info
    {
        public decimal ID { get; set; }
        public string Controlador { get; set; }
        public string Error { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}