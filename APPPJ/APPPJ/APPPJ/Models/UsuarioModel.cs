namespace APPPJ.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class UsuarioModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }

        [JsonProperty("IdUsuario")]
        public string IdUsuario { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Clave")]
        public string Clave { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}

