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
    public class ClinicTypesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ClinicTypesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ClinicTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicType>>> GetClinicType()
        {
            return await _context.ClinicType.Where(c=>c.active==true).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicType>>> GetDisabled()
        {
            return await _context.ClinicType.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<ClinicType> clinicType)
        {
            if (clinicType.Count == 0)
                return NoContent();

            try
            {
                foreach (ClinicType item in clinicType)
                {
                    var s = _context.ClinicType.Where(s => s.id == item.id).FirstOrDefault();
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
        // GET: api/ClinicTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicType>> GetClinicType(int id)
        {
            var clinicType = await _context.ClinicType.FindAsync(id);

            if (clinicType == null)
            {
                return NotFound();
            }

            return clinicType;
        }

        // PUT: api/ClinicTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicType(int id, ClinicType clinicType)
        {
            if (id != clinicType.id)
            {
                return BadRequest();
            }

            _context.Entry(clinicType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicTypeExists(id))
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

        // POST: api/ClinicTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClinicType>> PostClinicType(ClinicType clinicType)
        {
            _context.ClinicType.Add(clinicType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinicType", new { id = clinicType.id }, clinicType);
        }

        // DELETE: api/ClinicTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClinicType>> DeleteClinicType(int id)
        {
            var clinicType = await _context.ClinicType.FindAsync(id);
            if (clinicType == null)
            {
                return NotFound();
            }
            clinicType.active = false;
            //_context.ClinicType.Remove(clinicType);
            await _context.SaveChangesAsync();

            return clinicType;
        }

        private bool ClinicTypeExists(int id)
        {
            return _context.ClinicType.Any(e => e.id == id);
        }
    }
}
