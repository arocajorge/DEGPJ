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
        public long IdProducto { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("CalificacionA")]
        public long CalificacionA { get; set; }

        [JsonProperty("CalificacionB")]
        public long CalificacionB { get; set; }

        [JsonProperty("CalificacionC")]
        public long CalificacionC { get; set; }

        [JsonProperty("CalificacionD")]
        public long CalificacionD { get; set; }

        [JsonProperty("PrecioA")]
        public long PrecioA { get; set; }

        [JsonProperty("PrecioB")]
        public long PrecioB { get; set; }

        [JsonProperty("PrecioC")]
        public long PrecioC { get; set; }

        [JsonProperty("PrecioD")]
        public long PrecioD { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
