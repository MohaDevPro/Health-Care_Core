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
    public class UsersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public UsersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("{pageKey}/{pageSize}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int pageKey, int pageSize)
        {
            var users = await _context.User.Where(s => s.active == true).ToListAsync();
            if (pageSize != 0)
            {
                return users.Skip(pageKey).Take(pageSize).ToList();
            }
            else
            {
                return users;
            }
            
        }
         [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRoleID(int id)
        {
            return await _context.User.Where(s => s.active == true & s.Roleid == id).ToListAsync();
        }
        [AllowAnonymous]
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
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<User>>> GetDisabled()
        {
            return await _context.User.Where(a => a.active == false).ToListAsync();
        }
        
        [HttpGet("{pageKey}/{pageSize}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetNotActive(int pageKey, int pageSize)
        {
            
            var users =  await _context.User.Where(a => a.isActiveAccount == false).ToListAsync();
            if (pageSize != 0)
            {
                return users.Skip(pageKey).Take(pageSize).ToList();
            }
            else
            {
                return users;
            }

        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<int>> GetNumberOfUsersByRegion(int regionId)
        {
            return await _context.User.CountAsync(x => x.regionId==regionId);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<int>> GetNumberOfUsersByRegionAndRole(int regionId,int roleId)
        {
            return await _context.User.CountAsync(x => x.regionId == regionId && x.Roleid==roleId);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRegion(int regionId)
        {

            return await _context.User.Where(a => a.regionId==regionId).ToListAsync();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRegionAndRole(int regionId,int roleId)
        {
            return await _context.User.Where(a => a.regionId == regionId && a.Roleid==roleId).ToListAsync();
        }

        [HttpGet("{roleId}/{regionId}/{specialityId}/{pageKey}/{pageSize}/{byString}")]
        [Authorize(Roles = "admin,عيادة,مستشفى,دكتور")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsersByRole( int roleId, int regionId,int specialityId,int pageKey,int pageSize,string byString)
        {
            var requests = _context.DoctorClinicReqeusts;
            if (roleId == 2) {
                
                var clinicOBJ= await
                (from clinic in _context.ExternalClinic
                 join user in _context.User on clinic.userId equals user.id
                 where clinic.active == true && (requests.FirstOrDefault(x=>x.IsCanceled==false&&(x.FromID==user.id||x.ToID==user.id)&&x.ClinicID==0||x.ClinicID==clinic.id)==null)
                 select new
                 {
                     
                     user.id,
                     nameAR= clinic.Name,
                     //user.nameEN,
                     user.phoneNumber,
                     listRegionId = new List<int>(user.regionId),
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
                              ).Where(x=>x.nameAR.Contains(byString)).ToListAsync();
                if (regionId != 0)
                {
                    clinicOBJ = clinicOBJ.Where(x => x.listRegionId.Contains(regionId)).ToList();
                }
                if (specialityId != 0)
                {
                    clinicOBJ = clinicOBJ.Where(x => x.specialitylist.Exists(x => x.id == specialityId)).ToList();
                }
                if (pageSize != 0)
                    return clinicOBJ.Skip(pageKey).Take(pageSize).ToList();
                else
                    return clinicOBJ;
            }
            else
            { var doctorOBJ= await
               (from doctor in _context.Doctor
                join user in _context.User on doctor.Userid equals user.id
                where doctor.active == true && user.Roleid==5 && (requests.FirstOrDefault(x => x.IsCanceled == false && (x.FromID == user.id || x.ToID == user.id)) == null)
                select new
                {
                    user.id,
                    nameAR=doctor.name,
                    //user.nameEN,
                    user.phoneNumber,
                    listRegionId= (from dr in _context.Doctor
                                   join cldr in _context.clinicDoctors on dr.id equals cldr.Doctorid
                                   join cl in _context.ExternalClinic on cldr.Clinicid equals cl.id
                                   join usr in _context.User on cl.userId equals usr.id
                                   where dr.id == doctor.id
                                   select usr.regionId).ToList(),
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
                if (regionId != 0)
                {
                    doctorOBJ = doctorOBJ.Where(x => x.listRegionId.Contains(regionId)).ToList();
                }
                if (specialityId != 0)
                {
                    doctorOBJ = doctorOBJ.Where(x => x.specialitylist.Exists(x => x.id == specialityId)).ToList();
                }
                if (pageSize != 0)
                    return doctorOBJ.Skip(pageKey).Take(pageSize).ToList();
                else
                    return doctorOBJ;
            }
               

        }

        [HttpPut]
        [Authorize(Roles = "admin")]

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
        [Authorize(Roles = "admin")]

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
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,مريض")]
        public async Task<ActionResult<User>> PutPatientData(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }
            var patient = await _context.User.FindAsync(id);
            if(patient.phoneNumber!= user.phoneNumber)
            {
                var check = _context.User.Where(x => x.phoneNumber == user.phoneNumber).FirstOrDefault();
                if (check != null)
                    return Conflict();
            }
            patient.address = user.address;
            patient.nameAR = user.nameAR;
            patient.phoneNumber = user.phoneNumber;
            patient.regionId = user.regionId;
            patient.email = user.email;
            

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return patient;
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

            //return NoContent();
        }

        [HttpPut("{result}/{id}")]
        [Authorize]
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
        [Authorize(Roles = "admin")]

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
        [Authorize(Roles = "admin")]

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
        [Authorize(Roles = "admin")]

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
        [Authorize]
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
