using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSAPIEslabon.Web.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public byte[] Contrasena { get; set; }
        public bool Estatus { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
