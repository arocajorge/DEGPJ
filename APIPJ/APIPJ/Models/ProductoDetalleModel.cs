﻿namespace APIPJ.Models
{
    public class ProductoDetalleModel
    {
        public int IdProducto { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
    }
}