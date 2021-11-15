using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExternalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly IWebHostEnvironment _environment;


        public ExternalClinicsController(Health_CareContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ExternalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicAll ()
        {
            return await _context.ExternalClinic.Where(x => x.active == true).ToListAsync();
        }
        
        [HttpGet("{regionId}/{specialityId}/{pageKey}/{pageSize}/{byString}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinic(int regionId, int specialityId, int pageKey, int pageSize, string byString)
        {
            byString = byString.Replace("empty", "");

            var clinics= await (from clinic in _context.ExternalClinic join user in _context.User on clinic.userId equals user.id
                          where clinic.active == true
                          select new
                          {
                              id = clinic.id,
                              Name = clinic.Name,
                              Picture = clinic.Picture,
                              user.regionId,
                              BackgroundImage=clinic.BackgroundImage,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == clinic.id  && specialitydoctor.Roleid == 1
                                                select specialit).ToList(),
                          }

                          ).Where(x=>x.Name.Contains(byString)).ToListAsync();
            if (regionId != 0)
            {
                clinics = clinics.Where(x => x.regionId == regionId).ToList();
            }
            if (specialityId != 0)
            {
                clinics = clinics.Where(x => x.specialitylist.Exists(x => x.id == specialityId)).ToList();
            }
            
            if (pageSize != 0)
            {
                return clinics.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return clinics.ToList();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicForAdmin()
        {
            return await (from clinic in _context.ExternalClinic
                          where clinic.active == true
                          select new
                          {
                              id = clinic.id,
                              Name = clinic.Name,
                          }

                          ).ToListAsync();
        }


        // GET: api/ExternalClinics/5
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByDoctorID(int id, int pageKey, int pageSize)
        {
            var externalClinic = await (from clinic in _context.ExternalClinic where clinic.active == true join Clinicdoctor in _context.clinicDoctors on clinic.id equals Clinicdoctor.Clinicid
                                        where Clinicdoctor.Doctorid==id
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            BackgroundImage = clinic.BackgroundImage,

                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid==1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

            if (pageSize != 0)
            {
                return externalClinic.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return externalClinic.ToList();
        }

        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByHospitalID(int id, int pageKey, int pageSize)
        {
            var userid = _context.Hospitals.FirstOrDefault(x => x.id == id).UserId;
            var externalClinic = await (from clinic in _context.ExternalClinic.Where(x=>x.userId==userid && x.active == true)
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            BackgroundImage = clinic.BackgroundImage,
                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

           
            if (pageSize != 0)
            {
                return externalClinic.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return externalClinic.ToList();
        }
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByDepartmentID(int id, int pageKey, int pageSize)
        {
            var externalClinic = await (from clinic in _context.ExternalClinic 
                                        where clinic.HospitalDepartmentsID==id &&  clinic.active == true
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            BackgroundImage = clinic.BackgroundImage,
                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

           
            if (pageSize != 0)
            {
                return externalClinic.Skip(pageKey).Take(pageSize).ToList();
            }
            else
                return externalClinic.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetExternalClinic(int id)
        {
            var clinic = await _context.ExternalClinic.FindAsync(id);
            return new
            {
                id = clinic.id,
                Name = clinic.Name,
                Picture = clinic.Picture,
                BackgroundImage = clinic.BackgroundImage,
                 clinic.userId,
                specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                  join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                  where specialitydoctor.Doctorid == clinic.id && specialitydoctor.Roleid == 1
                                  select specialit).ToList(),
            };

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalClinic>> GetExternalClinicByuserID(int id)
        {
            var clinic = await _context.ExternalClinic.FirstOrDefaultAsync(x=>x.userId==id);
            if (clinic == null)
            {
                return NotFound();
            }
            return clinic;
            //return new
            //{
            //    id = clinic.id,
            //    Name = clinic.Name,
            //    Picture = clinic.Picture,
            //    BackgroundImage = clinic.BackgoundImage,

            //};

        }

        public async Task<ActionResult<IEnumerable<ExternalClinic>>> GetDisabled()
        {
            
            return await (from clinic in _context.ExternalClinic
                          join user in _context.User on clinic.userId equals user.id
                          where clinic.active == false && user.active == false
                          select clinic).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<ExternalClinic> externalClinic)
        {
            if (externalClinic.Count == 0)
                return NoContent();

            try
            {
                foreach (ExternalClinic item in externalClinic)
                {
                    ExternalClinic s = _context.ExternalClinic.Where(s => s.id == item.id).FirstOrDefault();
                    var user = _context.User.Where(x => x.id == s.userId).FirstOrDefault();
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

        // PUT: api/ExternalClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternalClinic(int id, ExternalClinic externalClinic)
        {
            if (id != externalClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(externalClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalClinicExists(id))
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

        // POST: api/ExternalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExternalClinic>> PostExternalClinic(ExternalClinic externalClinic)
        {
            _context.ExternalClinic.Add(externalClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExternalClinic", new { id = externalClinic.id }, externalClinic);
        }
        [HttpPost]
        //[Authorize(Roles = "admin, ExternalClinic")]
        public async Task<ActionResult<ExternalClinic>> PostExternalClinicWithImages([FromForm] ExternalClinic externalClinic, IFormFile Picture, IFormFile bg)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null && bg != null)
                {
                    try
                    {
                        _context.ExternalClinic.Add(externalClinic);
                        await _context.SaveChangesAsync();

                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileStream = System.IO.File.Create(path + "logo_externalClinic_" + externalClinic.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();

                        fileStream.Close();
                        externalClinic.Picture = @"\images\" + "logo_externalClinic_" + externalClinic.id + "." + Picture.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "bg_externalClinic_" + externalClinic.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        externalClinic.BackgroundImage = @"\images\" + "bg_externalClinic_" + externalClinic.id + "." + bg.ContentType.Split('/')[1];
                        _context.Entry(externalClinic).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {


                        throw;
                    }

                }
                else
                {
                    return BadRequest();
                }

            }

            return CreatedAtAction("GetExternalClinic", new { id = externalClinic.id }, externalClinic);
        }

        // DELETE: api/ExternalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternalClinic>> DeleteExternalClinic(int id)
        {
            var externalClinic = await _context.ExternalClinic.FindAsync(id);
            if (externalClinic == null)
            {
                return NotFound();
            }
            var user = _context.User.Where(x => x.id == externalClinic.userId).FirstOrDefault();
            user.active = false;
            externalClinic.active = false;
            //_context.ExternalClinic.Remove(externalClinic);
            await _context.SaveChangesAsync();

            return externalClinic;
        }

        private bool ExternalClinicExists(int id)
        {
            return _context.ExternalClinic.Any(e => e.id == id);
        }
    }
}
