namespace APPPJ.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class ProveedorModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("RUC")]
        public string Ruc { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
