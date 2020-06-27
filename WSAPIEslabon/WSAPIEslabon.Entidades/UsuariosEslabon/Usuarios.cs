using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WSAPIEslabon.Entidades.UsuariosEslabon
{
    public class Usuarios
    {
        public int Id { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        //[StringLength(50, MinimumLength = 7, ErrorMessage = "El campo debe tener más de 7 caracteres.")]
        public string Usuario{ get; set; }
        [Required]
        //[StringLength(50, MinimumLength = 10, ErrorMessage = "El campo debe tener más de 10 caracteres.")]
        public byte[] Contrasena { get; set; }
        [Required]
        public byte[] Llave { get; set; }
        [Required]
        public bool Estatus { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
