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
    public class ExternalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ExternalClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/ExternalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinic()
        {
            return await (from clinic in _context.ExternalClinic
                          select new
                          {
                              id = clinic.id,
                              Name = clinic.Name,
                              Picture = clinic.Picture,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
                                                select specialit).ToList(),
                          }

                          ).ToListAsync();
        }


        // GET: api/ExternalClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByDoctorID(int id)
        {
            var externalClinic = await (from clinic in _context.ExternalClinic join Clinicdoctor in _context.clinicDoctors on clinic.id equals Clinicdoctor.Clinicid
                                        where Clinicdoctor.Doctorid==id
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid==1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

            return externalClinic;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByHospitalID(int id)
        {
            var externalClinic = await (from clinic in _context.ExternalClinic.Where(x=>x.userId==id)
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

            return externalClinic;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetExternalClinicByDepartmentID(int id)
        {
            var externalClinic = await (from clinic in _context.ExternalClinic join DepartmentHospital in _context.hospitalDepartments on clinic.HospitalDepartmentsID equals DepartmentHospital.id
                                        where DepartmentHospital.DepatmentsOfHospitalID==id
                                        select new
                                        {
                                            id = clinic.id,
                                            Name = clinic.Name,
                                            Picture = clinic.Picture,
                                            specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                              join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                              where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
                                                              select specialit).ToList(),
                                        }

                          ).ToListAsync();

            return externalClinic;
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
                specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                  join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                  where specialitydoctor.Doctorid == clinic.id && specialitydoctor.Roleid == 1
                                  select specialit).ToList(),
            };

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

        // DELETE: api/ExternalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternalClinic>> DeleteExternalClinic(int id)
        {
            var externalClinic = await _context.ExternalClinic.FindAsync(id);
            if (externalClinic == null)
            {
                return NotFound();
            }

            _context.ExternalClinic.Remove(externalClinic);
            await _context.SaveChangesAsync();

            return externalClinic;
        }

        private bool ExternalClinicExists(int id)
        {
            return _context.ExternalClinic.Any(e => e.id == id);
        }
    }
}
