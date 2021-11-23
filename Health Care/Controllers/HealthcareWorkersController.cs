using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin, عامل صحي")]
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthcareWorkersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HealthcareWorkers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorker()
        {
            return await (from HealthWorker in _context.HealthcareWorker
                          where HealthWorker.active == true
                          select new
                          {
                              id = HealthWorker.id,
                              Name = HealthWorker.Name,
                              Picture = HealthWorker.Picture,
                              BackgroundImage=HealthWorker.BackGroundPicture,
                              Description = HealthWorker.Description,
                              Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                                          join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                                          where healthcareWorkerServices.HealthcareWorkerid == HealthWorker.id
                                          select service).ToList(),
                          }
                          ).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetDisabled()
        {
            return await (from worker in _context.HealthcareWorker
                          join user in _context.User on worker.userId equals user.id
                          where worker.active == false && user.active == false
                          select worker).ToListAsync();
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RestoreService(List<HealthcareWorker> halthcareWorker)
        {
            if (halthcareWorker.Count == 0)
                return NoContent();

            try
            {
                foreach (HealthcareWorker item in halthcareWorker)
                {
                    var s = _context.HealthcareWorker.Where(s => s.id == item.id).FirstOrDefault();
                    var user = _context.User.Where(x=>x.id == s.userId).FirstOrDefault();
                    user.active = true;
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

        [HttpGet("{pageKey}/{pageSize}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HealthcareWorker>>> GetHealthcareWorkers(int pageKey, int pageSize)
        {
            var healthcares= await _context.HealthcareWorker.Include(x=>x.HealthcareWorkerRegions).Where(a => a.active == true).ToListAsync();
            if (pageSize != 0)
            {
                return healthcares.Skip(pageKey).Take(pageSize).ToList();
            }
            else
            {
                return healthcares;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkersWithRegions()
        {
            return await _context.HealthcareWorker.Where(a => a.active == true).Include(h => h.HealthcareWorkerRegions).ToListAsync();
        }
    

        // GET: api/HealthcareWorkers/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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
                BackgroundImage = healthcareWorker.BackGroundPicture,
                healthcareWorker.active,
                //Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                //                  join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                //                  where healthcareWorkerServices.HealthcareWorkerid == id 
                //                  select new { 
                //                  id=service.id,
                //                  serviceName=service.serviceName,
                //                  servicePrice= healthcareWorkerServices.Price
                //                  }).ToList(),
            };

            return healthcareWorker;
        }
        [HttpGet("{id}")]
        //نظر تحتاج مستخدمة في صقحة ال ServicesSearch
        public async Task<ActionResult<object>> GetHealthcareWorkerforWorkerDetailPage(int id)
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
                BackgroundImage = healthcareWorker.BackGroundPicture,
                healthcareWorker.active,
                Services = (from healthcareWorkerServices in _context.HealthcareWorkerService
                            join service in _context.Service on healthcareWorkerServices.serviceId equals service.id
                            where healthcareWorkerServices.HealthcareWorkerid == id
                            select service).ToList(),
            };

            return doctor;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<HealthcareWorker>> GetHealthcareWorkerByUserId(int id)
        {
            var healthcareWorker = await _context.HealthcareWorker.Where(x=>x.userId == id).FirstOrDefaultAsync();
            if (healthcareWorker == null)
            {
                return NotFound();
            }
            return healthcareWorker;
        }

        [HttpGet("{serviceId}")]
        //نظر تحتاج مستخدمة في صقحة ال ServicesSearch
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
            HealthcareWorker worker = _context.HealthcareWorker.Find(healthcareWorker.id);
            worker.Name = healthcareWorker.Name ?? worker.Name;
            worker.ReagionID = healthcareWorker.ReagionID != 0 ? healthcareWorker.ReagionID :  worker.ReagionID;
            worker.active = worker.active;
            worker.CountOfDoesNotCome = healthcareWorker.CountOfDoesNotCome != 0 ? healthcareWorker.CountOfDoesNotCome: worker.CountOfDoesNotCome;
            worker.Description = healthcareWorker.Description ?? worker.Description;
            worker.Gender = healthcareWorker.Gender ?? worker.Gender;
            worker.specialityId = healthcareWorker.specialityId != 0 ? healthcareWorker.specialityId : worker.specialityId;
            worker.userId = healthcareWorker.userId != 0 ? healthcareWorker.userId : worker.userId;
            worker.WorkPlace = healthcareWorker.WorkPlace ?? worker.WorkPlace;
            //_context.Entry(healthcareWorker).State = EntityState.Modified;

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

        [AllowAnonymous]
        [HttpGet("{patientId}/{regionId}/{serviceId}/{pageKey}/{pageSize}/{byString}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHealthcareWorkerWithFavorite(int patientId,int pageKey,int pageSize,int regionId,int serviceId,string Bystring)
        {
            var favorite = await _context.Favorite.Where(x => x.PatientId == patientId && x.type == "worker").ToListAsync();
            Bystring = Bystring.Replace("empty", "");
            var WorkerH =await (from HealthWorker in _context.HealthcareWorker
                                 join user in _context.User on HealthWorker.userId equals user.id
                                 where HealthWorker.active == true
                                 select new
                                 {
                                     id = HealthWorker.id,
                                     Name = HealthWorker.Name,
                                     Picture = HealthWorker.Picture,
                                     user.regionId,
                                     regionworking = _context.HealthcareWorkerRegions.Where(x => x.HealthcareWorkerid == HealthWorker.id).ToList(),
                                     BackgroundImage = HealthWorker.BackGroundPicture,
                                     userId = HealthWorker.userId,
                                     services = (from workerService in _context.HealthcareWorkerService
                                                 join service in _context.Service on workerService.serviceId equals service.id
                                                 where workerService.HealthcareWorkerid == HealthWorker.id
                                                 select service).ToList(),
                                     Description = HealthWorker.Description
                                 }
             ).Where(x => x.Name.Contains(Bystring)).ToListAsync();

             if (serviceId != 0)
            {
                WorkerH = WorkerH.Where(x => x.services.Exists(x => x.id == serviceId)).ToList();
            }
             if (regionId != 0)
            {
                WorkerH = WorkerH.Where(x => x.regionworking.Exists(c => c.RegionID == regionId)).ToList();
            }
            var listFinalResult = new List<object>();
            bool flag = false;
            foreach (var i in WorkerH)
            {
                foreach (var j in favorite)
                {
                    if (j.UserId == i.id)
                        flag = true;
                }
                var docrotwithfavorite = new
                {
                    i.id,
                    i.Name,
                    i.Picture,
                    i.Description,
                    i.regionId,
                    i.regionworking,
                    i.userId,
                    i.services,
                    isFavorite = flag ? true : false,
                };
                listFinalResult.Add(docrotwithfavorite);
                flag = false;
            }
           
            if (pageSize != 0)
            {
                return listFinalResult.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return listFinalResult.ToList();
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
        
        [HttpPost]
        public async Task<ActionResult<HealthcareWorker>> PostHealthcareWorkerServices(List<HealthcareWorkerService> healthcareWorkerservices)
        {
            if (healthcareWorkerservices.Count > 0)
            {
            var healthcareWorker = await _context.HealthcareWorker.Where(x => x.id == healthcareWorkerservices[0].HealthcareWorkerid).Include(x=>x.HealthcareWorkerServices)
                            .FirstOrDefaultAsync();
                        healthcareWorker.HealthcareWorkerServices = healthcareWorkerservices;
                        await _context.SaveChangesAsync();

                        return CreatedAtAction("GetHealthcareWorker", new { id = healthcareWorker.id }, healthcareWorker);
            }
            return BadRequest();
            
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
            var user = _context.User.Where(x => x.id == healthcareWorker.userId).FirstOrDefault();
            user.active = false;
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
