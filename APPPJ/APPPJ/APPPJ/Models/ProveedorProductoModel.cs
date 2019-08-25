namespace APPPJ.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class ProveedorProductoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Secuencia")]
        public int Secuencia { get; set; }

        [JsonProperty("IdProducto")]
        public int IdProducto { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
