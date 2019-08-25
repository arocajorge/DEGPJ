using System;
namespace APPPJ.Models
{
    public class CatalogoModel
    {
        public string CedulaRuc { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public Enumeradores.eTipoCombo TipoCombo { get; set; }
    }
}
