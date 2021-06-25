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
    public class SpecialityHealthWorkersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public SpecialityHealthWorkersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/SpecialityHealthWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityHealthWorker>>> GetSpecialityHealthWorker()
        {
            return await _context.SpecialityHealthWorker.Where(s=>s.active == true).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityHealthWorker>>> GetDisabled()
        {
            return await _context.SpecialityHealthWorker.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<SpecialityHealthWorker> specialityHealthWorker)
        {
            if (specialityHealthWorker.Count == 0)
                return NoContent();

            try
            {
                foreach (SpecialityHealthWorker item in specialityHealthWorker)
                {
                    var s = _context.SpecialityHealthWorker.Where(s => s.id == item.id).FirstOrDefault();
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

        // GET: api/SpecialityHealthWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialityHealthWorker>> GetSpecialityHealthWorker(int id)
        {
            var specialityHealthWorker = await _context.SpecialityHealthWorker.FindAsync(id);

            if (specialityHealthWorker == null)
            {
                return NotFound();
            }

            return specialityHealthWorker;
        }

        // PUT: api/SpecialityHealthWorkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialityHealthWorker(int id, SpecialityHealthWorker specialityHealthWorker)
        {
            if (id != specialityHealthWorker.id)
            {
                return BadRequest();
            }

            _context.Entry(specialityHealthWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialityHealthWorkerExists(id))
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

        // POST: api/SpecialityHealthWorkers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SpecialityHealthWorker>> PostSpecialityHealthWorker(SpecialityHealthWorker specialityHealthWorker)
        {
            _context.SpecialityHealthWorker.Add(specialityHealthWorker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialityHealthWorker", new { id = specialityHealthWorker.id }, specialityHealthWorker);
        }

        // DELETE: api/SpecialityHealthWorkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpecialityHealthWorker>> DeleteSpecialityHealthWorker(int id)
        {
            var specialityHealthWorker = await _context.SpecialityHealthWorker.FindAsync(id);
            if (specialityHealthWorker == null)
            {
                return NotFound();
            }
            specialityHealthWorker.active = false;
            //_context.SpecialityHealthWorker.Remove(specialityHealthWorker);
            await _context.SaveChangesAsync();

            return specialityHealthWorker;
        }

        private bool SpecialityHealthWorkerExists(int id)
        {
            return _context.SpecialityHealthWorker.Any(e => e.id == id);
        }
    }
}
