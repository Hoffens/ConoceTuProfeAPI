using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EligeTuProfe.Data;
using EligeTuProfe.Models;
using Microsoft.AspNetCore.Authorization;
using EligeTuProfeAPI.Models;

namespace EligeTuProfeAPI.Controlleres
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly EligeTuProfeContext _context;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;

        public UsuariosController(EligeTuProfeContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
          return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);
            await _context.Usuarios.Include(u => u.InscripcionAlumno).ToListAsync();
            await _context.Usuarios.Include(u => u.Comentarios).ToListAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Rut)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'EligeTuProfeContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Rut))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Rut }, usuario);
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Rut == id)).GetValueOrDefault();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var userOut = _context.Usuarios.Where(u => u.Correo == login.Correo && u.UserPassword == login.UserPassword).ToList();
            if (userOut == null || userOut.Count == 0)
            {
                return Unauthorized(new { Status = false, Message = "Correo o contraseña no válidos" });
            }
            var token = _jwtAuthenticationManager.Authenticate(userOut[0].Rut, userOut[0].UserPassword);
            if (token == null)
            {
                return Unauthorized(new { Status = false, Message = "Sesión expirada, debe volver a iniciar sesión." });
            }
            return Ok(new { Status = true, Message = "Sesión iniciada correctamente", Token = token, User = userOut[0] });
        }
    }
}
