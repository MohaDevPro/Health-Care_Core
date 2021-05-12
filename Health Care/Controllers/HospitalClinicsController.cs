using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly IWebHostEnvironment _environment;


        public HospitalClinicsController(Health_CareContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: api/HospitalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHospitalClinic()
        {
            return await (from hospital in _context.Hospitals
                          select new
                          {
                              id = hospital.hospitalId,
                              Name = hospital.Name,
                              Picture = hospital.Picture,
                              Backgroundimage=hospital.BackgoundImage,
                              Description = hospital.Description

                          }

                          ).ToListAsync();
        }

        [HttpGet("{hospitalId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetClinicAndDoctorByHospitalID(int hospitalId)
        {
            var hospital = await _context.Hospitals.Where(x => x.hospitalId == hospitalId).FirstOrDefaultAsync();
            var hospitalinfo_clinicinfo = await (from hospitalObj in _context.User
                                                 where hospitalObj.id == hospitalId
                                                 select new 
                                                 {
                                                     HospitalInfo = hospitalObj,
                                                     HospitalDepartmentsList = (from HospitalDepartment in _context.hospitalDepartments
                                                                               join departments in _context.departmentsOfHospitals
                                                                               on HospitalDepartment.DepatmentsOfHospitalID equals departments.id
                                                                               where HospitalDepartment.Hospitalid == hospital.id
                                                                                select new
                                                                                {
                                                                                    id = departments.id,
                                                                                    Name = departments.Name,
                                                                                }).ToList(),
                                                     HospitalClinicList = (from clinic in _context.ExternalClinic.Where(x => x.userId == hospitalId)
                                                                           select new
                                                                           {
                                                                               id = clinic.id,
                                                                               Name = clinic.Name,
                                                                           }
                                                                                  ).ToList(),
                                                     HospitalDoctorList = (from doctor in _context.Doctor
                                                                           join Clinicdoctor in _context.clinicDoctors on doctor.id equals Clinicdoctor.Doctorid
                                                                           join clinic in _context.ExternalClinic on Clinicdoctor.Clinicid equals clinic.id
                                                                           where clinic.userId == hospitalId
                                                                           select new
                                                                           {
                                                                               id = doctor.id,
                                                                               Name = doctor.name,
                                                                               appointmentPrice = doctor.appointmentPrice,
                                                                               numberOfAvailableAppointment = doctor.numberOfAvailableAppointment,
                                                                           }
                                                                                  ).ToList()


                                                 }).ToListAsync();





            //var hospitalinfo_clinicinfo = await (from hospitalObj in _context.User
            //                                     where hospitalObj.id == hospitalId
            //                                     select new HospitalClinicDoctorViewModel
            //                                     {
            //                                         HospitalInfo = hospitalObj,
            //                                         HospitalClinicInfoList = (from hospitalClinicObj in _context.HospitalClinic
            //                                                               join clinicTypeObj in _context.ClinicType
            //                                                               on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                                                               where hospitalClinicObj.hospitalId == hospitalId
            //                                                               select new HospitalClinic
            //                                                               {
            //                                                                   hospitalId = hospitalClinicObj.hospitalId,
            //                                                                   //clinicId = hospitalClinicObj.clinicId,
            //                                                                   //appointmentPrice = hospitalClinicObj.appointmentPrice,
            //                                                                   //numberOfAvailableAppointment = hospitalClinicObj.numberOfAvailableAppointment,
            //                                                               }).ToList(),


            //                                     }).ToListAsync();

            //var hospitalinfo_clinicinfo = await(from hospitalClinicObj in _context.HospitalClinic
            //                                     join hospitalObj in _context.User on hospitalClinicObj.hospitalId equals hospitalObj.id
            //                                     join clinicTypeObj in _context.ClinicType on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                                     join clinicDoctorObj in _context.clinicDoctors on clinicTypeObj.id equals clinicDoctorObj.Clinicid
            //                                     where hospitalClinicObj.hospitalId == hospitalId
            //                                     select new HospitalClinicDoctorViewModel
            //                                     {
            //                                         HospitalInfo = hospitalObj,
            //                                         ClinicInfo = clinicTypeObj,
            //                                         ClinicDoctorInfo = clinicDoctorObj,
            //                                         HospitalClinicInfo = hospitalClinicObj
            //                                     }).ToListAsync();

            //var hospitalinfo_clinicinfo = await (from hospitalClinicObj in _context.HospitalClinic
            //                               join hospitalObj in _context.User on hospitalClinicObj.hospitalId equals hospitalObj.id
            //                               join clinicTypeObj in _context.ClinicType on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                               join clinicDoctorObj in _context.clinicDoctors on clinicTypeObj.id equals clinicDoctorObj.Clinicid
            //                               where hospitalClinicObj.hospitalId == hospitalId
            //                               select new HospitalClinicDoctorViewModel
            //                               {
            //                                   HospitalInfo = hospitalObj ,
            //                                   ClinicInfo = clinicTypeObj ,
            //                                   ClinicDoctorInfo = clinicDoctorObj ,
            //                                   HospitalClinicInfo = hospitalClinicObj
            //                               }).ToListAsync();


            //var externalClinic = await (from clinic in _context.ExternalClinic.Where(x => x.userId == id)
            //                            select new
            //                            {
            //                                id = clinic.id,
            //                                Name = clinic.Name,
            //                                Picture = clinic.Picture,
            //                                specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
            //                                                  join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
            //                                                  where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
            //                                                  select specialit).ToList(),
            //                            }

            //              ).ToListAsync();

            return hospitalinfo_clinicinfo;
        }

        // GET: api/HospitalClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospitalClinic(int id)
        {
            var hospitalClinic = await _context.Hospitals.FindAsync(id);

            if (hospitalClinic == null)
            {
                return NotFound();
            }

            return hospitalClinic;
        }

        // PUT: api/HospitalClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalClinic(int id, Hospital hospitalClinic)
        {
            if (id != hospitalClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalClinicExists(id))
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

        // POST: api/HospitalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospitalClinic(Hospital hospitalClinic)
        {
            _context.Hospitals.Add(hospitalClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalClinic", new { id = hospitalClinic.id }, hospitalClinic);
        }

        // DELETE: api/HospitalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hospital>> DeleteHospitalClinic(int id)
        {
            var hospitalClinic = await _context.Hospitals.FindAsync(id);
            if (hospitalClinic == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(hospitalClinic);
            await _context.SaveChangesAsync();

            return hospitalClinic;
        }
        [HttpPut("{departmentid}/{hospitalid}")]
        public async Task<IActionResult> PutDepartmentsOfHospital(int departmentid,int hospitalid, [FromForm] DepartmentsOfHospital DepartmentsOfHospital, IFormFile Picture, IFormFile bg)
        {
            if (departmentid != DepartmentsOfHospital.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var hospitaldepartment =await _context.hospitalDepartments.FirstOrDefaultAsync(x => x.DepatmentsOfHospitalID == departmentid && x.Hospitalid == hospitalid);

                    try
                    {
                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "logo_" + hospitaldepartment.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        hospitaldepartment.Picture = @"\images\" + "logo_" + hospitaldepartment.id + "." + Picture.ContentType.Split('/')[1];
                        if (bg == null)
                        {
                            fileStream.Dispose();
                        }
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "bg_" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        hospitaldepartment.Background = @"\images\" + "bg" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        _context.Entry(DepartmentsOfHospital).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                        return CreatedAtAction("GetDepartmentsOfHospital", new { id = DepartmentsOfHospital.id }, DepartmentsOfHospital);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
    
                        {
                            throw;
                        }
                    }



            }
            return NoContent();
        }

        // POST: api/DepartmentsOfHospitals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{hospitalid}")]
        //[Authorize(Roles = "admin, DepartmentsOfHospital")]
        public async Task<ActionResult<DepartmentsOfHospital>> PostDepartmentsOfHospital(int hospitalid,[FromForm] DepartmentsOfHospital DepartmentsOfHospital, IFormFile Picture, IFormFile bg)
        {
            if (ModelState.IsValid)
            {

                    try
                    {
                        var hospitaldepartment = new HospitalDepartments();
                        hospitaldepartment.Hospitalid = hospitalid;
                        hospitaldepartment.DepatmentsOfHospitalID = DepartmentsOfHospital.id;
                        _context.hospitalDepartments.Add(hospitaldepartment);
                        await _context.SaveChangesAsync();

                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "logo_" + hospitaldepartment.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        hospitaldepartment.Picture = @"\images\" + "logo_" + hospitaldepartment.id + "." + Picture.ContentType.Split('/')[1];
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "bg_" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        hospitaldepartment.Background = @"\images\" + "bg" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        _context.Entry(hospitaldepartment).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }
                    }
                    catch (Exception)
                    {


                        throw;
                    }

              

            }

            return CreatedAtAction("GetDepartmentsOfHospital", new { id = DepartmentsOfHospital.id }, DepartmentsOfHospital);
        }

        // DELETE: api/DepartmentsOfHospitals/5
        [HttpDelete("{Departmeentid}/{hospitalid}")]
        //[Authorize(Roles = "admin, DepartmentsOfHospital")]
        public async Task<ActionResult<HospitalDepartments>> DeleteDepartmentsOfHospital(int Departmeentid,int hospitalid)
        {
            var hospitaldepartment = await _context.hospitalDepartments.FirstOrDefaultAsync(x=>x.Hospitalid==hospitalid && x.DepatmentsOfHospitalID==Departmeentid);
            if (hospitaldepartment == null)
            {
                return NotFound();
            }
            //var departments = await _context.ExternalClinic.Where(d => d.HospitalDepartmentsID == hospitaldepartmentid.id).ToListAsync();
            //foreach (Departments dep in departments)
            //{
            //    dep.Active = false;
            //}
            _context.hospitalDepartments.Remove(hospitaldepartment);
            //DepartmentsOfHospital.isActive = false;
            await _context.SaveChangesAsync();
            //System.IO.File.Delete(_environment.WebRootPath + DepartmentsOfHospital.Picture);
            //System.IO.File.Delete(_environment.WebRootPath + DepartmentsOfHospital.BackgroundPicture);
            return hospitaldepartment;
        }


        private bool HospitalClinicExists(int id)
        {
            return _context.Hospitals.Any(e => e.id == id);
        }
    }
}
