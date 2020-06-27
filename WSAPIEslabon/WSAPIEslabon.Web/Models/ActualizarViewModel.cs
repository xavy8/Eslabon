using System;
using System.ComponentModel.DataAnnotations;

namespace WSAPIEslabon.Web.Models
{
    public class ActualizarViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "El campo debe tener más de 7 caracteres.")]
        public string Usuario { get; set; }
        [Required]
        public string Sexo { get; set; }
    }
}
