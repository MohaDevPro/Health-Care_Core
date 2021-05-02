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
    public class AppWorktimesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppWorktimesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/AppWorktimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppWorktime>>> GetAppWorktime()
        {
            return await _context.AppWorktime.ToListAsync();
        }

        // GET: api/AppWorktimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppWorktime>> GetAppWorktime(int id)
        {
            var appWorktime = await _context.AppWorktime.FindAsync(id);

            if (appWorktime == null)
            {
                return NotFound();
            }

            return appWorktime;
        }

        // PUT: api/AppWorktimes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppWorktime(int id, AppWorktime appWorktime)
        {
            if (id != appWorktime.id)
            {
                return BadRequest();
            }

            _context.Entry(appWorktime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppWorktimeExists(id))
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

        // POST: api/AppWorktimes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppWorktime>> PostAppWorktime(AppWorktime appWorktime)
        {
            _context.AppWorktime.Add(appWorktime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppWorktime", new { id = appWorktime.id }, appWorktime);
        }

        // DELETE: api/AppWorktimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppWorktime>> DeleteAppWorktime(int id)
        {
            var appWorktime = await _context.AppWorktime.FindAsync(id);
            if (appWorktime == null)
            {
                return NotFound();
            }

            _context.AppWorktime.Remove(appWorktime);
            await _context.SaveChangesAsync();

            return appWorktime;
        }

        private bool AppWorktimeExists(int id)
        {
            return _context.AppWorktime.Any(e => e.id == id);
        }
    }
}
