namespace APIPJ.Models
{
    public class ProductoModel
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double CalificacionA { get; set; }
        public double CalificacionB { get; set; }
        public double CalificacionC { get; set; }
        public double CalificacionD { get; set; }
        public double PrecioA { get; set; }
        public double PrecioB { get; set; }
        public double PrecioC { get; set; }
        public double PrecioD { get; set; }
        public bool Estado { get; set; }
        public string EscogerPrecioPor { get; internal set; }
    }
}