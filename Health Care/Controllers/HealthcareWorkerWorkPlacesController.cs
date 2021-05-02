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
    public class HealthcareWorkerWorkPlacesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthcareWorkerWorkPlacesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthcareWorkerWorkPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorkerWorkPlace>>> GetHealthcareWorkerWorkPlace()
        {
            return await _context.HealthcareWorkerWorkPlace.ToListAsync();
        }

        // GET: api/HealthcareWorkerWorkPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthcareWorkerWorkPlace>> GetHealthcareWorkerWorkPlace(int id)
        {
            var healthcareWorkerWorkPlace = await _context.HealthcareWorkerWorkPlace.FindAsync(id);

            if (healthcareWorkerWorkPlace == null)
            {
                return NotFound();
            }

            return healthcareWorkerWorkPlace;
        }

        // PUT: api/HealthcareWorkerWorkPlaces/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthcareWorkerWorkPlace(int id, HealthcareWorkerWorkPlace healthcareWorkerWorkPlace)
        {
            if (id != healthcareWorkerWorkPlace.id)
            {
                return BadRequest();
            }

            _context.Entry(healthcareWorkerWorkPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareWorkerWorkPlaceExists(id))
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

        // POST: api/HealthcareWorkerWorkPlaces
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthcareWorkerWorkPlace>> PostHealthcareWorkerWorkPlace(HealthcareWorkerWorkPlace healthcareWorkerWorkPlace)
        {
            _context.HealthcareWorkerWorkPlace.Add(healthcareWorkerWorkPlace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorkerWorkPlace", new { id = healthcareWorkerWorkPlace.id }, healthcareWorkerWorkPlace);
        }

        // DELETE: api/HealthcareWorkerWorkPlaces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthcareWorkerWorkPlace>> DeleteHealthcareWorkerWorkPlace(int id)
        {
            var healthcareWorkerWorkPlace = await _context.HealthcareWorkerWorkPlace.FindAsync(id);
            if (healthcareWorkerWorkPlace == null)
            {
                return NotFound();
            }

            _context.HealthcareWorkerWorkPlace.Remove(healthcareWorkerWorkPlace);
            await _context.SaveChangesAsync();

            return healthcareWorkerWorkPlace;
        }

        private bool HealthcareWorkerWorkPlaceExists(int id)
        {
            return _context.HealthcareWorkerWorkPlace.Any(e => e.id == id);
        }
    }
}
