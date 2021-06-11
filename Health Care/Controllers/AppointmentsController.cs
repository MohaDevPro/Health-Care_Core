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
    public class AppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointment()
        {
            return await _context.Appointment.Where(a => a.active == true).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDisabled()
        {
            return await _context.Appointment.Where(a=>a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<Appointment> appointement)
        {
            if (appointement.Count == 0)
                return NoContent();

            try
            {
                foreach (Appointment item in appointement)
                {
                    var s = _context.Appointment.Where(s => s.id == item.id).FirstOrDefault();
                    s.active = true;
                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: api/Appointments/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentBasedOnUserId(int userId)
        {
            var appointment = await _context.Appointment.Where(x=>x.userId == userId && x.active == true).ToListAsync();
            if (appointment == null) { return NotFound(); }
            return appointment;
        }
        [HttpGet("{appointmentDoctorClinicid}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentBasedOnappointmentDoctorClinicid(int appointmentDoctorClinicid)
        {
            var appointment = await _context.Appointment.Where(x => x.appointmentDoctorClinicId == appointmentDoctorClinicid+ "" && x.active == true).ToListAsync();
            if (appointment == null) { return NotFound(); }
            return appointment;
        }

        // GET: api/Appointments/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnStatusByUserId(int userId)
        {
            List <List<Appointment>> li = new List <List<Appointment>>();

            var ConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted==true && x.cancelledByUser == false && x.active == true).ToListAsync();
            var unConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted==false && x.cancelledByUser== false && x.active == true).ToListAsync();
            var cancelledAppointment = await _context.Appointment.Where(x => x.userId == userId && x.cancelledByUser== true && x.active == true).ToListAsync();
            
            li.Add(ConfirmedAppointment);
            li.Add(unConfirmedAppointment);
            li.Add(cancelledAppointment);
            
            if (li == null) { return NotFound(); }
            return li;
        }


        // GET: api/Appointments/5
        [HttpGet("{clinicId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnClinicId(int clinicId)
        {
            List<List<Appointment>> li = new List<List<Appointment>>();

            var ConfirmedAppointment = await _context.Appointment.Where(x => x.distnationClinicId == clinicId
            && x.Accepted == true && x.cancelledByUser == false && x.cancelledByClinicSecretary== false && x.active == true).OrderBy(x=> x.appointmentDate).ToListAsync();

            var unConfirmedAppointment = await _context.Appointment.Where(x => x.distnationClinicId == clinicId
            && x.Accepted == false && x.cancelledByUser == false && x.cancelledByClinicSecretary== false && x.active == true).OrderBy(x => x.appointmentDate).ToListAsync();

            var cancelledAppointmentByUser = await _context.Appointment.Where(x => x.distnationClinicId == clinicId
            && x.cancelledByUser == true && x.active == true).OrderBy(x => x.appointmentDate).ToListAsync();
            
            var cancelledAppointmentBySecretary = await _context.Appointment.Where(x => x.distnationClinicId == clinicId
            && x.cancelledByUser == false && x.cancelledByClinicSecretary == true && x.active == true).OrderBy(x => x.appointmentDate).ToListAsync();
            
            li.Add(ConfirmedAppointment);
            li.Add(unConfirmedAppointment);
            li.Add(cancelledAppointmentByUser);
            li.Add(cancelledAppointmentBySecretary);

            if (li == null) { return NotFound(); }
            return li;
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentBasedOnDate(string month, string day, string year, int clinicId, int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetConfirmedAppointmentBasedOnDate(string month, string day, string year, int clinicId, int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId 
                       && x.Accepted==true&& x.Paid == false && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentBasedOnDateToCancelbyClinic(string month, string day, string year, int clinicId, int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId 
                && x.cancelledByUser==false && x.cancelledByClinicSecretary== false && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.id }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment.active = false;
            //_context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.id == id);
        }
    }
}
