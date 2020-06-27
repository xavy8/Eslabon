using System;
using System.ComponentModel.DataAnnotations;

namespace WSAPIEslabon.Web.Models
{
    public class CrearViewModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "El campo debe tener más de 7 caracteres.")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "El campo debe tener más de 10 caracteres.")]
        public string Contrasena { get; set; }
        [Required]
        public bool Estatus { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
