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
    public class WorkerSalariesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public WorkerSalariesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/WorkerSalaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerSalary>>> GetWorkerSalary()
        {
            return await _context.WorkerSalary.ToListAsync();
        }

        // GET: api/WorkerSalaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerSalary>> GetWorkerSalary(int id)
        {
            var workerSalary = await _context.WorkerSalary.FindAsync(id);

            if (workerSalary == null)
            {
                return NotFound();
            }

            return workerSalary;
        }

        // PUT: api/WorkerSalaries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerSalary(int id, WorkerSalary workerSalary)
        {
            if (id != workerSalary.id)
            {
                return BadRequest();
            }

            _context.Entry(workerSalary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerSalaryExists(id))
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

        // POST: api/WorkerSalaries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkerSalary>> PostWorkerSalary(WorkerSalary workerSalary)
        {
            _context.WorkerSalary.Add(workerSalary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerSalary", new { id = workerSalary.id }, workerSalary);
        }

        // DELETE: api/WorkerSalaries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkerSalary>> DeleteWorkerSalary(int id)
        {
            var workerSalary = await _context.WorkerSalary.FindAsync(id);
            if (workerSalary == null)
            {
                return NotFound();
            }

            _context.WorkerSalary.Remove(workerSalary);
            await _context.SaveChangesAsync();

            return workerSalary;
        }

        private bool WorkerSalaryExists(int id)
        {
            return _context.WorkerSalary.Any(e => e.id == id);
        }
    }
}
