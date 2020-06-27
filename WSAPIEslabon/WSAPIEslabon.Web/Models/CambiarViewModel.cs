using System.ComponentModel.DataAnnotations;


namespace WSAPIEslabon.Web.Models
{
    public class CambiarViewModel
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string ContrasenaAnterior { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
