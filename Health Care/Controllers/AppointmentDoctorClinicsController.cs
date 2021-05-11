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
    public class AppointmentDoctorClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppointmentDoctorClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentDoctorClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDoctorClinic>>> GetAppointmentDoctorClinic()
        {
            return await _context.AppointmentDoctorClinic.ToListAsync();
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> GetAppointmentDoctorClinicBasedOnDate(string month, string day, string year,int clinicId , int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FirstOrDefaultAsync(x=>x.appointmentDate== searchDate
            &&x.clinicId==clinicId&&x.doctorId==doctorId);

            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }
            return appointmentDoctorClinic;
        }

        // GET: api/AppointmentDoctorClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> GetAppointmentDoctorClinic(int id)
        {
            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FindAsync(id);

            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }

            return appointmentDoctorClinic;
        }

        // PUT: api/AppointmentDoctorClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentDoctorClinic(int id, AppointmentDoctorClinic appointmentDoctorClinic)
        {
            if (id != appointmentDoctorClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(appointmentDoctorClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentDoctorClinicExists(id))
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

        // POST: api/AppointmentDoctorClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppointmentDoctorClinic>> PostAppointmentDoctorClinic(AppointmentDoctorClinic appointmentDoctorClinic)
        {
            _context.AppointmentDoctorClinic.Add(appointmentDoctorClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentDoctorClinic", new { id = appointmentDoctorClinic.id }, appointmentDoctorClinic);
        }

        // DELETE: api/AppointmentDoctorClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> DeleteAppointmentDoctorClinic(int id)
        {
            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FindAsync(id);
            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }

            _context.AppointmentDoctorClinic.Remove(appointmentDoctorClinic);
            await _context.SaveChangesAsync();

            return appointmentDoctorClinic;
        }

        private bool AppointmentDoctorClinicExists(int id)
        {
            return _context.AppointmentDoctorClinic.Any(e => e.id == id);
        }
    }
}
