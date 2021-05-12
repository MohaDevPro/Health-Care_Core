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
    public class DepartmentsOfHospitalsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public DepartmentsOfHospitalsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentsOfHospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentsOfHospital>>> GetdepartmentsOfHospital()
        {
            return await _context.departmentsOfHospitals.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetdepartmentsOfHospitalsByHospitalID(int id)
        {
            var hospital = await _context.Hospitals.Where(x => x.hospitalId == id).FirstOrDefaultAsync();
            return await (from hospitaldepartment in _context.hospitalDepartments join Departments in _context.departmentsOfHospitals on hospitaldepartment.DepatmentsOfHospitalID equals Departments.id
                          where hospitaldepartment.Hospitalid == hospital.id
                          select new
                          {
                              id = Departments.id,
                              Name = Departments.Name,
                              Picture = hospitaldepartment.Picture,
                              Backgroundimage = hospitaldepartment.Background

                          }

                          ).ToListAsync();
        }
        // GET: api/DepartmentsOfHospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentsOfHospital>> GetDepartmentsOfHospital(int id)
        {
            var departmentsOfHospital = await _context.departmentsOfHospitals.FindAsync(id);

            if (departmentsOfHospital == null)
            {
                return NotFound();
            }

            return departmentsOfHospital;
        }

        // PUT: api/DepartmentsOfHospitals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentsOfHospital(int id, DepartmentsOfHospital departmentsOfHospital)
        {
            if (id != departmentsOfHospital.id)
            {
                return BadRequest();
            }

            _context.Entry(departmentsOfHospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsOfHospitalExists(id))
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

        // POST: api/DepartmentsOfHospitals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DepartmentsOfHospital>> PostDepartmentsOfHospital(DepartmentsOfHospital departmentsOfHospital)
        {
            _context.departmentsOfHospitals.Add(departmentsOfHospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentsOfHospital", new { id = departmentsOfHospital.id }, departmentsOfHospital);
        }

        // DELETE: api/DepartmentsOfHospitals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentsOfHospital>> DeleteDepartmentsOfHospital(int id)
        {
            var departmentsOfHospital = await _context.departmentsOfHospitals.FindAsync(id);
            if (departmentsOfHospital == null)
            {
                return NotFound();
            }

            _context.departmentsOfHospitals.Remove(departmentsOfHospital);
            await _context.SaveChangesAsync();

            return departmentsOfHospital;
        }

        private bool DepartmentsOfHospitalExists(int id)
        {
            return _context.departmentsOfHospitals.Any(e => e.id == id);
        }
    }
}
