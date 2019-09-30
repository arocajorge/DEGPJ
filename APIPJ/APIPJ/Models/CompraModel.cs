using System.Collections.Generic;

namespace APIPJ.Models
{
    public class CompraModel
    {
        public decimal IdCompra { get; set; }
        public string ProvCedulaRuc { get; set; }
        public string ProvNombre { get; set; }
        public string ProvTipo { get; set; }
        public string ProvCodigo { get; set; }
        public string IdUsuario { get; set; }
        public string Codigo { get; set; }
        public int IdProducto { get; set; }
        public double Calificacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Precio { get; set; }
        public double Cantidad { get; set; }
        public double Total { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public int SecuenciaDispositivoComp { get; set; }
        public string Dispositivo { get; set; }
        public List<CompraDetalleModel> ListaDetalle { get; set; }
    }
}