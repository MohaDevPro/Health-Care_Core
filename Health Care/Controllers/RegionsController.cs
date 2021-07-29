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
    public class RegionsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public RegionsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegion()
        {
            return await _context.Region.Where(a=>a.active == true).ToListAsync();
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Governorate>>> GetGovernorate()
        //{
        //    return await _context.Governorate.ToListAsync();
        //}
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<District>>> GetDistrict()
        //{
        //    return await _context.District.ToListAsync();
        //}



        // GET: api/Regions/5

        public async Task<ActionResult<IEnumerable<Region>>> GetDisabled()
        {
            return await _context.Region.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
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
        public async Task<ActionResult<IEnumerable<Region>>> GetRegionBasedOnDistrictId(int id)
        {
            return await _context.Region.Where(x => x.DistrictID == id && x.active==true).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Governorate>>> GetGovernorate()
        {
            return await _context.Governorate.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrict()
        {
            return await _context.District.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictBasedOnGovernorateId(int id)
        {
            return await _context.District.Where(x => x.GovernorateID == id && x.active==true).ToListAsync();
        }

        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGovernorate(int id,Governorate governorate)
        {
            if (id != governorate.ID)
            {
                return BadRequest();
            }

            _context.Entry(governorate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!GovernorateExists(id))
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id,District district)
        {
            if (id != district.ID)
            {
                return BadRequest();
            }

            _context.Entry(district).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!DistrictExists(id))
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
        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
            _context.Region.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.ID }, region);
        }

        [HttpPost]
        public async Task<ActionResult<Governorate>> PostGovernorate(Governorate governorate)
        {
            _context.Governorate.Add(governorate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGovernorate", new { id = governorate.ID }, governorate);
        }
        [HttpPost("{GovernorateId}")]
        public async Task<ActionResult<District>> PostDistrict(int GovernorateId, District district)
        {
            district.GovernorateID = GovernorateId;
            _context.District.Add(district);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrict", new { id = district.ID }, district);
        }



        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Governorate>> DeleteGovernorate(int id)
        {
            var governorate = await _context.Governorate.FindAsync(id);
            if (governorate == null)
            {
                return NotFound();
            }

            _context.Governorate.Remove(governorate);
            await _context.SaveChangesAsync();

            return governorate;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<District>> DeleteDistrict(int id)
        {
            var district = await _context.District.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            _context.District.Remove(district);
            await _context.SaveChangesAsync();

            return district;
        }

        private bool RegionExists(int id)
        {
            return _context.Region.Any(e => e.ID == id);
        }
        private bool GovernorateExists(int id)
        {
            return _context.Governorate.Any(e => e.ID == id);
        }
        private bool DistrictExists(int id)
        {
            return _context.District.Any(e => e.ID == id);
        }
    }
}
