using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfitRatiosController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ProfitRatiosController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ProfitRatios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfitRatios>>> GetProfitRatios()
        {
            return await _context.ProfitRatios.ToListAsync();
        }

        // GET: api/ProfitRatios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfitRatios>> GetProfitRatios(int id)
        {
            var profitRatios = await _context.ProfitRatios.FindAsync(id);

            if (profitRatios == null)
            {
                return NotFound();
            }

            return profitRatios;
        }

        // PUT: api/ProfitRatios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfitRatios(int id, ProfitRatios profitRatios)
        {
            if (id != profitRatios.id)
            {
                return BadRequest();
            }

            _context.Entry(profitRatios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfitRatiosExists(id))
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

        // POST: api/ProfitRatios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProfitRatios>> PostProfitRatios(ProfitRatios profitRatios)
        {
            _context.ProfitRatios.Add(profitRatios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfitRatios", new { id = profitRatios.id }, profitRatios);
        }

        // DELETE: api/ProfitRatios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfitRatios>> DeleteProfitRatios(int id)
        {
            var profitRatios = await _context.ProfitRatios.FindAsync(id);
            if (profitRatios == null)
            {
                return NotFound();
            }

            _context.ProfitRatios.Remove(profitRatios);
            await _context.SaveChangesAsync();

            return profitRatios;
        }

        private bool ProfitRatiosExists(int id)
        {
            return _context.ProfitRatios.Any(e => e.id == id);
        }
    }
}
