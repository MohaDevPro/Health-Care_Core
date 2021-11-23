using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.AspNetCore.Authorization;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin, مريض ,عامل صحي")]
    public class HealthWorkerRequestByUsersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthWorkerRequestByUsersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthWorkerRequestByUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthWorkerRequestByUser>>> GetHealthWorkerRequestByUser()
        {
            return await _context.HealthWorkerRequestByUser.ToListAsync();
        }

        // GET: api/HealthWorkerRequestByUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthWorkerRequestByUser>> GetHealthWorkerRequestByUser(int id)
        {
            var healthWorkerRequestByUser = await _context.HealthWorkerRequestByUser.FindAsync(id);

            if (healthWorkerRequestByUser == null)
            {
                return NotFound();
            }

            return healthWorkerRequestByUser;
        }

        // PUT: api/HealthWorkerRequestByUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthWorkerRequestByUser(int id, HealthWorkerRequestByUser healthWorkerRequestByUser)
        {
            if (id != healthWorkerRequestByUser.id)
            {
                return BadRequest();
            }

            _context.Entry(healthWorkerRequestByUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthWorkerRequestByUserExists(id))
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

        // POST: api/HealthWorkerRequestByUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthWorkerRequestByUser>> PostHealthWorkerRequestByUser(HealthWorkerRequestByUser healthWorkerRequestByUser)
        {
            _context.HealthWorkerRequestByUser.Add(healthWorkerRequestByUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthWorkerRequestByUser", new { id = healthWorkerRequestByUser.id }, healthWorkerRequestByUser);
        }

        // DELETE: api/HealthWorkerRequestByUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHealthWorkerRequestByUser(int id)
        {
            var healthWorkerRequestByUser = await _context.HealthWorkerRequestByUser.FindAsync(id);
            if (healthWorkerRequestByUser == null)
            {
                return NotFound();
            }
            WorkerAppointment appointment = _context.WorkerAppointment.Where(x=>x.id == healthWorkerRequestByUser.appointmentId).FirstOrDefault();
            Patient patient = _context.Patient.Where(x=>x.userId == appointment.patientId).FirstOrDefault();
            patient.Balance += appointment.servicePrice;
            _context.HealthWorkerRequestByUser.Remove(healthWorkerRequestByUser);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        // DELETE: api/HealthWorkerRequestByUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthWorkerRequestByUser>> DeleteHealthWorkerRequestByUserByAppointmentID(int id)
        {
            var healthWorkerRequestByUser = _context.HealthWorkerRequestByUser.Where(a=>a.appointmentId == id).FirstOrDefault();
            if (healthWorkerRequestByUser == null)
            {
                return NotFound();
            }
            _context.HealthWorkerRequestByUser.Remove(healthWorkerRequestByUser);
            await _context.SaveChangesAsync();

            return healthWorkerRequestByUser;
        }

        private bool HealthWorkerRequestByUserExists(int id)
        {
            return _context.HealthWorkerRequestByUser.Any(e => e.id == id);
        }
    }
}
