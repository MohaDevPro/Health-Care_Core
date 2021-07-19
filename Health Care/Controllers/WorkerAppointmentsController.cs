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
    public class WorkerAppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        //// To Get 2 weeks Appointment based on Work Days of Worker
        //[HttpGet("{month}/{day}/{year}")]
        //public List<String> GetDatesBetween(string month, string day, string year)
        //{
        //    string startDate = month + "/" + day + "/" + year;
        //    DateTime sd = DateTime.ParseExact(startDate, "M/d/yyyy", null);
        //    List<DateTime> allDates = new List<DateTime>();
        //    List<String> allDatesString = new List<String>();

        //    for (DateTime date = sd; date <= sd.AddDays(20); date = date.AddDays(1))
        //    {
        //        allDates.Add(date.Date);
        //        var x = date.Date.Month + "/" + date.Date.Day + "/" + date.Date.Year;
        //        allDatesString.Add(x);
        //    }

        //    return allDatesString;
        //}

        public WorkerAppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/WorkerAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerAppointment>>> GetWorkerAppointment()
        {
            return await _context.WorkerAppointment.ToListAsync();
        }

        // GET: api/WorkerAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerAppointment>> GetWorkerAppointment(int id)
        {
            var workerAppointment = await _context.WorkerAppointment.FindAsync(id);

            if (workerAppointment == null)
            {
                return NotFound();
            }

            return workerAppointment;
        }

        [HttpGet]
        public string timee()
        {
            List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            string TodayDate = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

            return TodayDate;
        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<IEnumerable<WorkerAppointmentViewModel>>> GetWorkerUncofirmedAppointmentBasedOnWorkerId(int workerId)
        {
            List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            string TodayDate = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            var workerAppointmentList = await _context.WorkerAppointment.Where(x => x.workerId == workerId && x.AcceptedByHealthWorker == false)
                .OrderBy(x=>x.appointmentDate).ToListAsync();

            foreach (var item in workerAppointmentList)
            {
                var workerAppointmentRequest = await _context.HealthWorkerRequestByUser.FirstOrDefaultAsync(x => x.RequestDate == TodayDate && x.appointmentId == item.id);
                result.Add(new WorkerAppointmentViewModel()
                {
                    workerAppointment = item,
                    healthWorkerRequestByUser = workerAppointmentRequest,
                });
            }

            if (result == null) { return NotFound(); }
            
            return result;
        }
        [HttpGet("{Month}")]
        public async Task<ActionResult<object>> GetServiceMonthRecords(int Month)
        {
            var appointmentOfMonth = _context.WorkerAppointment.Where(x => x.appointmentDate.Contains("/" + Month + "/"));
            var NoRepittedPatientAppointment = new List<WorkerAppointment>();
            foreach (var i in appointmentOfMonth)
            {
                if (NoRepittedPatientAppointment.FirstOrDefault(x => x.patientId == i.patientId && x.workerId == i.workerId) == null)
                {
                    NoRepittedPatientAppointment.Add(i);
                }

            }
            var healthWorkers = _context.HealthcareWorker.Where(x => x.active);
            if (appointmentOfMonth.Count() > 0 && healthWorkers.Count() > 0)
            {
                var MonthRecords = new List<object>();
                foreach (var healthworker in healthWorkers)
                {
                    var subAppointment = appointmentOfMonth.Where(x => x.workerId == healthworker.id && x.AcceptedByHealthWorker && x.cancelledByHealthWorker == false);
                    var subNoRepittedPatientAppointment = NoRepittedPatientAppointment.Where(x => x.workerId == healthworker.id && x.AcceptedByHealthWorker && x.cancelledByHealthWorker == false);
                    MonthRecords.Add(new
                    {
                        healthWorkerid = healthworker.id,
                        NumberOfServices = subAppointment.Count(),
                        HealthWorkerName = healthworker.Name,
                        NumberOfUsers = subNoRepittedPatientAppointment.Where(x => x.workerId == healthworker.id).Count(),
                        TotalyPrice = subAppointment.Select(x => x.servicePrice).Sum(),
                        BinefetOfApp = subAppointment.Select(x => x.servicePrice * x.PercentageFromAppointmentPriceForApp / 100).Sum(),
                        BinefetOfHealthWorker = subAppointment.Select(x => x.servicePrice * (100 - x.PercentageFromAppointmentPriceForApp) / 100).Sum(),
                    });
                }

                return MonthRecords;
            }
            return new List<object>();

        }

        // PUT: api/WorkerAppointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerAppointment(int id, WorkerAppointment workerAppointment)
        {
            if (id != workerAppointment.id)
            {
                return BadRequest();
            }

            _context.Entry(workerAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerAppointmentExists(id))
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
        public async Task<IActionResult> ConfirmByPatient(int id)
        {
            WorkerAppointment workerAppointment = _context.WorkerAppointment.Where(x => x.id == id).FirstOrDefault();
            if (null == workerAppointment)
            {
                return NotFound();
            }
            workerAppointment.ConfirmHealthWorkerCome_ByPatient = true;
            _context.Entry(workerAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerAppointmentExists(id))
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
        public async Task<IActionResult> ConfirmByWorker(int id)
        {
            WorkerAppointment workerAppointment = _context.WorkerAppointment.Where(x => x.id == id).FirstOrDefault();
            if (null == workerAppointment)
            {
                return NotFound();
            }
            workerAppointment.ConfirmHealthWorkerCome_ByHimself = true;
            _context.Entry(workerAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerAppointmentExists(id))
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

        // POST: api/WorkerAppointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkerAppointment>> PostWorkerAppointment(WorkerAppointment workerAppointment)
        {
            Random random = new Random();
            DateTime d = DateTime.Now;
            workerAppointment.CodeConfirmation = $"{d.ToString("dd")}{d.ToString("MM")}{d.ToString("yyyy")}-{random.Next(1000000, 9999999)}";
            _context.WorkerAppointment.Add(workerAppointment);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerAppointment", new { id = workerAppointment.id }, workerAppointment);
        }

        // DELETE: api/WorkerAppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkerAppointment>> DeleteWorkerAppointment(int id)
        {
            var workerAppointment = await _context.WorkerAppointment.FindAsync(id);
            if (workerAppointment == null)
            {
                return NotFound();
            }

            _context.WorkerAppointment.Remove(workerAppointment);
            await _context.SaveChangesAsync();

            return workerAppointment;
        }

        private bool WorkerAppointmentExists(int id)
        {
            return _context.WorkerAppointment.Any(e => e.id == id);
        }
    }
}
