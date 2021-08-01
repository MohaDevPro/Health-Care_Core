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

        [HttpGet("{id}/{isfrom}")]
        public async Task<ActionResult<IEnumerable<User>>> GetDoctorClinicReqeustsByuserID(int id,bool isfrom)
        {
            if (isfrom)
            {
                return await (from request in _context.DoctorClinicReqeusts
                              join user in _context.User on request.ToID equals user.id
                              where request.FromID == id && request.IsAccepted==false && request.IsCanceled==false 
                              select user).ToListAsync();
            }
            return await (from request in _context.DoctorClinicReqeusts 
                          join user in _context.User on request.FromID equals user.id 
                          where request.ToID== id && request.IsAccepted == false && request.IsCanceled == false
                          select user).ToListAsync();
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
            doctorClinicReqeust.CancelResoun = Resoun;
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
