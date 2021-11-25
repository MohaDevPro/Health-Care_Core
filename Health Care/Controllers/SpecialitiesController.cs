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

    public class SpecialitiesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public SpecialitiesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Specialities
        [AllowAnonymous]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpeciality()
        {
            return await _context.Speciality.Where(s=>s.active==true).ToListAsync();
        } 
        
        //public async Task<ActionResult<IEnumerable<SpecialityHealthWorker>>> GetSpecialityHealthWorker()
        //{
        //    return await _context.SpecialityHealthWorker.ToListAsync();
        //}
        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<Speciality>>> GetDisabled()
        {
            return await _context.Speciality.Where(a => a.active == false).ToListAsync();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]

        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<Speciality> Speciality)
        {
            if (Speciality.Count == 0)
                return NoContent();

            try
            {
                foreach (Speciality item in Speciality)
                {
                    var s = _context.Speciality.Where(s => s.id == item.id).FirstOrDefault();
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
        [AllowAnonymous]

        [HttpGet("{isbasic}")]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialityByBasicType(bool isbasic)
        {
            return await _context.Speciality.Where(x=>x.isBasic==isbasic && x.active ==true).ToListAsync();
        }
        [AllowAnonymous]

        [HttpGet("{UserID}/{RoleID}")]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialityByUseridAndRoleID(int UserID,int RoleID)
        {
            return await (from speciality in  _context.Speciality join sepeialityDoctor in _context.SpeciallyDoctors 
                          on speciality.id equals sepeialityDoctor.Specialityid
                          where sepeialityDoctor.Doctorid==UserID && sepeialityDoctor.Roleid==RoleID && speciality.active ==true
                          select speciality
                          ).ToListAsync();
        }
        [Authorize(Roles = "admin,مستشفى,دكتور,عيادة")]

        [HttpPost("{UserID}/{SpecialityID}/{RoleID}")]
        public async Task<ActionResult<Speciality>> PostSpecialityForUser(int UserID,int SpecialityID,int RoleID)
        {
            var specialitydoctor = new SpeciallyDoctor() { Doctorid = UserID, Specialityid = SpecialityID, Roleid = RoleID };
            var check = _context.SpeciallyDoctors.FirstOrDefault(x => x.Doctorid == UserID && x.Specialityid == SpecialityID && x.Roleid == RoleID);
            if (check == null)
            {
                _context.SpeciallyDoctors.Add(specialitydoctor);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetSpeciality", new { id = specialitydoctor.id }, specialitydoctor);
        }
        [Authorize(Roles = "admin,مستشفى,دكتور,عيادة")]

        [HttpDelete("{UserID}/{SpecialityID}/{RoleID}")]
        public async Task<ActionResult<SpeciallyDoctor>> DeleteSpecialityForUser(int UserID, int SpecialityID, int RoleID)
        {
            var speciality = await _context.SpeciallyDoctors.FirstOrDefaultAsync(x=>x.Doctorid==UserID&&x.Specialityid==SpecialityID&&x.Roleid==RoleID);
            if (speciality == null)
            {
                return NotFound();
            }

            _context.SpeciallyDoctors.Remove(speciality);
            await _context.SaveChangesAsync();

            return speciality;
        }
        // GET: api/Specialities/5
        [AllowAnonymous]

        [HttpGet("{id}")]
        public async Task<ActionResult<Speciality>> GetSpeciality(int id)
        {
            var speciality = await _context.Speciality.FindAsync(id);

            if (speciality == null)
            {
                return NotFound();
            }

            return speciality;
        }

        // PUT: api/Specialities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> PutSpeciality(int id, Speciality speciality)
        {
            if (id != speciality.id)
            {
                return BadRequest();
            }

            _context.Entry(speciality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialityExists(id))
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

        // POST: api/Specialities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Speciality>> PostSpeciality(Speciality speciality)
        {
            _context.Speciality.Add(speciality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeciality", new { id = speciality.id }, speciality);
        }

        // DELETE: api/Specialities/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Speciality>> DeleteSpeciality(int id)
        {
            var speciality = await _context.Speciality.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }
            speciality.active = false;
            //_context.Speciality.Remove(speciality);
            await _context.SaveChangesAsync();

            return speciality;
        }

        private bool SpecialityExists(int id)
        {
            return _context.Speciality.Any(e => e.id == id);
        }
    }
}
