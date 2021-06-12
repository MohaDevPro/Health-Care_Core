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
    public class UserContractsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public UserContractsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/UserContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContract>>> GetUserContract()
        {
            return await _context.UserContract.ToListAsync();
        }

        // GET: api/UserContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserContract>> GetUserContract(int id)
        {
            var userContract = await _context.UserContract.FindAsync(id);

            if (userContract == null)
            {
                return NotFound();
            }

            return userContract;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<bool>> GetIsUserAcceptContract(int id)
        {
            var userContract = await _context.UserContract.Where(u=>u.userId == id).FirstOrDefaultAsync();

            if (userContract == null)
            {
                return false;
            }
            if (Convert.ToDateTime(userContract.contractEndDate) < Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")))
            {
                return false;
            }
            return true;
        }
        // PUT: api/UserContracts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserContract(int id, UserContract userContract)
        {
            if (id != userContract.id)
            {
                return BadRequest();
            }

            _context.Entry(userContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContractExists(id))
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

        // POST: api/UserContracts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserContract>> PostUserContract(UserContract userContract)
        {
            _context.UserContract.Add(userContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserContract", new { id = userContract.id }, userContract);
        }

        // DELETE: api/UserContracts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserContract>> DeleteUserContract(int id)
        {
            var userContract = await _context.UserContract.FindAsync(id);
            if (userContract == null)
            {
                return NotFound();
            }

            _context.UserContract.Remove(userContract);
            await _context.SaveChangesAsync();

            return userContract;
        }

        private bool UserContractExists(int id)
        {
            return _context.UserContract.Any(e => e.id == id);
        }
    }
}
