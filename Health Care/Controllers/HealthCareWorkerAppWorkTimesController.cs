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
    public class HealthCareWorkerAppWorkTimesController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HealthCareWorkerAppWorkTimesController(Health_CareContext context)
        {
            _context = context;
        }
        static HealthCareWorkerAppWorkTime availableWork { get; set; }

        // GET: api/HealthCareWorkerAppWorkTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthCareWorkerAppWorkTime>>> GetHealthCareWorkerAppWorkTime()
        {
            return await _context.HealthCareWorkerAppWorkTime.ToListAsync();
        }

        // GET: api/HealthCareWorkerAppWorkTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> GetHealthCareWorkerAppWorkTime(int id)
        {
            var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.FindAsync(id);
            if (healthCareWorkerAppWorkTime == null)
            {
                return NotFound();
            }
            return healthCareWorkerAppWorkTime;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HealthCareWorkerAppWorkTime>>> GetHealthCareWorkerAppWorkTimeBasedonUserId(int id)
        {
            var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.Where(x=>x.userId==id).ToListAsync();
            
            return healthCareWorkerAppWorkTime;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<HealthCareWorkerAppWorkTime>>> GetWorkerWorkTimeByuserId(int userId)
        {
            return await _context.HealthCareWorkerAppWorkTime.Where(x => x.IsAdditional == false && x.userId == userId).OrderBy(x => x.day).ToListAsync();
        }
        [HttpGet("{id}/{day}")]
        public async Task<ActionResult<String>> GetWorkerAvailableTimeBasedonUserIDDayAndNotAdditional(int id, int day)
        {
            var appworktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == day && x.IsAdditional == false ).ToListAsync();
            if (appworktime.Count >= 2)
                return "exsist";
            else if (appworktime.Count == 1)
                return appworktime[0].shiftAM_PM;
            else return "ok";
        }
        [HttpGet]
        public async Task<ActionResult<String>> GetWorkerWorkTimeGroupByServiceIDDayAndNotAdditional(int id,string period, bool sat, bool sun, bool mon, bool tue, bool wed, bool thur, bool fri)
        {
            string finalResult = "";
            bool isExsist = false;
            if (sat)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 6 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += " -دوام يوم السبت صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم السبت مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (sun)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 7 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الاحد صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الاحد مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (mon)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 1 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الاثنين صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الاثنين مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (tue)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 2 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الثلاثاء صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الثلاثاء مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (wed)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 3 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الاربعاء صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الاربعاء مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (thur)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 4 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count == 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الخميس صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الخميس مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (fri)
            {
                var worktime = await _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == id & x.day == 5 && x.IsAdditional == false && x.shiftAM_PM == period ).ToListAsync();
                if (worktime.Count >= 1)
                {
                    if (worktime[0].shiftAM_PM == "A")
                        finalResult += "-دوام يوم الجمعة صباحاً موجود مسبقاً";
                    else finalResult += "-دوام يوم الجمعة مساءً موجود مسبقاً";
                    isExsist = true;
                }
            }
            if (isExsist)
                return finalResult;
            else return "ok";

        }


        // PUT: api/HealthCareWorkerAppWorkTimes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHealthCareWorkerAppWorkTime(int id, HealthCareWorkerAppWorkTime healthCareWorkerAppWorkTime)
        //{
        //    if (id != healthCareWorkerAppWorkTime.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(healthCareWorkerAppWorkTime).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HealthCareWorkerAppWorkTimeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthCareWorkerAppWorkTime(int id, HealthCareWorkerAppWorkTime appWorktime)
        {
            if (id != appWorktime.id)
            {
                return BadRequest();
            }
            int additionalDay = 1;
            if (appWorktime.day != 7)
                additionalDay = appWorktime.day + 1;
            var AdditionalserviceSetting = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == appWorktime.userId && x.shiftAM_PM == appWorktime.shiftAM_PM && x.IsAdditional == true && x.day == additionalDay).FirstOrDefault();


            if (Convert.ToInt32(appWorktime.endTime) < Convert.ToInt32(appWorktime.startTime))
            {
                appWorktime.endTime = 1439;//11:59
                appWorktime.IsAdditional = false;
                availableWork = appWorktime;
                _context.Entry(appWorktime).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    if (AdditionalserviceSetting != null)
                    {
                        _context.HealthCareWorkerAppWorkTime.Remove(AdditionalserviceSetting);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthCareWorkerAppWorkTimeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (appWorktime.day == 7)
                    appWorktime.day = 0;
                else appWorktime.day++;
                appWorktime.startTime = 0;
                appWorktime.endTime = Convert.ToInt32(appWorktime.endTime);
                appWorktime.IsAdditional = true;
                availableWork = appWorktime;

                addAppWorktime();
            }
            else
            {
                appWorktime.IsAdditional = false;
                _context.Entry(appWorktime).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    if (AdditionalserviceSetting != null)
                    {
                        _context.HealthCareWorkerAppWorkTime.Remove(AdditionalserviceSetting);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthCareWorkerAppWorkTimeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            return NoContent();
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
            var GetAll = await (from Available in _context.HealthCareWorkerAppWorkTime where Available.userId == id && Available.IsAdditional == false select Available).ToListAsync();
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
        public async Task<ActionResult<Object>> GetAppWorktimeByIdAndPeriod(int id, string AM_PM)
        {
            bool isSelected = false;
            var Open = "";
            var Close = "";
            var GetAll = await (from Available in _context.HealthCareWorkerAppWorkTime where Available.userId == id && Available.shiftAM_PM == AM_PM select Available).FirstOrDefaultAsync();
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

        // POST: api/HealthCareWorkerAppWorkTimes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<HealthCareWorkerAppWorkTime>> PostHealthCareWorkerAppWorkTime(HealthCareWorkerAppWorkTime healthCareWorkerAppWorkTime)
        //{
        //    _context.HealthCareWorkerAppWorkTime.Add(healthCareWorkerAppWorkTime);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetHealthCareWorkerAppWorkTime", new { id = healthCareWorkerAppWorkTime.id }, healthCareWorkerAppWorkTime);
        //}
        [HttpPost]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> PostHealthCareWorkerAppWorkTime(HealthCareWorkerAppWorkTime appWorktime)
        {
            if (Convert.ToInt32(appWorktime.endTime) < Convert.ToInt32(appWorktime.startTime))
            {
                appWorktime.endTime = 1439;//11:59
                appWorktime.IsAdditional = false;
                availableWork = appWorktime;
                addAppWorktime();
                if (appWorktime.day == 7)
                    appWorktime.day = 0;
                else appWorktime.day++;
                appWorktime.startTime = 0;
                appWorktime.endTime = Convert.ToInt32(appWorktime.endTime);
                appWorktime.IsAdditional = true;
                availableWork = appWorktime;

                addAppWorktime();
            }
            else
            {
                appWorktime.IsAdditional = false;
                availableWork = appWorktime;
                addAppWorktime();
            }
            return CreatedAtAction("GetServiceSetting", new { id = appWorktime.id }, appWorktime);
        }
        [HttpPost]
        public ActionResult PostAvailableWorkTimeAsGroup(AvailableWorkTimeVM availableWorkTimeVM)
        {
            bool isDeffrintTime = true;

            //if (availableWorkTimeVM.shiftAMPM == "nothing")
            //{
            //    DeleteAllAppWorktimeByUserId(availableWorkTimeVM.userId);
            //    return Ok();
            //}
           
            //    if (!availableWorkTimeVM.AM)
            //    {
            //        DeleteAppworkTimeByPeriod(availableWorkTimeVM.userId, "A");
            //    };
            //    if (!availableWorkTimeVM.PM)
            //    {
            //        DeleteAppworkTimeByPeriod(availableWorkTimeVM.userId, "P");
            //    };
            //    var AvailableIfEdit = new AvailableWorkTimeVM
            //    {
            //        sat = false,
            //        sun = false,
            //        mon = false,
            //        tue = false,
            //        wed = false,
            //        thur = false,
            //        fri = false,
            //        shiftAMPM = availableWorkTimeVM.shiftAMPM,
            //        userId = availableWorkTimeVM.userId,
            //    };
            //    var checkIfEdit = (from Available in _context.HealthCareWorkerAppWorkTime where Available.userId == availableWorkTimeVM.userId && Available.shiftAM_PM == availableWorkTimeVM.shiftAMPM select Available).ToList();
            //    foreach (var items in checkIfEdit)
            //    {
            //        if (items.day == 1)
            //            AvailableIfEdit.mon = true;
            //        else if (items.day == 2)
            //            AvailableIfEdit.tue = true;
            //        else if (items.day == 3)
            //            AvailableIfEdit.wed = true;
            //        else if (items.day == 4)
            //            AvailableIfEdit.thur = true;
            //        else if (items.day == 5)
            //            AvailableIfEdit.fri = true;
            //        else if (items.day == 6)
            //            AvailableIfEdit.sat = true;
            //        else if (items.day == 7)
            //            AvailableIfEdit.sun = true;

            //        AvailableIfEdit.RealOpenTime = items.RealOpenTime;
            //        AvailableIfEdit.RealClossTime = items.RealClossTime;
            //        AvailableIfEdit.startTime = items.startTime;
            //        AvailableIfEdit.endTime = items.endTime;

            //    }
            //    if (checkIfEdit != null)
            //    {
            //        if (AvailableIfEdit.startTime != availableWorkTimeVM.startTime || AvailableIfEdit.endTime != availableWorkTimeVM.endTime)
            //        {
            //            DeleteAppWorktimeAsGroup(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM);
            //        }
            //        else
            //        {
            //            if (AvailableIfEdit.sat == true && availableWorkTimeVM.sat == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 6);

            //            }
            //            if (AvailableIfEdit.sun == true && availableWorkTimeVM.sun == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 7);

            //            }
            //            if (AvailableIfEdit.mon == true && availableWorkTimeVM.mon == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 1);

            //            }
            //            if (AvailableIfEdit.tue == true && availableWorkTimeVM.tue == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 2);

            //            }
            //            if (AvailableIfEdit.wed == true && availableWorkTimeVM.wed == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 3);

            //            }
            //            if (AvailableIfEdit.thur == true && availableWorkTimeVM.thur == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 4);

            //            }
            //            if (AvailableIfEdit.fri == true && availableWorkTimeVM.fri == false)
            //            {
            //                DeleteAppWorktimeByDayAndPeriod(availableWorkTimeVM.userId, availableWorkTimeVM.shiftAMPM, 5);
            //            }
            //        }
            //    }

                HealthCareWorkerAppWorkTime appworktime = new HealthCareWorkerAppWorkTime();
                appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                appworktime.userId = availableWorkTimeVM.userId;
                appworktime.shiftAM_PM = availableWorkTimeVM.shiftAMPM;
                appworktime.RealOpenTime = availableWorkTimeVM.RealOpenTime;
                appworktime.RealClossTime = availableWorkTimeVM.RealClossTime;

                try
                {
                    if (isDeffrintTime) {
                        if ( availableWorkTimeVM.sat == true)
                        {
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 6;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 7;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 6;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }

                        }
                        if ( availableWorkTimeVM.sun == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 7;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 1;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 7;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }

                        }
                        if (availableWorkTimeVM.mon == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 1;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 2;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 1;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                        }
                        if ( availableWorkTimeVM.tue == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 2;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 3;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 2;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }

                        }
                        if ( availableWorkTimeVM.wed == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 3;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 4;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 3;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }

                        }
                        if ( availableWorkTimeVM.thur == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 4;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 5;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {

                                appworktime.day = 4;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                        }
                        if ( availableWorkTimeVM.fri == true)
                        {
                            appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                            if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                            {
                                appworktime.day = 5;
                                appworktime.endTime = 1439;//11:59
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();

                                appworktime.startTime = 0;
                                appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                                appworktime.day = 6;
                                appworktime.IsAdditional = true;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                            else
                            {
                                appworktime.day = 5;
                                appworktime.IsAdditional = false;
                                availableWork = appworktime;
                                addAppWorktime();
                            }
                        }
                    }
                    //else
                    //{
                    //    if (AvailableIfEdit.sat == false && availableWorkTimeVM.sat == true)
                    //    {
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 6;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 7;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 6;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }

                    //    }
                    //    if (AvailableIfEdit.sun == false && availableWorkTimeVM.sun == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 7;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 1;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 7;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }

                    //    }
                    //    if (AvailableIfEdit.mon == false && availableWorkTimeVM.mon == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 1;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 2;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 1;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //    }
                    //    if (AvailableIfEdit.tue == false && availableWorkTimeVM.tue == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 2;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 3;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 2;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }

                    //    }
                    //    if (AvailableIfEdit.wed == false && availableWorkTimeVM.wed == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 3;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 4;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 3;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }

                    //    }
                    //    if (AvailableIfEdit.thur == false && availableWorkTimeVM.thur == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 4;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 5;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {

                    //            appworktime.day = 4;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //    }
                    //    if (AvailableIfEdit.fri == false && availableWorkTimeVM.fri == true)
                    //    {
                    //        appworktime.startTime = Convert.ToInt32(availableWorkTimeVM.startTime);
                    //        appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //        if (Convert.ToInt32(availableWorkTimeVM.endTime) < Convert.ToInt32(availableWorkTimeVM.startTime))
                    //        {
                    //            appworktime.day = 5;
                    //            appworktime.endTime = 1439;//11:59
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();

                    //            appworktime.startTime = 0;
                    //            appworktime.endTime = Convert.ToInt32(availableWorkTimeVM.endTime);
                    //            appworktime.day = 6;
                    //            appworktime.IsAdditional = true;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //        else
                    //        {
                    //            appworktime.day = 5;
                    //            appworktime.IsAdditional = false;
                    //            availableWork = appworktime;
                    //            addAppWorktime();
                    //        }
                    //    }
                    //}

                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(e.Message.ToString());
                }
            
        }
        public void addAppWorktime()
        {

            _context.HealthCareWorkerAppWorkTime.Add(availableWork);
            _context.SaveChanges();
            availableWork.id = 0;
        }
        public void DeleteAppWorktimeByDayAndPeriod(int userId, string AM_PM, int day)
        {
            var getappWorktime = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == userId && x.shiftAM_PM == AM_PM && x.day == day).SingleOrDefault();
            var appWorktime = _context.HealthCareWorkerAppWorkTime.Find(getappWorktime.id);
            _context.HealthCareWorkerAppWorkTime.Remove(appWorktime);

            _context.SaveChanges();
        }
        public void DeleteAppWorktimeAsGroup(int userId, string AM_PM)
        {
            var appWorktimes = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == userId && x.shiftAM_PM == AM_PM);


            _context.HealthCareWorkerAppWorkTime.RemoveRange(appWorktimes);
            _context.SaveChangesAsync();
        }
        public void DeleteAllAppWorktimeByUserId(int userId)
        {
            var appWorktimes = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == userId).ToList();
            foreach (var i in appWorktimes)
            {
                _context.HealthCareWorkerAppWorkTime.Remove(i);
                _context.SaveChanges();
            }


        }
        public void DeleteAppworkTimeByPeriod(int userId, string AM_PM)
        {
            var getappWorktime = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == userId && x.shiftAM_PM == AM_PM);
            _context.HealthCareWorkerAppWorkTime.RemoveRange(getappWorktime);

            _context.SaveChanges();
        }

        // DELETE: api/HealthCareWorkerAppWorkTimes/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<HealthCareWorkerAppWorkTime>> DeleteHealthCareWorkerAppWorkTime(int id)
        //{
        //    var healthCareWorkerAppWorkTime = await _context.HealthCareWorkerAppWorkTime.FindAsync(id);
        //    if (healthCareWorkerAppWorkTime == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.HealthCareWorkerAppWorkTime.Remove(healthCareWorkerAppWorkTime);
        //    await _context.SaveChangesAsync();

        //    return healthCareWorkerAppWorkTime;
        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthCareWorkerAppWorkTime>> DeleteHealthCareWorkerAppWorkTime(int id)
        {
            var appWorktime = await _context.HealthCareWorkerAppWorkTime.FindAsync(id);
            if (appWorktime == null)
            {
                return NotFound();
            }
            int additionalDay = 1;
            if (appWorktime.day != 7)
                additionalDay = appWorktime.day + 1;
            var AdditionalserviceSetting = _context.HealthCareWorkerAppWorkTime.Where(x => x.userId == appWorktime.userId && x.shiftAM_PM == appWorktime.shiftAM_PM && x.IsAdditional == true && x.day == additionalDay).FirstOrDefault();

            _context.HealthCareWorkerAppWorkTime.Remove(appWorktime);
            await _context.SaveChangesAsync();

            if (AdditionalserviceSetting != null)
            {
                _context.HealthCareWorkerAppWorkTime.Remove(AdditionalserviceSetting);
                await _context.SaveChangesAsync();
            }

            return appWorktime;
        }

        private bool HealthCareWorkerAppWorkTimeExists(int id)
        {
            return _context.HealthCareWorkerAppWorkTime.Any(e => e.id == id);
        }
    }
}
