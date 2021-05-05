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
    public class HospitalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HospitalClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HospitalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHospitalClinic()
        {
            return await (from hospital in _context.HospitalClinic
                          select new
                          {
                              id = hospital.hospitalId,
                              Name = hospital.Name,
                              Picture = hospital.Picture,
                              Description=hospital.Description

                          }

                          ).ToListAsync();
        }

        // GET: api/HospitalClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalClinic>> GetHospitalClinic(int id)
        {
            var hospitalClinic = await _context.HospitalClinic.FindAsync(id);

            if (hospitalClinic == null)
            {
                return NotFound();
            }

            return hospitalClinic;
        }

        // PUT: api/HospitalClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalClinic(int id, HospitalClinic hospitalClinic)
        {
            if (id != hospitalClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalClinicExists(id))
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

        // POST: api/HospitalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalClinic>> PostHospitalClinic(HospitalClinic hospitalClinic)
        {
            _context.HospitalClinic.Add(hospitalClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalClinic", new { id = hospitalClinic.id }, hospitalClinic);
        }

        // DELETE: api/HospitalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalClinic>> DeleteHospitalClinic(int id)
        {
            var hospitalClinic = await _context.HospitalClinic.FindAsync(id);
            if (hospitalClinic == null)
            {
                return NotFound();
            }

            _context.HospitalClinic.Remove(hospitalClinic);
            await _context.SaveChangesAsync();

            return hospitalClinic;
        }

        private bool HospitalClinicExists(int id)
        {
            return _context.HospitalClinic.Any(e => e.id == id);
        }
    }
}
