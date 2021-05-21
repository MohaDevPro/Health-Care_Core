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
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthcareWorkersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthcareWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorker()
        {
            return await (from HealthWorker in _context.HealthcareWorker
                          select new
                          {
                              id = HealthWorker.id,
                              Name = HealthWorker.Name,
                              Picture = HealthWorker.Picture,
                              Backgroundimage=HealthWorker.BackGroundPicture,
                              Description = HealthWorker.Description
                          }
                          ).ToListAsync();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkers()
        {
            return await _context.HealthcareWorker.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkersWithRegions()
        {
            return await _context.HealthcareWorker.Include(h => h.HealthcareWorkerRegions).ToListAsync();
        }

        // GET: api/HealthcareWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetHealthcareWorker(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.FindAsync(id);
            if (healthcareWorker == null)
            {
                return NotFound();
            }
            var doctor = new
            {
                id = id,
                Name = healthcareWorker.Name,
                Picture = healthcareWorker.Picture,
                Backgroundimage = healthcareWorker.BackGroundPicture,
                Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                                  join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                                  where healthcareWorkerServices.HealthcareWorkerid == id 
                                  select new { 
                                  id=service.id,
                                  serviceName=service.serviceName,
                                  servicePrice= healthcareWorkerServices.Price

                                  }).ToList(),
            };


            return doctor;
        }

        // PUT: api/HealthcareWorkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthcareWorker(int id, HealthcareAppworktime healthcareWorker)
        {
            if (id != healthcareWorker.id)
            {
                return BadRequest();
            }

            _context.Entry(healthcareWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareWorkerExists(id))
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


        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkerWithFavorite(int patientId)
        {
            var WorkerWithFavorite = await (from fav in _context.Favorite join Worker in _context.HealthcareWorker on fav.UserId equals Worker.userId where fav.PatientId == patientId select fav).ToListAsync();
            var WorkerH = await (from HealthWorker in _context.HealthcareWorker
                                 select new
                                 {
                                     id = HealthWorker.id,
                                     Name = HealthWorker.Name,
                                     Picture = HealthWorker.Picture,
                                     userId = HealthWorker.userId,
                                     Description = HealthWorker.Description
                                 }
                          ).ToListAsync();
            var listFinalResult = new List<object>();
            bool flag = false;
            foreach (var i in WorkerH)
            {
                foreach (var j in WorkerWithFavorite)
                {
                    if (j.UserId == i.userId)
                        flag = true;
                }
                var docrotwithfavorite = new
                {
                    i.id,
                    i.Name,
                    i.Picture,
                    i.Description,
                    isFavorite = flag ? true : false,
                };
                listFinalResult.Add(docrotwithfavorite);
                flag = false;
            }
            return listFinalResult;


        }

        // POST: api/HealthcareWorkers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HealthcareAppworktime>> PostHealthcareWorker(HealthcareAppworktime healthcareWorker)
        {
            _context.HealthcareWorker.Add(healthcareWorker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
        }
        
        //[HttpPost("{id}")]
        //public async Task<ActionResult<HealthcareWorker>> PostHealthcareWorker(int id, HealthcareWorker healthcareWorker)
        //{
        //    _context.HealthcareWorker.Add(healthcareWorker);
        //    await _context.SaveChangesAsync();
        //    //var healthWorker = _context.HealthcareWorker.Include(h=>h.HealthcareWorkerRegions).Where(h => h.userId == id).FirstOrDefault();
        //    //if (healthWorker == null)
        //    //{
        //    //    return NotFound("This is not exist");
        //    //}
        //    //healthWorker.specialityID = healthcareWorker.specialityID;
        //    //healthWorker.WorkPlace = healthcareWorker.WorkPlace;
        //    //healthWorker.ReagionID = healthcareWorker.ReagionID;
        //    //healthWorker.Gender = healthcareWorker.Gender;
        //    //healthWorker.Description = healthcareWorker.Description;
        //    //foreach (var item in healthcareWorker.HealthcareWorkerRegions)
        //    //{
        //    //    healthWorker.HealthcareWorkerRegions.Add(item);
        //    //}
        //    //await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
        //}
        [HttpPost]
        public async Task<ActionResult<HealthcareAppworktime>> PostHealthcareWorkerServices(List<HealthcareWorkerService> healthcareWorkerservices)
        {
            //var healthcareWorker = await _context.HealthcareWorker.Include(x=>x.HealthcareWorkerServices).FirstOrDefaultAsync(x=>x.id==healthcareWorkerservices[0].HealthcareWorkerid);
            var healthcareWorker = await _context.HealthcareWorker.Include(x=>x.HealthcareWorkerServices).FirstOrDefaultAsync(x=>x.userId==healthcareWorkerservices[0].HealthcareWorkerid);
            healthcareWorker.HealthcareWorkerServices = healthcareWorkerservices;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
        }

        // DELETE: api/HealthcareWorkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthcareAppworktime>> DeleteHealthcareWorker(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.FindAsync(id);
            if (healthcareWorker == null)
            {
                return NotFound();
            }

            _context.HealthcareWorker.Remove(healthcareWorker);
            await _context.SaveChangesAsync();

            return healthcareWorker;
        }

        private bool HealthcareWorkerExists(int id)
        {
            return _context.HealthcareWorker.Any(e => e.id == id);
        }
    }
}
