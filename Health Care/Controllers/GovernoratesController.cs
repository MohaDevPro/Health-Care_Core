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
    public class GovernoratesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public GovernoratesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Governorates
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Governorate>>> GetGovernorate()
        {
            return await _context.Governorate.Where(x=> x.active == true).ToListAsync();
        }

        // GET: api/Governorates/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Governorate>> GetGovernorate(int id)
        {
            var governorate = await _context.Governorate.FindAsync(id);

            if (governorate == null)
            {
                return NotFound();
            }

            return governorate;
        }
        public async Task<ActionResult<IEnumerable<Governorate>>> GetDisabled()
        {
            return await _context.Governorate.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> RestoreService(List<Governorate> governorate)
        {
            if (governorate.Count == 0)
                return NoContent();

            try
            {
                foreach (Governorate item in governorate)
                {
                    var s = _context.Governorate.Where(s => s.ID == item.ID).FirstOrDefault();
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

        // PUT: api/Governorates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGovernorate(int id, Governorate governorate)
        {
            if (id != governorate.ID)
            {
                return BadRequest();
            }

            _context.Entry(governorate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GovernorateExists(id))
                {
                    return NotFound();
                }
                else
                {
                }
            }

            return NoContent();
        }

        // POST: api/Governorates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Governorate>> PostGovernorate(Governorate governorate)
        {
            _context.Governorate.Add(governorate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGovernorate", new { id = governorate.ID }, governorate);
        }

        // DELETE: api/Governorates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Governorate>> DeleteGovernorate(int id)
        {
            var governorate = await _context.Governorate.FindAsync(id);
            if (governorate == null)
            {
                return NotFound();
            }
            governorate.active = false;
            //_context.Governorate.Remove(governorate);
            await _context.SaveChangesAsync();

            return governorate;
        }

        private bool GovernorateExists(int id)
        {
            return _context.Governorate.Any(e => e.ID == id);
        }
    }
}
