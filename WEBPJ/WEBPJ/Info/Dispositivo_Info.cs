using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class Dispositivo_Info
    {
        public int IdDispositivo { get; set; }
        [Required(ErrorMessage = ("el campo dispositivo es obligatorio"))]
        public string Dispositivo1 { get; set; }
        public string di_Descripcion { get; set; }
    }
}