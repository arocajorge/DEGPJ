using System.ComponentModel.DataAnnotations;

namespace WEBPJ.Info
{
    public class Usuario_Info
    {
        public decimal IdTransaccionSession { get; set; }
        [Required(ErrorMessage = ("el campo idusuario es obligatorio"))]
        public string IdUsuario { get; set; }
        [Required(ErrorMessage = ("el campo nombre es obligatorio"))]
        public string Nombre { get; set; }
        [Required(ErrorMessage = ("el campo tipo de usuario es obligatorio"))]
        public string TipoUsuario { get; set; }
        [Required(ErrorMessage = ("el campo clave es obligatorio"))]
        public string Clave { get; set; }
        public bool Estado { get; set; }
    }
}