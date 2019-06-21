namespace APPPJ.Models
{
    using Newtonsoft.Json;
    public class UsuarioModel
    {
        [JsonProperty("IdUsuario")]
        public string IdUsuario { get; set; }
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }
        [JsonProperty("Clave")]
        public string Clave { get; set; }
        [JsonProperty("Estado")]
        public bool Estado { get; set; }
    }
}

