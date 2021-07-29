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
                          join user in _context.User on hospital.UserId equals user.id 
                          
                          where hospital.active == true
                          select new
                          {
                              id = hospital.id,
                              Name = hospital.Name,
                              Picture = hospital.Picture,
                              user.regionId,
                              departmentsList = (from HospitalDep in _context.hospitalDepartments
                                                join departments in _context.departmentsOfHospitals on HospitalDep.DepatmentsOfHospitalID equals departments.id
                                                where HospitalDep.Hospitalid == hospital.id && departments.active == true 
                                                select departments).ToList(),
                              BackgroundImage =hospital.BackgoundImage,
                              Description = hospital.Description
                          }

                          ).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHospitalClinicForAdmin()
        {
            return await (from hospital in _context.Hospitals
                          where hospital.active == true
                          select new
                          {
                              id = hospital.id,
                              Name = hospital.Name,

                          }

                          ).ToListAsync();
        }


        [HttpGet("{hospitalId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetClinicAndDoctorByHospitalID(int hospitalId)
        {
            var hospital = await _context.Hospitals.Where(x => x.UserId == hospitalId && x.active ==true).FirstOrDefaultAsync();
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
                                                                           where doctor.active == true
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospitalClinicByuserID(int id)
        {
            var hospitalClinic = await _context.Hospitals.FirstOrDefaultAsync(x=>x.UserId==id);

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
                        hospitaldepartment.Background = @"\images\" + "bg_" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1];
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
        public async Task<ActionResult<object>> PostDepartmentsOfHospital(int hospitalid,[FromForm] DepartmentsOfHospital DepartmentsOfHospital, IFormFile Picture, IFormFile bg)
        {
            var hospitaldepartment = new HospitalDepartments();
            if (ModelState.IsValid)
            {

                    try
                    {
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
                        hospitaldepartment.Background = @"\images\" + "bg_" + hospitaldepartment.id + "." + bg.ContentType.Split('/')[1];
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
            return CreatedAtAction("GetDepartmentsOfHospital", new { id = hospitaldepartment.id }, new { hospitalDepartmentid= hospitaldepartment.id,DepartmentsOfHospital.id,DepartmentsOfHospital.Name });
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



        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternalClinic(int id, [FromForm] ExternalClinic ExternalClinic, IFormFile Picture, IFormFile bg)
        {
            if (id != ExternalClinic.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {

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
                        fileStream = System.IO.File.Create(path + "ExternalClinic_logo_" + ExternalClinic.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        ExternalClinic.Picture = @"\images\" + "ExternalClinic_logo_" + ExternalClinic.id + "." + Picture.ContentType.Split('/')[1];
                        if (bg == null)
                        {
                            fileStream.Dispose();
                        }
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "bg_" + ExternalClinic.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        ExternalClinic.BackgroundImage = @"\images\" + "bg_" + ExternalClinic.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        _context.Entry(ExternalClinic).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                    return CreatedAtAction("GetExternalClinic", new { id = ExternalClinic.id }, ExternalClinic);
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

        // POST: api/ExternalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //[Authorize(Roles = "admin, ExternalClinic")]
        public async Task<ActionResult<ExternalClinic>> PostExternalClinic([FromForm] ExternalClinic ExternalClinic, IFormFile Picture, IFormFile bg)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    _context.ExternalClinic.Add(ExternalClinic);
                    await _context.SaveChangesAsync();

                    string path = _environment.WebRootPath + @"\images\";
                    FileStream fileStream;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "ExternalClinic_logo_" + ExternalClinic.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        ExternalClinic.Picture = @"\images\" + "ExternalClinic_logo_" + ExternalClinic.id + "." + Picture.ContentType.Split('/')[1];
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "ExternalClinic_bg_" + ExternalClinic.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        ExternalClinic.BackgroundImage = @"\images\" + "ExternalClinic_bg_" + ExternalClinic.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        _context.Entry(ExternalClinic).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {


                    throw;
                }



            }

            return CreatedAtAction("GetExternalClinic", new { id = ExternalClinic.id }, ExternalClinic);
        }

        // DELETE: api/ExternalClinics/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin, ExternalClinic")]
        public async Task<ActionResult<ExternalClinic>> DeleteExternalClinic(int id)
        {
            var ExternalClinic = await _context.ExternalClinic.FirstOrDefaultAsync(x => x.id == id);
            if (ExternalClinic == null)
            {
                return NotFound();
            }
            //var departments = await _context.ExternalClinic.Where(d => d.ExternalClinicID == hospitaldepartmentid.id).ToListAsync();
            //foreach (Departments dep in departments)
            //{
            //    dep.Active = false;
            //}
            _context.ExternalClinic.Remove(ExternalClinic);
            //ExternalClinic.isActive = false;
            await _context.SaveChangesAsync();
            //System.IO.File.Delete(_environment.WebRootPath + ExternalClinic.Picture);
            //System.IO.File.Delete(_environment.WebRootPath + ExternalClinic.BackgroundPicture);
            return ExternalClinic;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, [FromForm] Doctor Doctor, IFormFile Picture, IFormFile bg)
        {
            if (id != Doctor.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {

                try
                {
                    var d = _context.Doctor.Find(id);
                    d.appointmentPrice = Doctor.appointmentPrice;
                    d.name = Doctor.name;
                    d.numberOfAvailableAppointment = Doctor.numberOfAvailableAppointment;
                    string path = _environment.WebRootPath + @"\images\";
                    FileStream fileStream;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "Doctor_logo_" + Doctor.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        d.Picture = @"\images\" + "Doctor_logo_" + Doctor.id + "." + Picture.ContentType.Split('/')[1];
                        if (bg == null)
                        {
                            fileStream.Dispose();
                        }
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "bg_doctor_" + Doctor.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        d.backgroundImage = @"\images\" + "bg_doctor_" + Doctor.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        //_context.Entry(Doctor).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                    return CreatedAtAction("GetDoctor", new { id = Doctor.id }, Doctor);
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

        // POST: api/Doctors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{ClinicID}")]
        //[Authorize(Roles = "admin, Doctor")]
        public async Task<ActionResult<Doctor>> PostDoctor(int ClinicID, [FromForm] Doctor Doctor, IFormFile Picture, IFormFile bg)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    _context.Doctor.Add(Doctor);
                    await _context.SaveChangesAsync();

                    _context.clinicDoctors.Add(new ClinicDoctor() { Clinicid = ClinicID, Doctorid = Doctor.id });
                    string path = _environment.WebRootPath + @"\images\";
                    FileStream fileStream;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "Doctor_logo_" + Doctor.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        Doctor.Picture = @"\images\" + "Doctor_logo_" + Doctor.id + "." + Picture.ContentType.Split('/')[1];
                    }
                    if (bg != null)
                    {
                        fileStream = System.IO.File.Create(path + "Doctor_bg_" + Doctor.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        Doctor.backgroundImage = @"\images\" + "Doctor_bg_" + Doctor.id + "." + bg.ContentType.Split('/')[1];
                    }
                    if (Picture != null || bg != null)
                    {
                        _context.Entry(Doctor).State = EntityState.Modified;

                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {


                    throw;
                }



            }

            return CreatedAtAction("GetDoctor", new { id = Doctor.id }, Doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin, Doctor")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var Doctor = await _context.Doctor.FirstOrDefaultAsync(x => x.id == id);
            if (Doctor == null)
            {
                return NotFound();
            }
            //var departments = await _context.Doctor.Where(d => d.DoctorID == hospitaldepartmentid.id).ToListAsync();
            //foreach (Departments dep in departments)
            //{
            //    dep.Active = false;
            //}
            _context.Doctor.Remove(Doctor);
            //Doctor.isActive = false;
            await _context.SaveChangesAsync();
            //System.IO.File.Delete(_environment.WebRootPath + Doctor.Picture);
            //System.IO.File.Delete(_environment.WebRootPath + Doctor.BackgroundPicture);
            return Doctor;
        }


        private bool HospitalClinicExists(int id)
        {
            return _context.Hospitals.Any(e => e.id == id);
        }
    }
}
