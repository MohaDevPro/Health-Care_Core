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
    public class ServicesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ServicesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetServiceByHealthWorkerID(int id)
        {
            var choosingServices = _context.HealthcareWorker.Include(x => x.HealthcareWorkerServices).FirstOrDefault(x => x.id == id).HealthcareWorkerServices.Select(x=>x.serviceId);
            return await (from service in  _context.Service
                          select new
                          {
                              service.id,
                              service.serviceName,
                              service.servicePrice,
                              isSelected = choosingServices.Contains(service.id)
                          }
                          
                          ).ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Service.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Service.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteService(int id)
        {
            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Service.Remove(service);
            await _context.SaveChangesAsync();

            return service;
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.id == id);
        }
    }
}
