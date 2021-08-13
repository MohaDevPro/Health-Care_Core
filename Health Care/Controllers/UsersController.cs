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
    public class UsersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public UsersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.Where(s => s.active == true).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetDisabled()
        {
            return await _context.User.Where(a => a.active == false).ToListAsync();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetNotActive()
        {
            return await _context.User.Where(a => a.isActiveAccount == false).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetNumberOfUsersByRegion(int regionId)
        {
            return await _context.User.CountAsync(x => x.regionId==regionId);
        }
        [HttpGet]
        public async Task<ActionResult<int>> GetNumberOfUsersByRegionAndRole(int regionId,int roleId)
        {
            return await _context.User.CountAsync(x => x.regionId == regionId && x.Roleid==roleId);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRegion(int regionId)
        {

            return await _context.User.Where(a => a.regionId==regionId).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRegionAndRole(int regionId,int roleId)
        {
            return await _context.User.Where(a => a.regionId == regionId && a.Roleid==roleId).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsersByRole( int roleId)
        {
            if (roleId == 2) {
                
                return await
                (from clinic in _context.ExternalClinic
                 join user in _context.User on clinic.userId equals user.id
                 where clinic.active == true
                 select new
                 {
                     
                     user.id,
                     nameAR= clinic.Name,
                     user.nameEN,
                     user.phoneNumber,
                     user.regionId,
                     user.Roleid,
                     clinicID=clinic.id,
                     //user.isActiveAccount,
                     //user.email,
                     //user.DeviceId,
                     //user.completeData,
                     user.address,
                     specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                       join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                       where specialitydoctor.Doctorid == clinic.id  && specialitydoctor.Roleid == 1
                                       select specialit).ToList(),
                 }
                              ).ToListAsync();
            }
            else return await
               (from doctor in _context.Doctor
                join user in _context.User on doctor.Userid equals user.id
                where doctor.active == true && user.Roleid==5
                select new
                {
                    user.id,
                    nameAR=doctor.name,
                    user.nameEN,
                    user.phoneNumber,
                    user.regionId,
                    user.Roleid,
                    //user.isActiveAccount,
                    //user.email,
                    //user.DeviceId,
                    //user.completeData,
                    user.address,
                    specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                      join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                      where specialitydoctor.Doctorid == doctor.id && specialitydoctor.Roleid == 0
                                      select specialit).ToList(),
                }
                             ).ToListAsync();

        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<User> users)
        {
            if (users.Count == 0)
                return NoContent();

            try
            {
                foreach (User item in users)
                {
                    var s = _context.User.Where(s => s.id == item.id).FirstOrDefault();
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


        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpPut("{result}/{id}")]
        public async Task<IActionResult> PutUserCompleteData(bool reuslt,int id )
        {

            var user =_context.User.Where(u => u.id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }



            try
            {
                user.completeData = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<IActionResult> ActiveUser(int id )
        {

            var user =_context.User.Where(u => u.id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            if (user.Roleid == 1)
            {
                var worker = _context.HealthcareWorker.Where(x => x.userId == id).FirstOrDefault();
                worker.active = true;
            }
            else if (user.Roleid == 2)
            {
                var clinic = _context.ExternalClinic.Where(x => x.userId == id).FirstOrDefault();
                clinic.active = true;
            }
            else if (user.Roleid == 3)
            {
                var hospital = _context.Hospitals.Where(x => x.UserId == id).FirstOrDefault();
                hospital.active = true;
            }
            else if (user.Roleid == 5)
            {
                var doctor = _context.Doctor.Where(x => x.Userid == id).FirstOrDefault();
                doctor.active = true;
            }

            try
            {
                user.isActiveAccount = true;
                user.active = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<IActionResult> InactiveUser(int id )
        {

            var user =_context.User.Where(u => u.id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            if (user.Roleid == 1)
            {
                var worker = _context.HealthcareWorker.Where(x => x.userId == id).FirstOrDefault();
                worker.active = false;
            }
            else if (user.Roleid == 2)
            {
                var clinic = _context.ExternalClinic.Where(x => x.userId == id).FirstOrDefault();
                clinic.active = false;
            }
            else if (user.Roleid == 3)
            {
                var hospital = _context.Hospitals.Where(x => x.UserId == id).FirstOrDefault();
                hospital.active = false;
            }
            else if (user.Roleid == 5)
            {
                var doctor = _context.Doctor.Where(x => x.Userid == id).FirstOrDefault();
                doctor.active = false;
            }

            try
            {
                user.active = false;
                user.isActiveAccount = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.active = false;
            //_context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPut]
        public async Task<IActionResult> ChangePass(CH_Password password)
        {
            User users = await _context.User.FindAsync(password.id);

            if (users.Password != password.OldPass)
                return BadRequest();

            users.Password = password.NewPass;

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(password.id))
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

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }

    }
}
