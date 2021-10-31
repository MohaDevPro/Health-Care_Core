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
    public class DoctorClinicReqeustsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public DoctorClinicReqeustsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/DoctorClinicReqeusts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorClinicReqeust>>> GetDoctorClinicReqeusts()
        {
            return await _context.DoctorClinicReqeusts.ToListAsync();
        }

        [HttpGet("{id}/{isfrom}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorClinicReqeustsByuserID(int id,bool isfrom, int pageKey, int pageSize)
        {

            var users = new List<UserRequestVM>();
            if (isfrom)
            {
                users = await (from request in _context.DoctorClinicReqeusts
                               join user in _context.User on request.ToID equals user.id
                               where request.FromID == id
                               select new UserRequestVM
                               {
                                   user = user,
                                   request = request
                               }).ToListAsync();
            }
            else
            {
                users = await (from request in _context.DoctorClinicReqeusts
                               join user in _context.User on request.FromID equals user.id
                               where request.ToID == id
                               select new UserRequestVM
                               {
                                   user = user,
                                   request = request,
                               }).ToListAsync();
            }
            var finalusers = new List<object>();
            var doctors = _context.Doctor.ToList();
            var clinics = _context.ExternalClinic.ToList();
            for (int i = 0; i < users.Count; i++)
            {
                var clinicid = 0;
                if (users[i].user.Roleid == 2)
                {
                    users[i].user.nameAR = clinics.FirstOrDefault(x => x.userId == users[i].user.id).Name;

                }
                else if (users[i].user.Roleid == 3)
                {
                    users[i].user.nameAR = clinics.FirstOrDefault(x => x.id == users[i].request.ClinicID).Name;
                    clinicid = clinics.FirstOrDefault(x => x.id == users[i].request.ClinicID).id;
                }
                else if (users[i].user.Roleid == 5)
                {
                    users[i].user.nameAR = doctors.FirstOrDefault(x => x.Userid == users[i].user.id).name;
                }
                finalusers.Add(new
                {
                    users[i].user.id,
                    users[i].user.nameAR,
                    users[i].user.phoneNumber,
                    users[i].user.address,
                    users[i].request.IsAccepted,
                    users[i].request.IsCanceled,
                    users[i].request.CancelResoun,
                    users[i].user.Roleid,
                    clinicID = clinicid
                }) ;
            }
            return finalusers.Skip(pageKey).Take(pageSize).ToList();

        }
        // GET: api/DoctorClinicReqeusts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorClinicReqeust>> GetDoctorClinicReqeust(int id)
        {
            var doctorClinicReqeust = await _context.DoctorClinicReqeusts.FindAsync(id);

            if (doctorClinicReqeust == null)
            {
                return NotFound();
            }

            return doctorClinicReqeust;
        }

        // PUT: api/DoctorClinicReqeusts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{fromID}/{ToID}")]
        public async Task<IActionResult> AcceptDoctorClinicRequests(int fromID,int ToID)
        {

            var doctorClinicReqeust = _context.DoctorClinicReqeusts.FirstOrDefault(x => x.FromID == fromID && x.ToID == ToID);

            doctorClinicReqeust.IsAccepted = true;
            var listid = new List<int>() { fromID, ToID };
            int Clinicid=0;
            int Doctorid=0;
            foreach(var i in listid)
            {
                var tt = _context.User.FirstOrDefault(x => x.id == i);
                if (tt.Roleid == 2)
                {
                    Clinicid = _context.ExternalClinic.FirstOrDefault(x => x.userId == tt.id).id;

                }else if (tt.Roleid == 5)
                {
                    Doctorid = _context.Doctor.FirstOrDefault(x => x.Userid == tt.id).id;
                }
                else
                {
                    Clinicid = _context.ExternalClinic.FirstOrDefault(x => x.id == doctorClinicReqeust.ClinicID).id;
                }
            }

            _context.Add(new ClinicDoctor { Clinicid = Clinicid, Doctorid = Doctorid });
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{fromID}/{ToID}/{Resoun}")]
        public async Task<IActionResult> CancelDoctorClinicRequests(int fromID, int ToID,string Resoun)
        {

            var doctorClinicReqeust = _context.DoctorClinicReqeusts.FirstOrDefault(x => x.FromID == fromID && x.ToID == ToID);

            doctorClinicReqeust.IsCanceled = true;
            doctorClinicReqeust.IsAccepted = false;
            doctorClinicReqeust.CancelResoun = Resoun;
            var listid = new List<int>() { fromID, ToID };
            int Clinicid = 0;
            int Doctorid = 0;
            foreach (var i in listid)
            {
                var tt = _context.User.FirstOrDefault(x => x.id == i);
                if (tt.Roleid == 2)
                {
                    Clinicid = _context.ExternalClinic.FirstOrDefault(x => x.userId == tt.id).id;

                }
                else if (tt.Roleid == 5)
                {
                    Doctorid = _context.Doctor.FirstOrDefault(x => x.Userid == tt.id).id;
                }
                else
                {
                    Clinicid = _context.ExternalClinic.FirstOrDefault(x => x.id == doctorClinicReqeust.ClinicID).id;
                }
            }
            var doctorclinic = _context.clinicDoctors.FirstOrDefault(x => x.Doctorid == Doctorid && x.Clinicid == Clinicid);
            if (doctorclinic != null)
            {
                _context.Remove(doctorclinic);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/DoctorClinicReqeusts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DoctorClinicReqeust>> PostDoctorClinicReqeust(DoctorClinicReqeust doctorClinicReqeust)
        {
            _context.DoctorClinicReqeusts.Add(doctorClinicReqeust);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctorClinicReqeust", new { id = doctorClinicReqeust.id }, doctorClinicReqeust);
        }

        // DELETE: api/DoctorClinicReqeusts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorClinicReqeust>> DeleteDoctorClinicReqeust(int id)
        {
            var doctorClinicReqeust = await _context.DoctorClinicReqeusts.FindAsync(id);
            if (doctorClinicReqeust == null)
            {
                return NotFound();
            }

            _context.DoctorClinicReqeusts.Remove(doctorClinicReqeust);
            await _context.SaveChangesAsync();

            return doctorClinicReqeust;
        }

        private bool DoctorClinicReqeustExists(int id)
        {
            return _context.DoctorClinicReqeusts.Any(e => e.id == id);
        }
    }
}
