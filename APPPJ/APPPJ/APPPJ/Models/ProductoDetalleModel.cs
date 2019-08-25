namespace APPPJ.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class ProductoDetalleModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("IdProducto")]
        public long IdProducto { get; set; }

        [JsonProperty("Secuencia")]
        public long Secuencia { get; set; }

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

        public double Calificacion { get; set; }
        public double PonderacionFinal { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
