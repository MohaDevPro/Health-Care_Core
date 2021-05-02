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
    public class WorkerAppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public WorkerAppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/WorkerAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerAppointment>>> GetWorkerAppointment()
        {
            return await _context.WorkerAppointment.ToListAsync();
        }

        // GET: api/WorkerAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerAppointment>> GetWorkerAppointment(int id)
        {
            var workerAppointment = await _context.WorkerAppointment.FindAsync(id);

            if (workerAppointment == null)
            {
                return NotFound();
            }

            return workerAppointment;
        }

        // PUT: api/WorkerAppointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerAppointment(int id, WorkerAppointment workerAppointment)
        {
            if (id != workerAppointment.id)
            {
                return BadRequest();
            }

            _context.Entry(workerAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerAppointmentExists(id))
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

        // POST: api/WorkerAppointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkerAppointment>> PostWorkerAppointment(WorkerAppointment workerAppointment)
        {
            _context.WorkerAppointment.Add(workerAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerAppointment", new { id = workerAppointment.id }, workerAppointment);
        }

        // DELETE: api/WorkerAppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkerAppointment>> DeleteWorkerAppointment(int id)
        {
            var workerAppointment = await _context.WorkerAppointment.FindAsync(id);
            if (workerAppointment == null)
            {
                return NotFound();
            }

            _context.WorkerAppointment.Remove(workerAppointment);
            await _context.SaveChangesAsync();

            return workerAppointment;
        }

        private bool WorkerAppointmentExists(int id)
        {
            return _context.WorkerAppointment.Any(e => e.id == id);
        }
    }
}
