namespace APIPJ.Models
{
    public class ProductoDetalleModel
    {
        public int IdProducto { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Ponderacion { get; set; }
        public bool EsObligatorio { get; set; }
        public double PorcentajeMinimo { get; set; }
        public string ValorOptimo { get; set; }
        
    }
}