using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class ProductoDetalle_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdProducto { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = ("el campo valor minimo es obligatorio"))]
        public double Minimo { get; set; }
        [Required(ErrorMessage = ("el campo valor máximo es obligatorio"))]
        public double Maximo { get; set; }
        [Required(ErrorMessage = ("el campo ponderacion es obligatorio"))]
        public double Ponderacion { get; set; }
        [Required(ErrorMessage = ("el campo es obligatorio"))]
        public bool EsObligatorio { get; set; }
        [Required(ErrorMessage = ("el campo porcentaje minimo es obligatorio"))]
        public double PorcentajeMinimo { get; set; }
        [Required(ErrorMessage = ("el campo valor óptimo es obligatorio"))]
        public string ValorOptimo { get; set; }
        [Required(ErrorMessage = ("el campo escoger precio por es obligatorio"))]
        public string EscogerPrecioPor { get; set; }

    }
}