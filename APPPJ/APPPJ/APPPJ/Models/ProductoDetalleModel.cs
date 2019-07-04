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

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
