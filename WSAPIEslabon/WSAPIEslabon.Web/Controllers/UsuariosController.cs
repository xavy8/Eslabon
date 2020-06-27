using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSAPIEslabon.Datos;
using WSAPIEslabon.Entidades.UsuariosEslabon;
using WSAPIEslabon.Web.Models;

namespace WSAPIEslabon.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextEslabon _context;

        public UsuariosController(DbContextEslabon context)
        {
            _context = context;
        }

        // GET: api/Usuarios/ListaUsuarios
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> ListaUsuarios()
        {
            var usuario = await _context.Usuarios.ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                Correo = u.Correo,
                Usuario = u.Usuario,
                Contrasena = u.Contrasena,
                Estatus = u.Estatus,
                Sexo = u.Sexo,
                FechaCreacion = u.FechaCreacion
            });
        }

        // GET: api/Usuarios/VerUsuario/LuisJavier
        [HttpGet("[action]/{usuario}")]
        public async Task<ActionResult<Usuarios>> VerUsuario([FromRoute] string usuario)
        {
            var usuarios = await _context.Usuarios.Where(u => u.Usuario == usuario).SingleOrDefaultAsync();

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioViewModel
            {
                Id = usuarios.Id,
                Correo = usuarios.Correo,
                Usuario = usuarios.Usuario,
                Contrasena = usuarios.Contrasena,
                Estatus = usuarios.Estatus,
                Sexo = usuarios.Sexo,
                FechaCreacion = usuarios.FechaCreacion
            });
        }

        // POST: api/Usuarios/RegistrarUsuario
        [HttpPost("[action]")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revisarUsuario = model.Usuario.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.Usuario == revisarUsuario))
            {
                return BadRequest("El usuario ya existe.");
            }

            EncriptarContrasena(model.Contrasena, out byte[] contrasenaHash, out byte[] contrasenaLlave);

            Usuarios usuario = new Usuarios
            {
                Correo = model.Correo.ToLower(),
                Usuario = model.Usuario,
                Contrasena = contrasenaHash,
                Llave = contrasenaLlave,
                Estatus = true,
                Sexo = model.Sexo,
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(usuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok(new { codigo = 200 });
        }

        private void EncriptarContrasena(string contrasena, out byte[] contrasenaHash, out byte[] contrasenaLlave)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                contrasenaLlave = hmac.Key;
                contrasenaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena));
            }
        }

        // PUT: api/Usuarios/ActualizarUsuario
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == model.Id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Correo = model.Correo;
            usuario.Usuario = model.Usuario;
            usuario.Sexo = model.Sexo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/ActivarDesactivar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarDesactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estatus = !usuario.Estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var usuario = model.Usuario;

            var checarUsuario = await _context.Usuarios.Where(u => u.Usuario == usuario).FirstOrDefaultAsync();

            if(checarUsuario == null)
            {
                return NotFound();
            }

            if(!VerificarContrasena(model.Contrasena, checarUsuario.Contrasena, checarUsuario.Llave))
            {
                return NotFound();
            }

            return Ok(new { codigo = 200 });

        }

        // PUT: api/Usuarios/CambiarContrasena
        [HttpPut("[action]")]
        public async Task<IActionResult> CambiarContrasena([FromBody] CambiarViewModel model)
        {
            var usuario = model.Usuario;

            var checarUsuario = await _context.Usuarios.Where(u => u.Usuario == usuario).FirstOrDefaultAsync();

            if (checarUsuario == null)
            {
                return NotFound();
            }

            if (!VerificarContrasena(model.ContrasenaAnterior, checarUsuario.Contrasena, checarUsuario.Llave))
            {
                return NotFound();
            }

            EncriptarContrasena(model.Contrasena, out byte[] contrasenaHash, out byte[] contrasenaLlave);
            checarUsuario.Contrasena = contrasenaHash;
            checarUsuario.Llave = contrasenaLlave;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(new { codigo = 200 });
        }

        private bool VerificarContrasena(string contrasena, byte[] contrasenaHash, byte[] contrasenaLlave)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(contrasenaLlave))
            {
                var contrasenaHashNueva = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena));
                return new ReadOnlySpan<byte>(contrasenaHash).SequenceEqual(new ReadOnlySpan<byte>(contrasenaHashNueva));
            }
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
