using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EligeTuProfeAPI.Data;
using EligeTuProfeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace EligeTuProfeAPI.Controlleres
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturasController : ControllerBase
    {
        private readonly EligeTuProfeContext _context;

        public AsignaturasController(EligeTuProfeContext context)
        {
            _context = context;
        }

        // GET: api/Asignaturas/carrera/id
        [Authorize]
        [HttpGet("carrera/{id}")]
        public async Task<IActionResult> GetAsignaturasCarrera(int id)
        {
          if (_context.Asignaturas == null)
          {
              return NotFound(new { Status = false, Message = "Carrera no posee asignaturas." });
          }
          var asignaturas = await _context.Asignaturas.Where(a => a.Estado && a.CodigoCarrera == id).ToListAsync();
          return Ok(new { Status = true, Message = "Asignaturas obtenidas correctamente.", Asignaturas = asignaturas });
        }

        [Authorize]
        [HttpGet("profesores/{id}")]
        public async Task<IActionResult> GetProfesoresAsignatura(int id)
        {
            if (_context.Asignaturas == null)
            {
                return NotFound(new { Status = false, Message = "Carrera no posee asignaturas." });
            }
            var profesores = await (from profesor in _context.Profesors join inscripcion in _context.InscripcionProfesors on 
                              profesor.Rut equals inscripcion.Rut where inscripcion.CodigoAsignatura == id
                              select new
                              {
                                  profesor = profesor,
                                  inscripcion = inscripcion
                              }).ToListAsync();

            //var asignaturas = await _context.Asignaturas.Where(a => a.Estado && a.CodigoCarrera == id).ToListAsync();
            return Ok(new { Status = true, Message = "Profesores obtenidos correctamente.", Profesores = profesores });
        }

        // GET: api/Asignaturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura(int id)
        {
          if (_context.Asignaturas == null)
          {
              return NotFound();
          }
            var asignatura = await _context.Asignaturas.FindAsync(id);

            if (asignatura == null)
            {
                return NotFound();
            }

            return asignatura;
        }
        
        // PUT: api/Asignaturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignatura(int id, Asignatura asignatura)
        {
            if (id != asignatura.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(asignatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaturaExists(id))
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

        // POST: api/Asignaturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignatura>> PostAsignatura(Asignatura asignatura)
        {
          if (_context.Asignaturas == null)
          {
              return Problem("Entity set 'EligeTuProfeContext.Asignaturas'  is null.");
          }
            _context.Asignaturas.Add(asignatura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AsignaturaExists(asignatura.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAsignatura", new { id = asignatura.Codigo }, asignatura);
        }

        // DELETE: api/Asignaturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura(int id)
        {
            if (_context.Asignaturas == null)
            {
                return NotFound();
            }
            var asignatura = await _context.Asignaturas.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            _context.Asignaturas.Remove(asignatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignaturaExists(int id)
        {
            return (_context.Asignaturas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
