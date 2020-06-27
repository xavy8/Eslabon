using System.ComponentModel.DataAnnotations;


namespace WSAPIEslabon.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contrasena  { get; set; }
    }
}
