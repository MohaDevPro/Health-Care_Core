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
    public class ChargeOrRechargeRequestsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ChargeOrRechargeRequestsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ChargeOrRechargeRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChargeOrRechargeRequest>>> GetChargeOrRechargeRequest()
        {
            return await _context.ChargeOrRechargeRequest.ToListAsync();
        }

        // GET: api/ChargeOrRechargeRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeOrRechargeRequest>> GetChargeOrRechargeRequest(int id)
        {
            var chargeOrRechargeRequest = await _context.ChargeOrRechargeRequest.FindAsync(id);

            if (chargeOrRechargeRequest == null)
            {
                return NotFound();
            }

            return chargeOrRechargeRequest;
        }

        // PUT: api/ChargeOrRechargeRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChargeOrRechargeRequest(int id, ChargeOrRechargeRequest chargeOrRechargeRequest)
        {
            if (id != chargeOrRechargeRequest.id)
            {
                return BadRequest();
            }

            _context.Entry(chargeOrRechargeRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargeOrRechargeRequestExists(id))
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

        // POST: api/ChargeOrRechargeRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ChargeOrRechargeRequest>> PostChargeOrRechargeRequest(ChargeOrRechargeRequest chargeOrRechargeRequest)
        {
            _context.ChargeOrRechargeRequest.Add(chargeOrRechargeRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChargeOrRechargeRequest", new { id = chargeOrRechargeRequest.id }, chargeOrRechargeRequest);
        }

        // DELETE: api/ChargeOrRechargeRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ChargeOrRechargeRequest>> DeleteChargeOrRechargeRequest(int id)
        {
            var chargeOrRechargeRequest = await _context.ChargeOrRechargeRequest.FindAsync(id);
            if (chargeOrRechargeRequest == null)
            {
                return NotFound();
            }

            _context.ChargeOrRechargeRequest.Remove(chargeOrRechargeRequest);
            await _context.SaveChangesAsync();

            return chargeOrRechargeRequest;
        }

        private bool ChargeOrRechargeRequestExists(int id)
        {
            return _context.ChargeOrRechargeRequest.Any(e => e.id == id);
        }
    }
}
