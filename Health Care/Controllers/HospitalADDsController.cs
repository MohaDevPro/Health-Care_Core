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
    public class HospitalADDsController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly IWebHostEnvironment _environment;

        public HospitalADDsController(Health_CareContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: api/HospitalADDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalADDs>>> GetHospitalADDs()
        {
            return await _context.HospitalADDs.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalADDs>> IncreaseCounterOfseen(int id)
        {
            var hospitalADDs = await _context.HospitalADDs.FindAsync(id);
            hospitalADDs.NumberOfSeen += 1;
            _context.Entry(hospitalADDs).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hospitalADDs;

        }

        // GET: api/HospitalADDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalADDs>> GetHospitalADDs(int id)
        {
            var hospitalADDs = await _context.HospitalADDs.FindAsync(id);

            if (hospitalADDs == null)
            {
                return NotFound();
            }

            return hospitalADDs;
        }

        // PUT: api/HospitalADDs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalADDs(int id, [FromForm] HospitalADDs hospitalADDs, IFormFile Picture)
        {
            if (id != hospitalADDs.id)
            {
                return BadRequest();
            }
            string path = _environment.WebRootPath + @"\images\";
            FileStream fileStream;
            if (Picture != null)
            {
                fileStream = System.IO.File.Create(path + "hospitalADDs_logo_" + hospitalADDs.id + "." + Picture.ContentType.Split('/')[1]);
                Picture.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
                hospitalADDs.Picture = @"\images\" + "hospitalADDs_logo_" + hospitalADDs.id + "." + Picture.ContentType.Split('/')[1];
            }

                _context.Entry(hospitalADDs).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalADDsExists(id))
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

        // POST: api/HospitalADDs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalADDs>> PostHospitalADDs([FromForm] HospitalADDs hospitalADDs, IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _context.HospitalADDs.Add(hospitalADDs);
                    await _context.SaveChangesAsync();

                    string path = _environment.WebRootPath + @"\images\";
                    FileStream fileStream;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "hospitalADDs_logo_" + hospitalADDs.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        hospitalADDs.Picture = @"\images\" + "hospitalADDs_logo_" + hospitalADDs.id + "." + Picture.ContentType.Split('/')[1];
                    }
                    if (Picture != null)
                    {
                        _context.Entry(hospitalADDs).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {


                    throw;
                }



            }

            return CreatedAtAction("GetHospitalADDs", new { id = hospitalADDs.id }, hospitalADDs);
        }

        // DELETE: api/HospitalADDs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalADDs>> DeleteHospitalADDs(int id)
        {
            var hospitalADDs = await _context.HospitalADDs.FindAsync(id);
            if (hospitalADDs == null)
            {
                return NotFound();
            }

            _context.HospitalADDs.Remove(hospitalADDs);
            await _context.SaveChangesAsync();

            return hospitalADDs;
        }

        private bool HospitalADDsExists(int id)
        {
            return _context.HospitalADDs.Any(e => e.id == id);
        }
    }
}
