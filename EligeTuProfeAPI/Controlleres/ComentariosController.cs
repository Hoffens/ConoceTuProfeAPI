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

namespace EligeTuProfeAPI.Controlleres
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly EligeTuProfeContext _context;

        public ComentariosController(EligeTuProfeContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
          if (_context.Comentarios == null)
          {
              return NotFound();
          }
            return await _context.Comentarios.ToListAsync();
        }

        [HttpGet("profesor/{id}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosProfesor(int id)
        {
            if (_context.Comentarios == null)
            {
                return NotFound();
            }
            return await _context.Comentarios.Where(c => c.Estado && c.RutProfesor == id).ToArrayAsync();
        }

        // GET: api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
          if (_context.Comentarios == null)
          {
              return NotFound();
          }
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }
        [Authorize]
        // Demo Get: Comentarios por profesor/año/periodo->semestre
        [HttpGet("{cod}/{id}/{year}/{periodo}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarioProfesor2(int cod, int id, int year, int periodo)
        {
            if (_context.Comentarios == null)
            {
                return NotFound();
            }
            var comentarios = await _context.Comentarios.Where(c => c.CodigoAsignatura == cod && c.RutProfesor == id && c.Year == year && c.Periodo == periodo).ToListAsync();
            return comentarios;
        }
       

        // PUT: api/Comentarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
          if (_context.Comentarios == null)
          {
              return Problem("Entity set 'EligeTuProfeContext.Comentarios'  is null.");
          }
          // verificar que el alumno esté inscrito en la asignatura y que no haya escrito un comentario anteriormente
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentario", new { id = comentario.Id }, comentario);
        }

        // DELETE: api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            if (_context.Comentarios == null)
            {
                return NotFound();
            }
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioExists(int id)
        {
            return (_context.Comentarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
