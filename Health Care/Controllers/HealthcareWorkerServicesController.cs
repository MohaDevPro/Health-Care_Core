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
    public class HealthcareWorkerServicesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthcareWorkerServicesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthcareWorkerServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorkerService>>> GetHealthcareWorkerService()
        {
            return await _context.HealthcareWorkerService.ToListAsync();
        }

        // GET: api/HealthcareWorkerServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthcareWorkerService>> GetHealthcareWorkerService(int id)
        {
            var healthcareWorkerService = await _context.HealthcareWorkerService.FindAsync(id);

            if (healthcareWorkerService == null)
            {
                return NotFound();
            }

            return healthcareWorkerService;
        }

        
        // PUT: api/HealthcareWorkerServices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthcareWorkerService(int id, HealthcareWorkerService healthcareWorkerService)
        {
            if (id != healthcareWorkerService.id)
            {
                return BadRequest();
            }

            _context.Entry(healthcareWorkerService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareWorkerServiceExists(id))
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

        // POST: api/HealthcareWorkerServices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthcareWorkerService>> PostHealthcareWorkerService(HealthcareWorkerService healthcareWorkerService)
        {
            _context.HealthcareWorkerService.Add(healthcareWorkerService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorkerService", new { id = healthcareWorkerService.id }, healthcareWorkerService);
        }

        // DELETE: api/HealthcareWorkerServices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthcareWorkerService>> DeleteHealthcareWorkerService(int id)
        {
            var healthcareWorkerService = await _context.HealthcareWorkerService.FindAsync(id);
            if (healthcareWorkerService == null)
            {
                return NotFound();
            }

            _context.HealthcareWorkerService.Remove(healthcareWorkerService);
            await _context.SaveChangesAsync();

            return healthcareWorkerService;
        }

        private bool HealthcareWorkerServiceExists(int id)
        {
            return _context.HealthcareWorkerService.Any(e => e.id == id);
        }
    }
}
