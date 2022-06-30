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
    public class CarrerasController : ControllerBase
    {
        private readonly EligeTuProfeContext _context;

        public CarrerasController(EligeTuProfeContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCarreras()
        {
          if (_context.Carreras == null)
          {
              return NotFound(new {Status = false, Message = "Carreras no encontradas."});
          }
            var carreras = await _context.Carreras.Where(c => c.Estado).ToListAsync();
            //var asignaturas = await _context.Carreras.Include(c => c.Asignaturas).ToListAsync();
            //var profesores = await _context.Asignaturas.Include(a => a.InscripcionProfesores).ToListAsync();
            return Ok(new { Status = true, Message = "Carreras obtenidas correctamente.", Carreras = carreras });
        }

        // GET: api/Carreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(int id)
        {
          if (_context.Carreras == null)
          {
              return NotFound();
          }
            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            return carrera;
        }

        // PUT: api/Carreras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrera(int id, Carrera carrera)
        {
            if (id != carrera.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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

        // POST: api/Carreras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(Carrera carrera)
        {
          if (_context.Carreras == null)
          {
              return Problem("Entity set 'EligeTuProfeContext.Carreras'  is null.");
          }
            _context.Carreras.Add(carrera);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarreraExists(carrera.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarrera", new { id = carrera.Codigo }, carrera);
        }

        // DELETE: api/Carreras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            if (_context.Carreras == null)
            {
                return NotFound();
            }
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarreraExists(int id)
        {
            return (_context.Carreras?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
