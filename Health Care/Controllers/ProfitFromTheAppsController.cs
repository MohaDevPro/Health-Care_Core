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
    public class ProfitFromTheAppsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ProfitFromTheAppsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ProfitFromTheApps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfitFromTheApp>>> GetProfitFromTheApp()
        {
            return await _context.ProfitFromTheApp.ToListAsync();
        }

        [HttpGet("{hospitalId}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<ProfitFromTheApp>> GetProfitFromTheAppBasedOnCategory(int hospitalId, int clinicId, int doctorId)
        {
            var ProfitObj = await _context.ProfitFromTheApp
                .FirstOrDefaultAsync(x => x.hospitalId == hospitalId && x.clinicId == clinicId && x.doctorId == doctorId);

            return ProfitObj;
        }

        // GET: api/ProfitFromTheApps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfitFromTheApp>> GetProfitFromTheApp(int id)
        {
            var profitFromTheApp = await _context.ProfitFromTheApp.FindAsync(id);

            if (profitFromTheApp == null) { return NotFound(); }
            return profitFromTheApp;
        }

        // PUT: api/ProfitFromTheApps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfitFromTheApp(int id, ProfitFromTheApp profitFromTheApp)
        {
            if (id != profitFromTheApp.id)
            {
                return BadRequest();
            }

            _context.Entry(profitFromTheApp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfitFromTheAppExists(id))
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

        // POST: api/ProfitFromTheApps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProfitFromTheApp>> PostProfitFromTheApp(ProfitFromTheApp profitFromTheApp)
        {
            _context.ProfitFromTheApp.Add(profitFromTheApp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfitFromTheApp", new { id = profitFromTheApp.id }, profitFromTheApp);
        }

        // DELETE: api/ProfitFromTheApps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfitFromTheApp>> DeleteProfitFromTheApp(int id)
        {
            var profitFromTheApp = await _context.ProfitFromTheApp.FindAsync(id);
            if (profitFromTheApp == null)
            {
                return NotFound();
            }

            _context.ProfitFromTheApp.Remove(profitFromTheApp);
            await _context.SaveChangesAsync();

            return profitFromTheApp;
        }

        private bool ProfitFromTheAppExists(int id)
        {
            return _context.ProfitFromTheApp.Any(e => e.id == id);
        }
    }
}
