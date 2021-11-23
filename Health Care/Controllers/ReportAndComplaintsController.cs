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
    [Authorize]
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

        [HttpGet("{userId}")] //for patient , clinic ......
        public async Task<ActionResult<ReportAndComplaintViewModel>> GetReportAndComplaintByUserId(int userId)
        {
            List<ReportAndComplaint> answeredReportAndComplaints = await _context.ReportAndComplaint.Where(x => x.userId == userId && x.isAnswered ).OrderByDescending(x=>x.ReportAndComplaintDate).ToListAsync();
            List<ReportAndComplaint> unAnsweredReportAndComplaints = await _context.ReportAndComplaint.Where(x => x.userId == userId && x.isAnswered==false ).ToListAsync();
            ReportAndComplaintViewModel x = new ReportAndComplaintViewModel()
            {
                AnsweredReportAndComplaint = answeredReportAndComplaints,
                UnAnsweredReportAndComplaint = unAnsweredReportAndComplaints,
            };
            return x ;
        }
        [Authorize(Roles = "admin")]

        [HttpGet("{pageKey}/{pageSize}")] //for patient , clinic ......
        public async Task<ActionResult<List<ReportAndComplaint>>> GetReportAndComplaintForAdmin(int pageKey, int pageSize)
        {
            //List<ReportAndComplaint> answeredReportAndComplaints = await _context.ReportAndComplaint.Where(x => x.isAnswered).ToListAsync();
            //List<ReportAndComplaint> unAnsweredReportAndComplaints = await _context.ReportAndComplaint.Where(x => x.isAnswered == false).OrderByDescending(x => x.ReportAndComplaintDate).ToListAsync();
            //ReportAndComplaintViewModel x = new ReportAndComplaintViewModel()
            //{
            //    AnsweredReportAndComplaint = answeredReportAndComplaints,
            //    UnAnsweredReportAndComplaint = unAnsweredReportAndComplaints,
            //};
            var reportAndComplaints = await _context.ReportAndComplaint.OrderByDescending(x => x.id).ToListAsync();
            if (pageSize != 0)
                return reportAndComplaints.Skip(pageKey).Take(pageSize).ToList();
            else return reportAndComplaints;
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
        [Authorize(Roles = "admin")]
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
