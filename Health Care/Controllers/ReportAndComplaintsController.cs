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
    public class ReportAndComplaintsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ReportAndComplaintsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ReportAndComplaints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportAndComplaint>>> GetReportAndComplaint()
        {
            return await _context.ReportAndComplaint.ToListAsync();
        }

        // GET: api/ReportAndComplaints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportAndComplaint>> GetReportAndComplaint(int id)
        {
            var reportAndComplaint = await _context.ReportAndComplaint.FindAsync(id);

            if (reportAndComplaint == null)
            {
                return NotFound();
            }

            return reportAndComplaint;
        }

        // PUT: api/ReportAndComplaints/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportAndComplaint(int id, ReportAndComplaint reportAndComplaint)
        {
            if (id != reportAndComplaint.id)
            {
                return BadRequest();
            }

            _context.Entry(reportAndComplaint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportAndComplaintExists(id))
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

        // POST: api/ReportAndComplaints
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReportAndComplaint>> PostReportAndComplaint(ReportAndComplaint reportAndComplaint)
        {
            _context.ReportAndComplaint.Add(reportAndComplaint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportAndComplaint", new { id = reportAndComplaint.id }, reportAndComplaint);
        }

        // DELETE: api/ReportAndComplaints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReportAndComplaint>> DeleteReportAndComplaint(int id)
        {
            var reportAndComplaint = await _context.ReportAndComplaint.FindAsync(id);
            if (reportAndComplaint == null)
            {
                return NotFound();
            }

            _context.ReportAndComplaint.Remove(reportAndComplaint);
            await _context.SaveChangesAsync();

            return reportAndComplaint;
        }

        private bool ReportAndComplaintExists(int id)
        {
            return _context.ReportAndComplaint.Any(e => e.id == id);
        }
    }
}
