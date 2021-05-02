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
    public class ExternalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ExternalClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ExternalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExternalClinic>>> GetExternalClinic()
        {
            return await _context.ExternalClinic.ToListAsync();
        }

        // GET: api/ExternalClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalClinic>> GetExternalClinic(int id)
        {
            var externalClinic = await _context.ExternalClinic.FindAsync(id);

            if (externalClinic == null)
            {
                return NotFound();
            }

            return externalClinic;
        }

        // PUT: api/ExternalClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternalClinic(int id, ExternalClinic externalClinic)
        {
            if (id != externalClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(externalClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalClinicExists(id))
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

        // POST: api/ExternalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExternalClinic>> PostExternalClinic(ExternalClinic externalClinic)
        {
            _context.ExternalClinic.Add(externalClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExternalClinic", new { id = externalClinic.id }, externalClinic);
        }

        // DELETE: api/ExternalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternalClinic>> DeleteExternalClinic(int id)
        {
            var externalClinic = await _context.ExternalClinic.FindAsync(id);
            if (externalClinic == null)
            {
                return NotFound();
            }

            _context.ExternalClinic.Remove(externalClinic);
            await _context.SaveChangesAsync();

            return externalClinic;
        }

        private bool ExternalClinicExists(int id)
        {
            return _context.ExternalClinic.Any(e => e.id == id);
        }
    }
}
