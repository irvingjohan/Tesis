using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIEGO.Server;
using RIEGO.Shared.Entidades;

namespace RIEGO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionLlaveHorarioController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RelacionLlaveHorarioController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/RelacionLlaveHorario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelacionLlaveHorario>>> GetRelacionLlaveHorario()
        {
            return await _context.RelacionLlaveHorario.ToListAsync();
        }

        // GET: api/RelacionLlaveHorario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelacionLlaveHorario>> GetRelacionLlaveHorario(int id)
        {
            var relacionLlaveHorario = await _context.RelacionLlaveHorario.FindAsync(id);

            if (relacionLlaveHorario == null)
            {
                return NotFound();
            }

            return relacionLlaveHorario;
        }

        // PUT: api/RelacionLlaveHorario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelacionLlaveHorario(int id, RelacionLlaveHorario relacionLlaveHorario)
        {
            if (id != relacionLlaveHorario.Id)
            {
                return BadRequest();
            }

            _context.Entry(relacionLlaveHorario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelacionLlaveHorarioExists(id))
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

        // POST: api/RelacionLlaveHorario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RelacionLlaveHorario>> PostRelacionLlaveHorario(RelacionLlaveHorario relacionLlaveHorario)
        {
            _context.RelacionLlaveHorario.Add(relacionLlaveHorario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelacionLlaveHorario", new { id = relacionLlaveHorario.Id }, relacionLlaveHorario);
        }

        // DELETE: api/RelacionLlaveHorario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelacionLlaveHorario(int id)
        {
            var relacionLlaveHorario = await _context.RelacionLlaveHorario.FindAsync(id);
            if (relacionLlaveHorario == null)
            {
                return NotFound();
            }

            _context.RelacionLlaveHorario.Remove(relacionLlaveHorario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelacionLlaveHorarioExists(int id)
        {
            return _context.RelacionLlaveHorario.Any(e => e.Id == id);
        }
    }
}
