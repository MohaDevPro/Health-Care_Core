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
    public class MedicalAdvicesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public MedicalAdvicesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/MedicalAdvices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalAdvice>>> GetMedicalAdvice()
        {
            return await _context.MedicalAdvice.ToListAsync();
        }

        // GET: api/MedicalAdvices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalAdvice>> GetMedicalAdvice(int id)
        {
            var medicalAdvice = await _context.MedicalAdvice.FindAsync(id);

            if (medicalAdvice == null)
            {
                return NotFound();
            }

            return medicalAdvice;
        }

        // PUT: api/MedicalAdvices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalAdvice(int id, MedicalAdvice medicalAdvice)
        {
            if (id != medicalAdvice.id)
            {
                return BadRequest();
            }

            _context.Entry(medicalAdvice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalAdviceExists(id))
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

        // POST: api/MedicalAdvices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MedicalAdvice>> PostMedicalAdvice(MedicalAdvice medicalAdvice)
        {
            _context.MedicalAdvice.Add(medicalAdvice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalAdvice", new { id = medicalAdvice.id }, medicalAdvice);
        }

        // DELETE: api/MedicalAdvices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalAdvice>> DeleteMedicalAdvice(int id)
        {
            var medicalAdvice = await _context.MedicalAdvice.FindAsync(id);
            if (medicalAdvice == null)
            {
                return NotFound();
            }

            _context.MedicalAdvice.Remove(medicalAdvice);
            await _context.SaveChangesAsync();

            return medicalAdvice;
        }

        private bool MedicalAdviceExists(int id)
        {
            return _context.MedicalAdvice.Any(e => e.id == id);
        }
    }
}
