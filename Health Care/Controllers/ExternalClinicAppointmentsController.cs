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
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalClinicAppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ExternalClinicAppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ExternalClinicAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExternalClinicAppointment>>> GetExternalClinicAppointment()
        {
            return await _context.ExternalClinicAppointment.ToListAsync();
        }

        // GET: api/ExternalClinicAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalClinicAppointment>> GetExternalClinicAppointment(int id)
        {
            var externalClinicAppointment = await _context.ExternalClinicAppointment.FindAsync(id);

            if (externalClinicAppointment == null)
            {
                return NotFound();
            }

            return externalClinicAppointment;
        }

        // PUT: api/ExternalClinicAppointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternalClinicAppointment(int id, ExternalClinicAppointment externalClinicAppointment)
        {
            if (id != externalClinicAppointment.id)
            {
                return BadRequest();
            }

            _context.Entry(externalClinicAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalClinicAppointmentExists(id))
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

        // POST: api/ExternalClinicAppointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExternalClinicAppointment>> PostExternalClinicAppointment(ExternalClinicAppointment externalClinicAppointment)
        {
            _context.ExternalClinicAppointment.Add(externalClinicAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExternalClinicAppointment", new { id = externalClinicAppointment.id }, externalClinicAppointment);
        }

        // DELETE: api/ExternalClinicAppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternalClinicAppointment>> DeleteExternalClinicAppointment(int id)
        {
            var externalClinicAppointment = await _context.ExternalClinicAppointment.FindAsync(id);
            if (externalClinicAppointment == null)
            {
                return NotFound();
            }

            _context.ExternalClinicAppointment.Remove(externalClinicAppointment);
            await _context.SaveChangesAsync();

            return externalClinicAppointment;
        }

        private bool ExternalClinicAppointmentExists(int id)
        {
            return _context.ExternalClinicAppointment.Any(e => e.id == id);
        }
    }
}
