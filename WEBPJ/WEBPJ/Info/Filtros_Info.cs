using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEBPJ.Helps;

namespace WEBPJ.Info
{
    public class Filtros_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha inicio debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha fin debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_fin { get; set; }

        public Filtros_Info()
        {
            IdTransaccionSession = 0;
            IdUsuario = SessionFixed.IdUsuario;
            Estado = "";
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }
}