namespace APPPJ.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using SQLite;

    public class ProductoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("IdProducto")]
        public int IdProducto { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("CalificacionA")]
        public double CalificacionA { get; set; }

        [JsonProperty("CalificacionB")]
        public double CalificacionB { get; set; }

        [JsonProperty("CalificacionC")]
        public double CalificacionC { get; set; }

        [JsonProperty("CalificacionD")]
        public double CalificacionD { get; set; }

        [JsonProperty("PrecioA")]
        public double PrecioA { get; set; }

        [JsonProperty("PrecioB")]
        public double PrecioB { get; set; }

        [JsonProperty("PrecioC")]
        public double PrecioC { get; set; }

        [JsonProperty("PrecioD")]
        public double PrecioD { get; set; }

        [JsonProperty("EscogerPrecioPor")]
        public string EscogerPrecioPor { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
