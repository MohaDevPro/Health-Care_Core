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
    public class DoctorsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public DoctorsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Doctors

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctor()
        {

            return await (from doctor in _context.Doctor
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              Backgroundimage = doctor.backgroundImage,
                              identificationImage = doctor.identificationImage,
                              graduationCertificateImage = doctor.graduationCertificateImage,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }
                          ).ToListAsync();
        }


        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorsWithFavorite(int patientId)
        {
            var favorite = (from PatientFavorite in _context.Favorite join doc in _context.Doctor on PatientFavorite.UserId equals doc.Userid where PatientFavorite.PatientId == patientId select PatientFavorite ).ToList();

            
            
            var doctors= await(from doctor in _context.Doctor
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              userId=doctor.Userid,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic==true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }
                          ).ToListAsync();
            var listFinalResult =new List<object>();
            bool flag = false;
            foreach(var i in doctors)
            {
                foreach(var j in favorite)
                {
                    if (j.UserId == i.userId)
                        flag = true;
                }
                var docrotwithfavorite = new
                {
                    i.id,
                    i.Name,
                    i.Picture,
                    i.specialitylist,
                    isFavorite = flag ? true : false,
                };
                listFinalResult.Add(docrotwithfavorite);
                flag = false;
            }
            return listFinalResult;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorBasedOnClinicID(int id)
        {
            return await (from doctor in _context.Doctor join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                          where Clinicdoctor.Clinicid==id
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              Backgroundimage=doctor.backgroundImage,
                              AppointmentPrice = doctor.appointmentPrice,
                              NumberofAvailableAppointment = doctor.numberOfAvailableAppointment,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }

                          ).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorBasedOnHospitalID(int id)
        {
            var doctors=(from doctor in _context.Doctor
                          join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                          join clinic in _context.ExternalClinic on Clinicdoctor.Clinicid equals clinic.id
                          where clinic.userId == id
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              Backgroundimage = doctor.backgroundImage,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }

                          );
            List<object> notRepitted = new List<object>();
            List<int> checkIDS = new List<int>();

            foreach (var i in doctors)
            {
                if (!checkIDS.Contains(i.id))
                {
                    notRepitted.Add(i);
                    checkIDS.Add(i.id);
                }
            }
            return notRepitted;
        }
        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetDoctor(int id)
        {
            var Doctor =await _context.Doctor.FindAsync(id);
            var doctor =new {
                id = id,
                Name = Doctor.name,
                Picture=Doctor.Picture,
                Backgroundimage = Doctor.backgroundImage,

                specialitylist =  (from specialitydoctor in _context.SpeciallyDoctors
                                 join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                 where specialitydoctor.Doctorid == id && specialitydoctor.Roleid == 0
                                   select specialit).ToList(),
                               };

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.id }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.id == id);
        }
    }
}
