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
    public class TablillaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TablillaController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Tablilla
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tablilla>>> GetTablilla()
        {
            return await _context.Tablilla.ToListAsync();
        }

        // GET: api/Tablilla/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tablilla>> GetTablilla(int id)
        {
            var tablilla = await _context.Tablilla.FindAsync(id);

            if (tablilla == null)
            {
                return NotFound();
            }

            return tablilla;
        }

        // PUT: api/Tablilla/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablilla(int id, Tablilla tablilla)
        {
            if (id != tablilla.Id)
            {
                return BadRequest();
            }

            _context.Entry(tablilla).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablillaExists(id))
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

        // POST: api/Tablilla
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tablilla>> PostTablilla(Tablilla tablilla)
        {
            _context.Tablilla.Add(tablilla);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablilla", new { id = tablilla.Id }, tablilla);
        }

        // DELETE: api/Tablilla/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTablilla(int id)
        {
            var tablilla = await _context.Tablilla.FindAsync(id);
            if (tablilla == null)
            {
                return NotFound();
            }

            _context.Tablilla.Remove(tablilla);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TablillaExists(int id)
        {
            return _context.Tablilla.Any(e => e.Id == id);
        }
    }
}
