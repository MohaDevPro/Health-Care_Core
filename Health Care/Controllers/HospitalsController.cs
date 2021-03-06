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
using Microsoft.AspNetCore.Authorization;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly IWebHostEnvironment _environment;

        public HospitalsController(Health_CareContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            return await _context.Hospitals.Where(s => s.active == true).ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetHospital(int id)
        {

            var hospitalClinic = await _context.Hospitals.Where(x => x.id == id).FirstOrDefaultAsync();

            if (hospitalClinic == null)
            {
                return NotFound();
            }

            return new
            {
                id = hospitalClinic.id,
                Name = hospitalClinic.Name,
                Picture = hospitalClinic.Picture,
                BackgroundImage = hospitalClinic.BackgoundImage,
                Description = hospitalClinic.Description

            };
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetDisabled()
        {
            return await (from hospital in _context.Hospitals
                          join user in _context.User on hospital.UserId equals user.id
                          where hospital.active == false && user.active == false
                          select hospital).ToListAsync();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> RestoreService(List<Hospital> hospital)
        {
            if (hospital.Count == 0)
                return NoContent();

            try
            {
                foreach (Hospital item in hospital)
                {
                    var s = _context.Hospitals.Where(s => s.id == item.id).FirstOrDefault();
                    var user = _context.User.Where(x => x.id == s.UserId).FirstOrDefault();
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

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,مستشفى")]
        public async Task<IActionResult> PutHospital(int id, Hospital hospital)
        {
            if (id != hospital.id)
            {
                return BadRequest();
            }

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
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
        [Authorize(Roles = "admin,مستشفى")]
        public async Task<IActionResult> PutHospitalWithImages(int id, [FromForm] Hospital hospital, IFormFile Picture, IFormFile bg)
        {

            User user = _context.User.Where(d => d.id == hospital.UserId).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            Hospital hospital2 = _context.Hospitals.Where(d => d.id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (hospital2 == null)
                {
                    return NotFound();
                }
                hospital2.Name = hospital.Name;
                hospital2.Description = hospital.Description;
                user.nameAR = hospital2.Name;
                if (Picture != null && bg != null)
                {
                    
                    try
                    {
                        
                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileStream = System.IO.File.Create(path + "logo_hospital_" + hospital2.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        hospital2.Picture = @"\images\" + "logo_hospital_" + hospital2.id + "." + Picture.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "bg_hospital_" + hospital2.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        hospital2.BackgoundImage = @"\images\" + "bg_hospital_" + hospital2.id + "." + bg.ContentType.Split('/')[1];


                        //_context.hospital.Add(hospital);
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
               

            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Hospitals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin,مستشفى")]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospital", new { id = hospital.id }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hospital>> DeleteHospital(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            var user = _context.User.Where(x => x.id == hospital.UserId).FirstOrDefault();
            user.active = false;
            hospital.active = false;
            //_context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return hospital;
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.id == id);
        }
    }
}
