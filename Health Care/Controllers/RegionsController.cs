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
    public class RegionsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public RegionsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegion()
        {
            return await _context.Region.Where(a => a.active == true).ToListAsync();
        }

        // GET: api/Regions/5
        [Authorize(Roles = "admin")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Region>>> GetDisabled()
        {
            return await _context.Region.Where(a => a.active == false).ToListAsync();
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> RestoreService(List<Region> regions)
        {
            if (regions.Count == 0)
                return NoContent();

            try
            {
                foreach (Region item in regions)
                {
                    var s = _context.Region.Where(s => s.ID == item.ID).FirstOrDefault();
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

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,عامل صحي")]
        public async Task<ActionResult<IEnumerable<Region>>> GetHealthWorkerRegionsByWorkerID(int id)
        {
            var workerRegions = await _context.HealthcareWorkerRegions.Where(x => x.HealthcareWorkerid == id).Select(x => x.Region).ToListAsync();
            return workerRegions;
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<Region>>> GetRegionBasedOnDistrictId(int id)
        {
            return await _context.Region.Where(x => x.DistrictID == id && x.active==true).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<District>>> GetDistrictBasedOnGovernorateId(int id)
        {
            return await _context.District.Where(x => x.GovernorateID == id && x.active==true).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> PutRegion(int id, Region region)
        {
            if (id != region.ID)
            {
                return BadRequest();
            }

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!RegionExists(id))
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

        // POST: api/Regions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
            _context.Region.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.ID }, region);
        }
        [HttpPost("{regionID}/{WorkerID}")]
        // تحتاج نظر تبع صفحة AddHealthcareWorkerForHospital
        [Authorize(Roles = "admin,عامل صحي")]
        public async Task<ActionResult> PostWorkerRegion(int regionID,int WorkerID)
        {
            var h1 = _context.HealthcareWorkerRegions.Where(x=>x.HealthcareWorkerid == WorkerID && x.RegionID == regionID).FirstOrDefault();
            
            if (h1 != null)
            {
                return Ok();
            }
            var h = new HealthcareWorkerRegion() { RegionID = regionID, HealthcareWorkerid = WorkerID };
            _context.HealthcareWorkerRegions.Add(h);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Region>> DeleteRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            region.active = false;
            //_context.Region.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }
        // تحتاج نظر تبع صفحة AddHealthcareWorkerForHospital
        [HttpDelete("{id}/{workerID}")]
        [Authorize(Roles = "admin,عامل صحي")]
        public async Task<ActionResult> DeleteWorkerRegion(int id,int workerID)
        {
            var region =  _context.HealthcareWorkerRegions.Where(x=>x.RegionID == id && x.HealthcareWorkerid == workerID).FirstOrDefault();
            if (region == null)
            {
                return NotFound();
            }

            _context.HealthcareWorkerRegions.Remove(region);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RegionExists(int id)
        {
            return _context.Region.Any(e => e.ID == id);
        }
    }
}
