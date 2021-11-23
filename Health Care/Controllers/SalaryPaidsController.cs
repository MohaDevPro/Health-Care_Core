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
    [Authorize(Roles = "admin")]

    public class SalaryPaidsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public SalaryPaidsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/SalaryPaids
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryPaid>>> GetSalaryPaid()
        {
            return await _context.SalaryPaid.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SalaryPaid>>> GetSalaryPaidByUserID(int id)
        {
            return await _context.SalaryPaid.Where(x=>x.userID == id).ToListAsync();
        }

        // GET: api/SalaryPaids/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryPaid>> GetSalaryPaid(int id)
        {
            var salaryPaid = await _context.SalaryPaid.FindAsync(id);

            if (salaryPaid == null)
            {
                return NotFound();
            }

            return salaryPaid;
        }

        // PUT: api/SalaryPaids/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryPaid(int id, SalaryPaid salaryPaid)
        {
            if (id != salaryPaid.Id)
            {
                return BadRequest();
            }

            _context.Entry(salaryPaid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryPaidExists(id))
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

        // POST: api/SalaryPaids
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SalaryPaid>> PostSalaryPaid(SalaryPaid salaryPaid)
        {
            salaryPaid.DateOfPay = DateTime.Now.ToString("yyyy/MM/dd hh:mm tt");
            _context.SalaryPaid.Add(salaryPaid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalaryPaid", new { id = salaryPaid.Id }, salaryPaid);
        }

        // DELETE: api/SalaryPaids/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalaryPaid>> DeleteSalaryPaid(int id)
        {
            var salaryPaid = await _context.SalaryPaid.FindAsync(id);
            if (salaryPaid == null)
            {
                return NotFound();
            }

            _context.SalaryPaid.Remove(salaryPaid);
            await _context.SaveChangesAsync();

            return salaryPaid;
        }

        private bool SalaryPaidExists(int id)
        {
            return _context.SalaryPaid.Any(e => e.Id == id);
        }
    }
}
