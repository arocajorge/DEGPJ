namespace APIPJ.Models
{
    public class CompraDetalleModel
    {
        public decimal IdCompra { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Ponderacion { get; set; }
        public bool EsObligatorio { get; set; }
        public double PorcentajeMinimo { get; set; }
        public string ValorOptimo { get; set; }
        public double Valor { get; set; }
    }
}