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

    public class ContractsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ContractsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContract()
        {
            return await _context.Contract.Where(s => s.active == true).Include(c=>c.ContractTerms).ToListAsync();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _context.Contract.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            return contract;
        }
        [Authorize(Roles = "admin,عامل صحي,عيادة,مستشفى,دكتور")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContractBasedOnRole(int id)
        {
            var contract = await _context.Contract.Where(s => s.active == true).Include(c=>c.ContractTerms).Where(c=>c.contractFor==id).FirstOrDefaultAsync();

            if (contract == null)
            {
                return NotFound();
            }

            return contract;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetDisabled()
        {
            return await _context.Contract.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> RestoreService(List<Contract> contract)
        {
            if (contract.Count == 0)
                return NoContent();

            try
            {
                foreach (Contract item in contract)
                {
                    var s = _context.Contract.Where(s => s.id == item.id).FirstOrDefault();
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

        // PUT: api/Contracts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, Contract contract)
        {
            if (id != contract.id)
            {
                return BadRequest();
            }
            Contract contract1 = _context.Contract.Where(c => c.id == id).Include(c=>c.ContractTerms).FirstOrDefault();
            contract1.contractFor = contract.contractFor;
            contract1.ContractTerms = contract.ContractTerms;
            _context.Entry(contract1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            _context.Contract.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = contract.id }, contract);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contract>> DeleteContract(int id)
        {
            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            contract.active = false;
            //_context.Contract.Remove(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.id == id);
        }
    }
}
