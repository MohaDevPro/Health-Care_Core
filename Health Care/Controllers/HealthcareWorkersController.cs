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
                          where HealthWorker.active == true
                          select new
                          {
                              id = HealthWorker.id,
                              Name = HealthWorker.Name,
                              Picture = HealthWorker.Picture,
                              Backgroundimage=HealthWorker.BackGroundPicture,
                              Description = HealthWorker.Description,
                              Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                                          join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                                          where healthcareWorkerServices.HealthcareWorkerid == HealthWorker.id
                                          select new
                                          {
                                              id = service.id,
                                              serviceName = service.serviceName,
                                              servicePrice = healthcareWorkerServices.Price

                                          }).ToList(),
                          }
                          ).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetDisabled()
        {
            return await _context.HealthcareWorker.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<HealthcareWorker> halthcareWorker)
        {
            if (halthcareWorker.Count == 0)
                return NoContent();

            try
            {
                foreach (HealthcareWorker item in halthcareWorker)
                {
                    var s = _context.HealthcareWorker.Where(s => s.id == item.id).FirstOrDefault();
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetHealthcareWorkers()
        {
            return await _context.HealthcareWorker.Include(x=>x.HealthcareWorkerRegions).Where(a => a.active == true).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkersWithRegions()
        {
            return await _context.HealthcareWorker.Where(a => a.active == true).Include(h => h.HealthcareWorkerRegions).ToListAsync();
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
                Description = healthcareWorker.Description,
                Backgroundimage = healthcareWorker.BackGroundPicture,
                healthcareWorker.active,
                Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                                  join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                                  where healthcareWorkerServices.HealthcareWorkerid == id 
                                  select new { 
                                  id=service.id,
                                  serviceName=service.serviceName,
                                  servicePrice= healthcareWorkerServices.Price

                                  }).ToList(),
            };


            return healthcareWorker;
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetHealthcareWorkerBasedOnServiceId(int serviceId)
        {
            //var healthcareWorkerServiceList = await _context.HealthcareWorkerService.Where(x => x.serviceId == serviceId).ToListAsync();
            List<HealthcareWorker> healthWorkerInfo = 
                                   (from hw in _context.HealthcareWorker
                                    where hw.active == true
                                    join hws in _context.HealthcareWorkerService on hw.id equals hws.HealthcareWorkerid
                                    where hws.serviceId == serviceId 
                                    select new HealthcareWorker
                                    {
                                        id = hw.id,
                                        Name = hw.Name,
                                        userId = hw.userId,
                                        Picture = hw.Picture,
                                        BackGroundPicture = hw.BackGroundPicture,
                                        Description = hw.Description,
                                        identificationImage = hw.identificationImage,
                                        specialityId = hw.specialityId,
                                        graduationCertificateImage = hw.graduationCertificateImage,
                                    }).ToList();
            return healthWorkerInfo;
        }


        // PUT: api/HealthcareWorkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthcareWorker(int id, HealthcareWorker healthcareWorker)
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
        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> PutWorkerStatus(int id, bool status)
        {

            var worker = _context.HealthcareWorker.Find(id);
            if (worker == null)
            {
                return BadRequest();
            }
            worker.active = status;
            _context.Entry(worker).State = EntityState.Modified;

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
            var WorkerWithFavorite = await (from fav in _context.Favorite join Worker in _context.HealthcareWorker.Where(x=>x.active == true)  on fav.UserId equals Worker.userId where fav.PatientId == patientId select fav).ToListAsync();
            var WorkerH = await (from HealthWorker in _context.HealthcareWorker
                                 where HealthWorker.active == true
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
        public async Task<ActionResult<HealthcareWorker>> PostHealthcareWorker(HealthcareWorker healthcareWorker)
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
        public async Task<ActionResult<HealthcareWorker>> PostHealthcareWorkerServices(List<HealthcareWorkerService> healthcareWorkerservices)
        {
            var healthcareWorker = await _context.HealthcareWorker.Where(x => x.userId == healthcareWorkerservices[0].HealthcareWorkerid).Include(x=>x.HealthcareWorkerServices)
                .FirstOrDefaultAsync();
            var d = 0;
            healthcareWorker.HealthcareWorkerServices = healthcareWorkerservices;
            var e = 5;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
        }

        // DELETE: api/HealthcareWorkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthcareWorker>> DeleteHealthcareWorker(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.FindAsync(id);
            if (healthcareWorker == null)
            {
                return NotFound();
            }
            healthcareWorker.active = false;
            //_context.HealthcareWorker.Remove(healthcareWorker);
            await _context.SaveChangesAsync();

            return healthcareWorker;
        }

        private bool HealthcareWorkerExists(int id)
        {
            return _context.HealthcareWorker.Any(e => e.id == id);
        }
    }
}
