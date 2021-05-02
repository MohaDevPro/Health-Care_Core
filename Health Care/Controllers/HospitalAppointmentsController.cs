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
    public class HospitalAppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HospitalAppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAppointment>>> GetHospitalAppointment()
        {
            return await _context.HospitalAppointment.ToListAsync();
        }

        // GET: api/HospitalAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAppointment>> GetHospitalAppointment(int id)
        {
            var hospitalAppointment = await _context.HospitalAppointment.FindAsync(id);

            if (hospitalAppointment == null)
            {
                return NotFound();
            }

            return hospitalAppointment;
        }

        // PUT: api/HospitalAppointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAppointment(int id, HospitalAppointment hospitalAppointment)
        {
            if (id != hospitalAppointment.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAppointmentExists(id))
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

        // POST: api/HospitalAppointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAppointment>> PostHospitalAppointment(HospitalAppointment hospitalAppointment)
        {
            _context.HospitalAppointment.Add(hospitalAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAppointment", new { id = hospitalAppointment.id }, hospitalAppointment);
        }

        // DELETE: api/HospitalAppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAppointment>> DeleteHospitalAppointment(int id)
        {
            var hospitalAppointment = await _context.HospitalAppointment.FindAsync(id);
            if (hospitalAppointment == null)
            {
                return NotFound();
            }

            _context.HospitalAppointment.Remove(hospitalAppointment);
            await _context.SaveChangesAsync();

            return hospitalAppointment;
        }

        private bool HospitalAppointmentExists(int id)
        {
            return _context.HospitalAppointment.Any(e => e.id == id);
        }
    }
}
