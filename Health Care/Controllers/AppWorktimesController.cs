using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;

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
        static AppWorktime availableWork { get; set; }

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
            //var doctor = await _context.Doctor.FindAsync(id);
            //var userid = doctor.Userid;
            var appWorktimes = await _context.AppWorktime.Where(x => x.userId == id).ToListAsync();
            return appWorktimes;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetAppWorktimeAsGroup(int id)
        {
            var AvailableIfEdit = new AvailableWorkTimeVM
            {
                sat = false,
                sun = false,
                mon = false,
                tue = false,
                wed = false,
                thur = false,
                fri = false,
                userId = id,
            };
            var GetAll = await (from Available in _context.AppWorktime where Available.userId == id select Available).ToListAsync();
            foreach (var items in GetAll)
            {
                if (items.day == 1)
                    AvailableIfEdit.mon = true;
                else if (items.day == 2)
                    AvailableIfEdit.tue = true;
                else if (items.day == 3)
                    AvailableIfEdit.wed = true;
                else if (items.day == 4)
                    AvailableIfEdit.thur = true;
                else if (items.day == 5)
                    AvailableIfEdit.fri = true;
                else if (items.day == 6)
                    AvailableIfEdit.sat = true;
                else AvailableIfEdit.sun = true;
            }

            var Result = new
            {
                sat = AvailableIfEdit.sat,
                sun = AvailableIfEdit.sun,
                mon = AvailableIfEdit.mon,
                tue = AvailableIfEdit.tue,
                wed = AvailableIfEdit.wed,
                thur = AvailableIfEdit.thur,
                fri = AvailableIfEdit.fri,
                userId = id,
            };
            return Result;
        }
        [HttpGet("{id}/{AM_PM}")]
        public async Task<ActionResult<Object>> GetAppWorktimeByIdAndPeriod(int id,string AM_PM)
        {
            bool isSelected=false;
            var Open = "";
            var Close = "";
            var GetAll = await (from Available in _context.AppWorktime where Available.userId == id && Available.shiftAM_PM==AM_PM select Available).FirstOrDefaultAsync();
            if (GetAll != null)
            {
                isSelected = true;
                Open = GetAll.RealOpenTime;
                Close = GetAll.RealClossTime;
            }
            var AvailableTime = new
            {
                morningEvening = isSelected,
                realOpenTime = Open,
                realClossTime = Close,
            };
            return AvailableTime;
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

        [HttpPost]
        public ActionResult PostAvailableWorkTimeAsGroup(AvailableWorkTimeVM availableWorkTimeVM)
        {
            var AvailableIfEdit = new AvailableWorkTimeVM {
                sat = false,
                sun = false,
                mon = false,
                tue = false,
                wed = false,
                thur = false,
                fri = false,
                shiftAMPM = availableWorkTimeVM.shiftAMPM,
                userId=availableWorkTimeVM.userId,
            };
            var checkIfEdit =  (from Available in _context.AppWorktime where Available.userId==availableWorkTimeVM.userId && Available.shiftAM_PM==availableWorkTimeVM.shiftAMPM select Available).ToList();
            foreach(var items in checkIfEdit)
            {
                if (items.day == 1)
                    AvailableIfEdit.mon = true;
                else if (items.day == 2)
                    AvailableIfEdit.tue = true;
                else if (items.day == 3)
                    AvailableIfEdit.wed = true;
                else if (items.day == 4)
                    AvailableIfEdit.thur = true;
                else if (items.day == 5)
                    AvailableIfEdit.fri = true;
                else if (items.day == 6)
                    AvailableIfEdit.sat = true;
                else if(items.day==7)
                    AvailableIfEdit.sun = true;

                AvailableIfEdit.RealOpenTime = items.RealOpenTime;
                AvailableIfEdit.RealClossTime = items.RealClossTime;
                AvailableIfEdit.startTime = items.startTime;
                AvailableIfEdit.endTime = items.endTime;
                
            }
            if (checkIfEdit != null)
            {
                if (AvailableIfEdit.startTime != availableWorkTimeVM.startTime || AvailableIfEdit.endTime != availableWorkTimeVM.endTime)
                {
                    DeleteAppWorktimeAsGroup(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM);
                }
                else
                {
                    if (AvailableIfEdit.sat == true && availableWorkTimeVM.sat == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 6);

                    }
                    if (AvailableIfEdit.sun == true && availableWorkTimeVM.sun == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 7);

                    }
                    if (AvailableIfEdit.mon == true && availableWorkTimeVM.mon == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 1);

                    }
                    if (AvailableIfEdit.tue == true && availableWorkTimeVM.tue == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 2);

                    }
                    if (AvailableIfEdit.wed == true && availableWorkTimeVM.wed == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 3);

                    }
                    if (AvailableIfEdit.thur == true && availableWorkTimeVM.thur == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 4);

                    }
                    if (AvailableIfEdit.fri == true && availableWorkTimeVM.fri == false)
                    {
                        DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 5);
                    }
                }
            }

            AppWorktime appworktime = new AppWorktime();
            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
            appworktime.userId = availableWorkTimeVM.userId;
            appworktime.shiftAM_PM = availableWorkTimeVM.shiftAMPM;
            appworktime.RealOpenTime = availableWorkTimeVM.RealOpenTime;
            appworktime.RealClossTime = availableWorkTimeVM.RealClossTime;

            try
            {
                if (AvailableIfEdit.sat==false && availableWorkTimeVM.sat == true )
                {
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 6;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();

                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 7;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 6;
                        availableWork = appworktime;
                        addAppWorktime();
                    }

                }
                if (AvailableIfEdit.sun == false && availableWorkTimeVM.sun == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 7;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 1;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 7;
                        availableWork = appworktime;
                        addAppWorktime();
                    }

                }
                if (AvailableIfEdit.mon == false && availableWorkTimeVM.mon == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 1;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 2;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 1;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                }
                if (AvailableIfEdit.tue == false && availableWorkTimeVM.tue == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 2;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 3;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 2;
                        availableWork = appworktime;
                        addAppWorktime();
                    }

                }
                if (AvailableIfEdit.wed == false && availableWorkTimeVM.wed == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 3;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 4;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 3;
                        availableWork = appworktime;
                        addAppWorktime();
                    }

                }
                if (AvailableIfEdit.thur == false && availableWorkTimeVM.thur == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 4;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 5;
                        availableWork = appworktime;

                        addAppWorktime();
                    }
                    else
                    {

                        appworktime.day = 4;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                }
                if (AvailableIfEdit.fri == false && availableWorkTimeVM.fri == true)
                {
                    appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    {
                        appworktime.day = 5;
                        appworktime.endTime = 1439;//11:59
                        availableWork = appworktime;
                        addAppWorktime();
                        appworktime.startTime = 0;
                        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                        appworktime.day = 6;
                        availableWork = appworktime;
                        addAppWorktime();
                    }
                    else
                    {
                        appworktime.day = 5;
                        availableWork = appworktime;

                        addAppWorktime();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return Content(e.Message.ToString());
            }
        }
        public void addAppWorktime()
        {

            _context.AppWorktime.Add(availableWork);
            _context.SaveChanges();
            availableWork.id = 0;
        }
        public  void DeleteAppWorktimeByDayAndPeriod(int userId,string AM_PM,int day)
        {
            var getappWorktime =  _context.AppWorktime.Where(x => x.userId == userId && x.shiftAM_PM == AM_PM && x.day == day).SingleOrDefault();
            var appWorktime =  _context.AppWorktime.Find(getappWorktime.id);
            _context.AppWorktime.Remove(appWorktime);
           
             _context.SaveChanges();
        }
        public void DeleteAppWorktimeAsGroup(int userId, string AM_PM)
        {
            var appWorktimes = _context.AppWorktime.Where(x => x.userId == userId && x.shiftAM_PM == AM_PM);
           
           
            _context.AppWorktime.RemoveRange(appWorktimes);
            _context.SaveChangesAsync();
        }
        private bool AppWorktimeExists(int id)
        {
            return _context.AppWorktime.Any(e => e.id == id);
        }
    }
}
