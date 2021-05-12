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
    public class AppWorktimesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppWorktimesController(Health_CareContext context)
        {
            _context = context;
        }
        //static AvailableWorkTimeVM availableWork { get; set; }

        // GET: api/AppWorktimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppWorktime>>> GetAppWorktime()
        {
            return await _context.AppWorktime.ToListAsync();
        }

        // GET: api/AppWorktimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppWorktime>> GetAppWorktime(int id)
        {
            var appWorktime = await _context.AppWorktime.FindAsync(id);

            if (appWorktime == null)
            {
                return NotFound();
            }

            return appWorktime;
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<AppWorktime>> GetAppWorktimeBasedOnDoctorID(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            var userid = doctor.Userid;
            var appWorktimes = await _context.AppWorktime.Where(x => x.userId == userid).ToListAsync();
            return appWorktimes;
        }

        // PUT: api/AppWorktimes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppWorktime(int id, AppWorktime appWorktime)
        {
            if (id != appWorktime.id)
            {
                return BadRequest();
            }

            _context.Entry(appWorktime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppWorktimeExists(id))
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

        // POST: api/AppWorktimes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppWorktime>> PostAppWorktime(AppWorktime appWorktime)
        {
            _context.AppWorktime.Add(appWorktime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppWorktime", new { id = appWorktime.id }, appWorktime);
        }

        // DELETE: api/AppWorktimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppWorktime>> DeleteAppWorktime(int id)
        {
            var appWorktime = await _context.AppWorktime.FindAsync(id);
            if (appWorktime == null)
            {
                return NotFound();
            }

            _context.AppWorktime.Remove(appWorktime);
            await _context.SaveChangesAsync();

            return appWorktime;
        }

        //[HttpPost]
        //public ActionResult PostServiceSettingAsGroup(AvailableWorkTimeVM availableWorkTimeVM)
        //{
        //    ServiceSetting serviceSetting = new ServiceSetting();
        //    serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //    serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //    serviceSetting.ServiceID = groupServiceSetting.ServiceID;
        //    serviceSetting.Period = groupServiceSetting.Period;

        //    try
        //    {
        //        if (groupServiceSetting.sat == true)
        //        {
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Saturday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();

        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Sunday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Saturday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }

        //        }
        //        if (groupServiceSetting.sun == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Sunday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Monday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Sunday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }

        //        }
        //        if (groupServiceSetting.mon == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Monday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Tuesday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Monday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //        }
        //        if (groupServiceSetting.tue == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Tuesday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Wednesday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Tuesday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }

        //        }
        //        if (groupServiceSetting.wed == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Wednesday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Thursday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Wednesday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }

        //        }
        //        if (groupServiceSetting.thur == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Thursday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Friday.ToString();
        //                availableWork = serviceSetting;

        //                addServiceSetting();
        //            }
        //            else
        //            {

        //                serviceSetting.Day = DayOfWeek.Thursday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //        }
        //        if (groupServiceSetting.fri == true)
        //        {
        //            serviceSetting.OpenTime = Convert.ToInt32(groupServiceSetting.OpenTime);
        //            serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //            if (Convert.ToInt32(groupServiceSetting.ClossTime) < Convert.ToInt32(groupServiceSetting.OpenTime))
        //            {
        //                serviceSetting.Day = DayOfWeek.Friday.ToString();
        //                serviceSetting.ClossTime = 1439;//11:59
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //                serviceSetting.OpenTime = 0;
        //                serviceSetting.ClossTime = Convert.ToInt32(groupServiceSetting.ClossTime);
        //                serviceSetting.Day = DayOfWeek.Saturday.ToString();
        //                availableWork = serviceSetting;
        //                addServiceSetting();
        //            }
        //            else
        //            {
        //                serviceSetting.Day = DayOfWeek.Friday.ToString();
        //                availableWork = serviceSetting;

        //                addServiceSetting();
        //            }
        //        }
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(e.Message.ToString());
        //    }
        //}
        //public void addServiceSetting()
        //{

        //    _context.ServiceSetting.Add(availableWork);
        //    _context.SaveChanges();
        //    availableWork.id = 0;
        //}

        private bool AppWorktimeExists(int id)
        {
            return _context.AppWorktime.Any(e => e.id == id);
        }
    }
}
