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
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public FavoritesController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorite()
        {
            return await _context.Favorite.ToListAsync();
        }

       


        [HttpGet("{patientId}/{type}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavoriteByPatientIdAndType(int patientId, string type)
        {

            return await _context.Favorite.Where(x => x.PatientId == patientId & x.type == type).ToListAsync();
        }

        [HttpGet("{patientId}/{userId}/{type}")]
        public async Task<ActionResult<Favorite>> GetFavoriteByPatientIdUserIdType(int patientId,int userId,string type)
        {

            return await _context.Favorite.Where(x => x.PatientId == patientId && x.UserId==userId && x.type == type).SingleOrDefaultAsync();
        }

        //GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorsWithFavorite(int patientId)
        {
            //var favorite = (from PatientFavorite in _context.Favorite join doc in _context.Doctor on PatientFavorite.UserId equals doc.Userid where PatientFavorite.PatientId == patientId select PatientFavorite).ToList();



            return await (from PatientFavorite in _context.Favorite
                          join doctor in _context.Doctor on PatientFavorite.UserId equals doctor.Userid
                          where PatientFavorite.PatientId == patientId && PatientFavorite.type=="doctor"
                          select new
                          {
                              id = doctor.id,
                              Name = doctor.name,
                              Picture = doctor.Picture,
                              specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
                                                join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
                                                where specialitydoctor.Doctorid == doctor.id && specialit.isBasic == true && specialitydoctor.Roleid == 0
                                                select specialit).ToList(),
                              isFavorite = true,


                          }
                          ).ToListAsync();
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetWorkersWithFavorite(int patientId)
        {


            return await (from PatientFavorite in _context.Favorite
                          join worker in _context.HealthcareWorker on PatientFavorite.UserId equals worker.userId
                          where PatientFavorite.PatientId == patientId && PatientFavorite.type == "worker"
                          select new
                          {
                              id = worker.id,
                              Name = worker.Name,
                              Picture = worker.Picture,
                              Description=worker.Description,
                              isFavorite = true,
                          }
                          ).ToListAsync();
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, Favorite favorite)
        {
            if (id != favorite.id)
            {
                return BadRequest();
            }

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite favorite)
        {
            favorite.UserId = _context.Doctor.FirstOrDefault(x => x.id == favorite.UserId).Userid;
            _context.Favorite.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.id }, favorite);
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{patientId}/{userId}/{type}")]
        //[Route("api/Favorites/{patientId}/{userId}/{type}")]
        public async Task<ActionResult<Favorite>> DeleteFavorite(int patientId,int userId,string type)
        {
            userId = _context.Doctor.FirstOrDefault(x => x.id == userId).Userid;
            var favorite = await _context.Favorite.Where(x=> x.PatientId==patientId && x.UserId==userId && x.type==type).SingleOrDefaultAsync();
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        private bool FavoriteExists(int id)
        {
            return _context.Favorite.Any(e => e.id == id);
        }
    }
}
