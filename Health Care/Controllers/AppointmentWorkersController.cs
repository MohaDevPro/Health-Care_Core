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
    public class AppointmentWorkersController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppointmentWorkersController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentWorker>>> GetAppointmentWorker()
        {
            return await _context.AppointmentWorker.ToListAsync();
        }

        // GET: api/AppointmentWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentWorker>> GetAppointmentWorker(int id)
        {
            var appointmentWorker = await _context.AppointmentWorker.FindAsync(id);

            if (appointmentWorker == null)
            {
                return NotFound();
            }

            return appointmentWorker;
        }

        // PUT: api/AppointmentWorkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentWorker(int id, AppointmentWorker appointmentWorker)
        {
            if (id != appointmentWorker.id)
            {
                return BadRequest();
            }

            _context.Entry(appointmentWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentWorkerExists(id))
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

        // POST: api/AppointmentWorkers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppointmentWorker>> PostAppointmentWorker(AppointmentWorker appointmentWorker)
        {
            _context.AppointmentWorker.Add(appointmentWorker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentWorker", new { id = appointmentWorker.id }, appointmentWorker);
        }

        // DELETE: api/AppointmentWorkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppointmentWorker>> DeleteAppointmentWorker(int id)
        {
            var appointmentWorker = await _context.AppointmentWorker.FindAsync(id);
            if (appointmentWorker == null)
            {
                return NotFound();
            }

            _context.AppointmentWorker.Remove(appointmentWorker);
            await _context.SaveChangesAsync();

            return appointmentWorker;
        }

        private bool AppointmentWorkerExists(int id)
        {
            return _context.AppointmentWorker.Any(e => e.id == id);
        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<object>> GetProfitforWorker(int workerId)
        {
            var appointmentWorkerObjs = await _context.AppointmentWorker.Where(e=>e.workerId==workerId).ToListAsync();
            
            int servicePercentage = await _context.ProfitRatios.Select(e => e.servicePercentage).FirstOrDefaultAsync();

            var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == workerId);
            DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

            List<AppointmentWorker> newAppointmentWorkerObj = new List<AppointmentWorker>();
            foreach (var item in appointmentWorkerObjs)
            {
                DateTime appointmentWorkerObjDate = DateTime.ParseExact(item.appointmentDate, "d/M/yyyy", null);
                if (appointmentWorkerObjDate.CompareTo(contractstartdate) >= 0)
                    newAppointmentWorkerObj.Add(item);
            }

            int profitSumforAppAdmin = 0;
            foreach (var item in newAppointmentWorkerObj)
            {
                profitSumforAppAdmin += item.totalProfitFromRealAppointment;
            };

            int totalAmount = profitSumforAppAdmin * 100 / servicePercentage;
            int profitSumforuser = totalAmount - profitSumforAppAdmin;


            int appointmentSum = 0;
            foreach (var item in newAppointmentWorkerObj)
            {
                appointmentSum += item.numberOfRealAppointment;
            };

            var appointmentWorker2 = new
            {
                contractDate = userContractObj.contractStartDate,
                profitforAppUntilNow = profitSumforAppAdmin,
                profitforUserUntilNow = profitSumforuser,
                TotalAmountUntilNow = totalAmount,
                numberOfRealAppointmentUntilNow = appointmentSum,
                //contract = userContractObj,
                //appointmentdoctorclinic = appointmentDoctorClinicObj
            };

            if (appointmentWorker2 == null) { return NotFound(); }
            return appointmentWorker2;

        }
    }
}
