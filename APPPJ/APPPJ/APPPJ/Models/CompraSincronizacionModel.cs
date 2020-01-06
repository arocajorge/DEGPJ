namespace APPPJ.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CompraSincronizacionModel 
    {
        [JsonProperty("IdCompra")]
        public decimal IdCompra { get; set; }

        [JsonProperty("ProvCedulaRuc")]
        public string ProvCedulaRuc { get; set; }

        [JsonProperty("ProvNombre")]
        public string ProvNombre { get; set; }

        [JsonProperty("ProvCodigo")]
        public string ProvCodigo { get; set; }

        [JsonProperty("IdUsuario")]
        public string IdUsuario { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("IdProducto")]
        public int IdProducto { get; set; }

        [JsonProperty("Calificacion")]
        public double Calificacion { get; set; }

        [JsonProperty("Fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("Precio")]
        public double Precio { get; set; }

        [JsonProperty("Cantidad")]
        public double Cantidad { get; set; }

        [JsonProperty("Total")]
        public double Total { get; set; }

        [JsonProperty("Comentario")]
        public string Comentario { get; set; }

        [JsonProperty("Estado")]
        public string Estado { get; set; }

        [JsonProperty("Dispositivo")]
        public string Dispositivo { get; set; }

        [JsonProperty("ListaDetalle")]
        public List<CompraDetalleModel> ListaDetalle { get; set; }
        public bool Sincronizado { get; internal set; }
        public int PKSQLite { get; internal set; }
    }
}
