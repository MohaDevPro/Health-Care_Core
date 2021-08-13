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
    public class HospitalClinicAddressesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HospitalClinicAddressesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HospitalClinicAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalClinicAddress>>> GetHospitalClinicAddress()
        {
            return await _context.HospitalClinicAddress.ToListAsync();
        }

        // GET: api/HospitalClinicAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalClinicAddress>> GetHospitalClinicAddress(int id)
        {
            var hospitalClinicAddress = await _context.HospitalClinicAddress.FindAsync(id);

            if (hospitalClinicAddress == null)
            {
                return NotFound();
            }

            return hospitalClinicAddress;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<HospitalClinicAddress>> GetHospitalClinicAddressBasedOnId(int userId)
        {
            var hospitalClinicAddress = await _context.HospitalClinicAddress.FirstOrDefaultAsync(x => x.hospitalOrClinicId == userId);

            if (hospitalClinicAddress == null)
            {
                return NotFound();
            }

            return hospitalClinicAddress;
        }

        // PUT: api/HospitalClinicAddresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalClinicAddress(int id, HospitalClinicAddress hospitalClinicAddress)
        {
            if (id != hospitalClinicAddress.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalClinicAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalClinicAddressExists(id))
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

        // POST: api/HospitalClinicAddresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalClinicAddress>> PostHospitalClinicAddress(HospitalClinicAddress hospitalClinicAddress)
        {
            _context.HospitalClinicAddress.Add(hospitalClinicAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalClinicAddress", new { id = hospitalClinicAddress.id }, hospitalClinicAddress);
        }

        // DELETE: api/HospitalClinicAddresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalClinicAddress>> DeleteHospitalClinicAddress(int id)
        {
            var hospitalClinicAddress = await _context.HospitalClinicAddress.FindAsync(id);
            if (hospitalClinicAddress == null)
            {
                return NotFound();
            }

            _context.HospitalClinicAddress.Remove(hospitalClinicAddress);
            await _context.SaveChangesAsync();

            return hospitalClinicAddress;
        }

        private bool HospitalClinicAddressExists(int id)
        {
            return _context.HospitalClinicAddress.Any(e => e.id == id);
        }
    }
}
