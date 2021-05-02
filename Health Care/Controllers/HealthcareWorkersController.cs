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
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthcareWorkersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthcareWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetHealthcareWorker()
        {
            return await _context.HealthcareWorker.ToListAsync();
        }

        // GET: api/HealthcareWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthcareWorker>> GetHealthcareWorker(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.FindAsync(id);

            if (healthcareWorker == null)
            {
                return NotFound();
            }

            return healthcareWorker;
        }

        // PUT: api/HealthcareWorkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthcareWorker(int id, HealthcareWorker healthcareWorker)
        {
            if (id != healthcareWorker.id)
            {
                return BadRequest();
            }

            _context.Entry(healthcareWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareWorkerExists(id))
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

        // POST: api/HealthcareWorkers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthcareWorker>> PostHealthcareWorker(HealthcareWorker healthcareWorker)
        {
            _context.HealthcareWorker.Add(healthcareWorker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
        }

        // DELETE: api/HealthcareWorkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthcareWorker>> DeleteHealthcareWorker(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.FindAsync(id);
            if (healthcareWorker == null)
            {
                return NotFound();
            }

            _context.HealthcareWorker.Remove(healthcareWorker);
            await _context.SaveChangesAsync();

            return healthcareWorker;
        }

        private bool HealthcareWorkerExists(int id)
        {
            return _context.HealthcareWorker.Any(e => e.id == id);
        }
    }
}
