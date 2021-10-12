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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetService()
        {
            var service = await _context.Service.Where(s=>s.active==true).ToListAsync();
            return service;
        }
        // GET: api/Services
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetServiceByHealthWorkerID(int id)
        {
            var healthcareWorker = _context.HealthcareWorker.Where(h => h.userId == id).FirstOrDefault();
            var choosingServices = _context.HealthcareWorker.Include(x => x.HealthcareWorkerServices).FirstOrDefault(x => x.id == healthcareWorker.id);
            var choosingIDs = new List<int>();
            if (choosingServices != null) {
                choosingIDs = choosingServices.HealthcareWorkerServices.Select(x => x.serviceId).ToList();
            }
            var listService = new List<object>();
            foreach(var service in _context.Service )
            {
                if (service.active == true) 
                {
                    //var price = service.servicePrice;
                    var oldservice = choosingIDs.Contains(service.id) ? choosingServices.HealthcareWorkerServices.First(x => x.serviceId == service.id) : new HealthcareWorkerService();
                    var services = new
                    {
                        service.id,
                        service.serviceName,
                        servicePrice = service.servicePrice,
                        isSelected = choosingIDs.Contains(service.id),
                        oldchoosing = oldservice
                    };
                    listService.Add(services);
                }
            }
            return listService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllServiceByHealthWorker() { 


            var choosingIDs = new List<int>();
            
            var listService = new List<object>();
            foreach(var service in _context.Service)
            {
                if (service.active == true)
                {
                    var services = new
                    {
                        service.id,
                        service.serviceName,
                        servicePrice = 0,
                        isSelected = false,
                        oldchoosing = new HealthcareWorkerService()
                    };
                    listService.Add(services);  
                }
            }
            return listService;
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

        public async Task<ActionResult<IEnumerable<Service>>> GetDisabled()
        {
            return await _context.Service.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<Service> service)
        {
            if (service.Count == 0)
                return NoContent();

            try
            {
                foreach (Service item in service)
                {
                    var s = _context.Service.Where(s => s.id == item.id).FirstOrDefault();
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
            service.active = false;
            //_context.Service.Remove(service);
            await _context.SaveChangesAsync();

            return service;
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.id == id);
        }
    }
}
