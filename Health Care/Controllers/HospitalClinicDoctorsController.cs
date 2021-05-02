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
    public class HospitalClinicDoctorsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HospitalClinicDoctorsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HospitalClinicDoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalClinicDoctor>>> GetHospitalClinicDoctor()
        {
            return await _context.HospitalClinicDoctor.ToListAsync();
        }

        // GET: api/HospitalClinicDoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalClinicDoctor>> GetHospitalClinicDoctor(int id)
        {
            var hospitalClinicDoctor = await _context.HospitalClinicDoctor.FindAsync(id);

            if (hospitalClinicDoctor == null)
            {
                return NotFound();
            }

            return hospitalClinicDoctor;
        }

        // PUT: api/HospitalClinicDoctors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalClinicDoctor(int id, HospitalClinicDoctor hospitalClinicDoctor)
        {
            if (id != hospitalClinicDoctor.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalClinicDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalClinicDoctorExists(id))
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

        // POST: api/HospitalClinicDoctors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalClinicDoctor>> PostHospitalClinicDoctor(HospitalClinicDoctor hospitalClinicDoctor)
        {
            _context.HospitalClinicDoctor.Add(hospitalClinicDoctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalClinicDoctor", new { id = hospitalClinicDoctor.id }, hospitalClinicDoctor);
        }

        // DELETE: api/HospitalClinicDoctors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalClinicDoctor>> DeleteHospitalClinicDoctor(int id)
        {
            var hospitalClinicDoctor = await _context.HospitalClinicDoctor.FindAsync(id);
            if (hospitalClinicDoctor == null)
            {
                return NotFound();
            }

            _context.HospitalClinicDoctor.Remove(hospitalClinicDoctor);
            await _context.SaveChangesAsync();

            return hospitalClinicDoctor;
        }

        private bool HospitalClinicDoctorExists(int id)
        {
            return _context.HospitalClinicDoctor.Any(e => e.id == id);
        }
    }
}
