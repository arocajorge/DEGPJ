namespace APPPJ.Models
{
    using Newtonsoft.Json;
    using SQLite;
    using System;
    public class CompraDetalleModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("IdCompra")]
        public decimal IdCompra { get; set; }
        [JsonProperty("Secuencia")]
        public int Secuencia { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("Minimo")]
        public double Minimo { get; set; }
        [JsonProperty("Maximo")]
        public double Maximo { get; set; }
        [JsonProperty("Ponderacion")]
        public double Ponderacion { get; set; }
        [JsonProperty("EsObligatorio")]
        public bool EsObligatorio { get; set; }
        [JsonProperty("PorcentajeMinimo")]
        public double PorcentajeMinimo { get; set; }
        [JsonProperty("ValorOptimo")]
        public string ValorOptimo { get; set; }
        [JsonProperty("Valor")]
        public double Valor { get; set; }
    }
}
