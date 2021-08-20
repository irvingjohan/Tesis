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
    public class LlaveController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LlaveController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Llave
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llave>>> GetLlave()
        {
            return await _context.Llave.ToListAsync();
        }

        // GET: api/Llave/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Llave>> GetLlave(int id)
        {
            var llave = await _context.Llave.FindAsync(id);

            if (llave == null)
            {
                return NotFound();
            }

            return llave;
        }

        // PUT: api/Llave/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLlave(int id, Llave llave)
        {
            if (id != llave.Id)
            {
                return BadRequest();
            }

            _context.Entry(llave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlaveExists(id))
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

        // POST: api/Llave
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llave>> PostLlave(Llave llave)
        {
            _context.Llave.Add(llave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLlave", new { id = llave.Id }, llave);
        }

        // DELETE: api/Llave/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLlave(int id)
        {
            var llave = await _context.Llave.FindAsync(id);
            if (llave == null)
            {
                return NotFound();
            }

            _context.Llave.Remove(llave);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlaveExists(int id)
        {
            return _context.Llave.Any(e => e.Id == id);
        }
    }
}
