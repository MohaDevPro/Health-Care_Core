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
//using Health_Care.Migrations;
using Microsoft.AspNetCore.Authorization;



namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "admin, دكتور")]
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
                          where doctor.active == true
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              BackgroundImage = doctor.backgroundImage,
                              identificationImage = doctor.identificationImage,
                              graduationCertificateImage = doctor.graduationCertificateImage,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }
                          ).ToListAsync();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDisabled()
        {
            return await (from doctor in _context.Doctor
                                        join user in _context.User on doctor.Userid equals user.id
                                        where doctor.active == false && user.active == false
                                        select doctor).ToListAsync();
        }


        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> RestoreService(List<Doctor> doctor)
        {
            if (doctor.Count == 0)
                return NoContent();

            try
            {
                foreach (Doctor item in doctor)
                {
                    var s = _context.Doctor.Where(s => s.id == item.id).FirstOrDefault();
                    var user = _context.User.Where(x => x.id == s.Userid).FirstOrDefault();
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetNamesOfDoctors()
        {

            return await (from doctor in _context.Doctor
                          where doctor.active == true
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                          }
                          ).ToListAsync();
        }


        [HttpGet("{patientId}/{regionId}/{specialityId}/{pageKey}/{pageSize}/{byString}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorsWithFavorite(int patientId,int regionId,int specialityId,int pageKey,int pageSize,string byString)
        {
            byString = byString.Replace("empty","");
            //var favorite = (from PatientFavorite in _context.Favorite
            //                where PatientFavorite.PatientId == patientId
            //                select PatientFavorite).ToList();
            var favorite = _context.Favorite.Where(x => x.PatientId == patientId && x.type == "doctor").ToList();
            
            var doctors = await (from doctor in _context.Doctor
                                 where doctor.active == true
                                 select new
                                 {
                                     id = doctor.id,
                                     Name = doctor.name,
                                     Picture = doctor.Picture,
                                     regionId=(from dr in _context.Doctor 
                                               join cldr in _context.clinicDoctors on dr.id equals cldr.Doctorid
                                               join cl in _context.ExternalClinic on cldr.Clinicid equals cl.id
                                               join usr in _context.User on cl.userId equals usr.id where dr.id== doctor.id select usr.regionId).ToList(),
                                     userId = doctor.Userid,
                                     specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                       join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                       where specialitydoctor.Doctorid == doctor.id && specialitydoctor.Roleid == 0
                                                       select specialit).ToList(),
                                 }
                          ).Where(x=>x.Name.Contains(byString)).ToListAsync();
            if (regionId != 0)
            {
                doctors = doctors.Where(x => x.regionId.Contains(regionId)).ToList();
            }
            if (specialityId != 0)
            {
                doctors = doctors.Where(x => x.specialitylist.Exists(x => x.id == specialityId)).ToList();
            }
            var listFinalResult = new List<object>();
            bool flag = false;
            foreach (var doctor in doctors)
            {
                
               
                foreach (var favorit in favorite)
                {
                    if (favorit.UserId == doctor.id)
                        flag = true;
                }
                var docrotwithfavorite = new
                {
                    doctor.id,
                    doctor.Name,
                    doctor.Picture,
                    doctor.regionId,
                    doctor.userId,
                    doctor.specialitylist,
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
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorinsideClinicBasedonClinicID(int id, int pageKey, int pageSize)
        {

            var doctors = await (from doctor in _context.Doctor
                                 where doctor.active == true
                                 join Clinic in _context.ExternalClinic on doctor.Userid equals Clinic.userId
                                 where Clinic.id == id
                                 select new
                                 {
                                     id = doctor.id,
                                     Name = doctor.name,
                                     Picture = doctor.Picture,
                                     BackgroundImage = doctor.backgroundImage,
                                     AppointmentPrice = doctor.appointmentPrice,
                                     NumberofAvailableAppointment = doctor.numberOfAvailableAppointment,
                                     specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                       join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                       where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                       select specialit).ToList(),
                                 }

                          ).ToListAsync();
            if (pageSize != 0)
            {
                return doctors.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return doctors.ToList();
        }

        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorBasedOnClinicID(int id,int pageKey,int pageSize)
        {

            var doctors =  await (from doctor in _context.Doctor where doctor.active == true 
                          join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                          where Clinicdoctor.Clinicid==id
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              BackgroundImage=doctor.backgroundImage,
                              AppointmentPrice = doctor.appointmentPrice,
                              NumberofAvailableAppointment = doctor.numberOfAvailableAppointment,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                          }

                          ).ToListAsync();
            if (pageSize != 0)
            {
                return doctors.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return doctors.ToList();
        }
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorBasedOnHospitalID(int id, int pageKey, int pageSize)
        {
            var userid = _context.Hospitals.FirstOrDefault(x=>x.id==id).UserId;
            var doctors=(from doctor in _context.Doctor
                         where doctor.active == true
                          join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                          join clinic in _context.ExternalClinic on Clinicdoctor.Clinicid equals clinic.id
                          where clinic.userId == userid
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              BackgroundImage = doctor.backgroundImage,
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
            return notRepitted.Skip(pageKey).Take(pageSize).ToList();
        }
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorBasedOnDepartmentID(int id, int pageKey, int pageSize)
        {
            var doctors = (from doctor in _context.Doctor
                           where doctor.active == true
                           join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                           join clinic in _context.ExternalClinic on Clinicdoctor.Clinicid equals clinic.id
                           where clinic.HospitalDepartmentsID == id && clinic.active
                           select new
                           {
                               id = doctor.id,
                               Name = doctor.name,
                               Picture = doctor.Picture,
                               BackgroundImage = doctor.backgroundImage,
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
            return notRepitted.Skip(pageKey).Take(pageSize).ToList();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetDoctor(int id)
        {
            var Doctor =await _context.Doctor.FindAsync(id);
            var doctor =new {
                id = id,
                Name = Doctor.name,
                Doctor.appointmentPrice,
                Picture=Doctor.Picture,
                BackgroundImage = Doctor.backgroundImage,
                Doctor.active,
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorBasedOnuserID(int id)
        {
            var Doctor = await _context.Doctor.FirstOrDefaultAsync(x=>x.Userid==id);
            //var doctor = new
            //{
            //    id = id,
            //    Name = Doctor.name,
            //    Picture = Doctor.Picture,
            //    BackgroundImage = Doctor.backgroundImage,

            //};

            if (Doctor == null)
            {
                return NotFound();
            }

            return Doctor;
        }
        // PUT: api/Doctors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "دكتور,مستشفى,عيادة")]
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
        [HttpPut("{id}/{status}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutDoctorStatus(int id, bool status)
        {

            var doctor = _context.Doctor.Find(id);
            if (doctor == null)
            {
                return BadRequest();
            }
            doctor.active = status;
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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.id }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var user = _context.User.Where(x => x.id == doctor.Userid).FirstOrDefault();
            user.active = false;
            doctor.active = false;
            //_context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.id == id);
        }
    }
}
