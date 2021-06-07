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
    public class HealthCareWorkerAppWorkTimesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthCareWorkerAppWorkTimesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthCareWorkerAppWorkTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthCareWorkerAppWorkTime>>> GetHealthCareWorkerAppWorkTime()
        {
            return await _context.HealthCareWorkerAppWorkTime.ToListAsync();
        }

        // GET: api/HealthCareWorkerAppWorkTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> GetHealthCareWorkerAppWorkTime(int id)
        {
            var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.FindAsync(id);
            if (healthCareWorkerAppWorkTime == null)
            {
                return NotFound();
            }
            return healthCareWorkerAppWorkTime;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HealthCareWorkerAppWorkTime>>> GetHealthCareWorkerAppWorkTimeBasedonUserId(int id)
        {
            var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.Where(x=>x.userId==id).ToListAsync();
            
            return healthCareWorkerAppWorkTime;
        }

        // PUT: api/HealthCareWorkerAppWorkTimes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthCareWorkerAppWorkTime(int id, HealthCareWorkerAppWorkTime healthCareWorkerAppWorkTime)
        {
            if (id != healthCareWorkerAppWorkTime.id)
            {
                return BadRequest();
            }

            _context.Entry(healthCareWorkerAppWorkTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthCareWorkerAppWorkTimeExists(id))
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

        // POST: api/HealthCareWorkerAppWorkTimes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> PostHealthCareWorkerAppWorkTime(HealthCareWorkerAppWorkTime healthCareWorkerAppWorkTime)
        {
            _context.HealthCareWorkerAppWorkTime.Add(healthCareWorkerAppWorkTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthCareWorkerAppWorkTime", new { id = healthCareWorkerAppWorkTime.id }, healthCareWorkerAppWorkTime);
        }

        // DELETE: api/HealthCareWorkerAppWorkTimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> DeleteHealthCareWorkerAppWorkTime(int id)
        {
            var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.FindAsync(id);
            if (healthCareWorkerAppWorkTime == null)
            {
                return NotFound();
            }

            _context.HealthCareWorkerAppWorkTime.Remove(healthCareWorkerAppWorkTime);
            await _context.SaveChangesAsync();

            return healthCareWorkerAppWorkTime;
        }

        private bool HealthCareWorkerAppWorkTimeExists(int id)
        {
            return _context.HealthCareWorkerAppWorkTime.Any(e => e.id == id);
        }
    }
}
