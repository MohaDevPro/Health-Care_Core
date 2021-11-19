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
        public async Task<ActionResult<IEnumerable<object>>> GetWorkerUncofirmedAppointmentBasedOnWorkerId(int workerId)
        {
            List<object> result = new List<object>();

            //string TodayDate = DateTime.Now.ToUniversalTime().AddHours(3).ToString("dd/MM/yyyy");
            var workerAppointmentList = await (from appointment in  _context.WorkerAppointment.Where(x => x.workerId == workerId && x.AcceptedByHealthWorker == false&&x.cancelledByHealthWorker==false)
                .OrderBy(x=>x.appointmentDate)
                join user in _context.User on appointment.patientId equals user.id
                join service in _context.Service on appointment.serviceId equals service.id
                select new
                {
                    appointment,
                    user.nameAR,
                    user.phoneNumber,
                    service.serviceName
                }
                
                ).ToListAsync();

            foreach (var item in workerAppointmentList)
            {
                var workerAppointmentRequest = await _context.HealthWorkerRequestByUser.FirstOrDefaultAsync(x => x.appointmentId == item.appointment.id);
                if (workerAppointmentRequest != null)
                {
                    result.Add(new 
                    {
                        workerAppointment = item.appointment,
                        healthWorkerRequestByUser = workerAppointmentRequest,
                        item.nameAR,
                        item.phoneNumber,
                        item.serviceName
                    });
                }

            }

            //if (result == null) { return NotFound(); }
            
            return result;
        }
        public string getDate(int workerId)
        {
            List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            string TodayDate = DateTime.Now.ToUniversalTime().AddHours(3).ToString("G");
           
            return TodayDate;
        }
        public string getDateWithAdd(int workerId)
        {
            List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            string TodayDate = DateTime.Now.ToUniversalTime().AddHours(7).ToString("G");

            return TodayDate;
        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<object>> GetWorkerCofirmedAppointmentBasedOnWorkerId(int workerId)
        {
            //List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            //string TodayDate = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            var ConfirmedAppointmentbyworker = await _context.WorkerAppointment.Where(x => x.workerId == workerId && x.AcceptedByHealthWorker == true && x.ConfirmHealthWorkerCome_ByPatient == false && x.cancelledByHealthWorker == false)
                .OrderBy(x => x.appointmentDate).ToListAsync();

            List<Service> servicesList = new List<Service>();
            List<Service> servicesListBasedOnAppointmentList = await _context.Service.ToListAsync();
            
            List<User> patienList = new List<User>();
            List<User> patientInfo = await _context.User.ToListAsync();

            foreach (var item in ConfirmedAppointmentbyworker)
            {
                servicesList.Add(servicesListBasedOnAppointmentList.FirstOrDefault(e => e.id == item.serviceId));
                patienList.Add(patientInfo.FirstOrDefault(e => e.id == item.patientId));
            }

            var result = new
            {
                ConfirmedAppointmentbyworker = ConfirmedAppointmentbyworker,
                servicesListBasedOnAppointmentList = servicesList,
                patientInfo = patienList,
            };
            if (result == null) { return NotFound(); }

            return result;
        }

   
        [HttpGet("{workerId}")]
        public async Task<ActionResult<object>> GetCofirmedAppointmentBasedOnWorkerId(int workerId)
        {
            //List<WorkerAppointmentViewModel> result = new List<WorkerAppointmentViewModel>();

            //string TodayDate = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            var ConfirmedAppointmentbyworker = await _context.WorkerAppointment.Where(x => x.workerId == workerId && x.AcceptedByHealthWorker == true && x.ConfirmHealthWorkerCome_ByPatient == true && x.cancelledByHealthWorker == false)
                .OrderBy(x => x.appointmentDate).ToListAsync();

            List<Service> servicesList = new List<Service>();
            List<Service> servicesListBasedOnAppointmentList = await _context.Service.ToListAsync();
            
            List<User> patienList = new List<User>();
            List<User> patientInfo = await _context.User.ToListAsync();

            foreach (var item in ConfirmedAppointmentbyworker)
            {
                servicesList.Add(servicesListBasedOnAppointmentList.FirstOrDefault(e => e.id == item.serviceId));
                patienList.Add(patientInfo.FirstOrDefault(e => e.id == item.patientId));
            }

            var result = new
            {
                ConfirmedAppointmentbyworker = ConfirmedAppointmentbyworker,
                servicesListBasedOnAppointmentList = servicesList,
                patientInfo = patienList,
            };
            if (result == null) { return NotFound(); }

            return result;
        }

        [HttpGet("{Month}/{HealthWorkerID}")]
        public async Task<ActionResult<object>> GetServiceMonthRecords(int Month,int HealthWorkerID)
        {
            var appointmentOfMonth = _context.WorkerAppointment.Where(x => x.appointmentDate.Contains("/" + Month + "/") && (x.ConfirmHealthWorkerCome_ByHimself || x.ConfirmHealthWorkerCome_ByPatient)).ToList();
            var NoRepittedPatientAppointment = new List<WorkerAppointment>();
            foreach (var i in appointmentOfMonth)
            {
                if (NoRepittedPatientAppointment.FirstOrDefault(x => x.patientId == i.patientId && x.workerId == i.workerId) == null)
                {
                    NoRepittedPatientAppointment.Add(i);
                }

            }
            var healthWorkers = _context.HealthcareWorker.Where(x => x.active).ToList();
            if (appointmentOfMonth.Count() > 0 && healthWorkers.Count() > 0)
            {
                if (HealthWorkerID != 0)
                {
                    healthWorkers = healthWorkers.Where(x => x.id == HealthWorkerID).ToList();
                }
                var MonthRecords = new List<object>();
                foreach (var healthworker in healthWorkers)
                {
                    var subAppointment = appointmentOfMonth.Where(x => x.workerId == healthworker.userId  );
                    var subNoRepittedPatientAppointment = NoRepittedPatientAppointment.Where(x => x.workerId == healthworker.userId );
                    var TotalyPrice = subAppointment.Select(x => x.servicePrice).Sum();
                    var BinefetOfApp = subAppointment.Select(x => x.servicePrice * x.PercentageFromAppointmentPriceForApp / 100).Sum();
                    var BinefetOfHealthWorker = subAppointment.Select(x => x.servicePrice * (100 - x.PercentageFromAppointmentPriceForApp) / 100).Sum();
                    MonthRecords.Add(new
                    {
                        healthWorkerid = healthworker.id,
                        NumberOfServices = subAppointment.Count(),
                        HealthWorkerName = healthworker.Name,
                        NumberOfUsers = subNoRepittedPatientAppointment.Count(),
                        TotalyPrice ,
                        BinefetOfApp ,
                        BinefetOfHealthWorker,
                    });
                }

                return MonthRecords;
            }
            return new List<object>();

        }

        [HttpGet("{userId}/{acceptedByWorker}/{confirmWorkerComeByPatient}/{cancelledByWorker}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetWorkerAppointmentBasedOnStatusByUserId(int userId,bool acceptedByWorker,bool confirmWorkerComeByPatient,bool cancelledByWorker,int pageKey,int pageSize)
        {
            //WorkerAppointmentsCategories li = new WorkerAppointmentsCategories();
            //var workerAppointment =await _context.WorkerAppointment.Where(x => x.patientId == userId).ToListAsync();
            var workerAppointment = await (from appoitnment in _context.WorkerAppointment
                                           join worker in _context.HealthcareWorker on appoitnment.workerId equals worker.userId join service in _context.Service on appoitnment.serviceId equals service.id
                                           where appoitnment.patientId == userId
                                           select new
                                           {
                                               appoitnment.id,
                                               appoitnment.patientId,
                                               appoitnment.PercentageFromAppointmentPriceForApp,
                                               appoitnment.regionId,
                                               appoitnment.reservedAmountUntilConfirm,
                                               appoitnment.serviceId,
                                               appoitnment.servicePrice,
                                               workerName= worker.Name,
                                               appoitnment.workerId,
                                               appoitnment.cancelledByHealthWorker,
                                               appoitnment.appointmentDate,
                                               appoitnment.appointmentShift,
                                               appoitnment.cancelReasonWrittenByHealthWorker,
                                               appoitnment.CodeConfirmation,
                                               appoitnment.ConfirmHealthWorkerCome_ByHimself,
                                               appoitnment.ConfirmHealthWorkerCome_ByPatient,
                                               appoitnment.doesNotCome,
                                               appoitnment.AcceptedByHealthWorker,
                                               service.serviceName,
                                           }).ToListAsync();
            //var ConfirmedAppointmentbyworker = await _context.WorkerAppointment.Where(x => x.patientId == userId && x.AcceptedByHealthWorker == true && x.ConfirmHealthWorkerCome_ByPatient==false).ToListAsync();
            //var WorkerComeToAppointment = await _context.WorkerAppointment.Where(x => x.patientId == userId && x.ConfirmHealthWorkerCome_ByPatient == true).ToListAsync();
            //var cancelledAppointmentbyworker = await _context.WorkerAppointment.Where(x => x.patientId == userId && x.cancelledByHealthWorker == true).ToListAsync();
            //var UnConfirmedAppointmentbyworker = await _context.WorkerAppointment.Where(x => x.patientId == userId && x.AcceptedByHealthWorker == false && x.ConfirmHealthWorkerCome_ByPatient == false).ToListAsync();
            if (cancelledByWorker)
                workerAppointment = workerAppointment.Where(x => x.patientId == userId && x.cancelledByHealthWorker == true).ToList();
            else if (confirmWorkerComeByPatient)
                workerAppointment = workerAppointment.Where(x => x.patientId == userId && x.ConfirmHealthWorkerCome_ByPatient == true).ToList();
            else if (acceptedByWorker)
                workerAppointment = workerAppointment.Where(x => x.patientId == userId && x.AcceptedByHealthWorker == true && x.ConfirmHealthWorkerCome_ByPatient == false).ToList();
            else
                workerAppointment = workerAppointment.Where(x => x.patientId == userId && x.AcceptedByHealthWorker == false && x.cancelledByHealthWorker == false).ToList();

            //li.ConfirmedAppointmentbyworker1 = ConfirmedAppointmentbyworker;
            //li.WorkerComeToAppointment1 = WorkerComeToAppointment;
            //li.cancelledAppointmentbyworker1 = cancelledAppointmentbyworker;
            //li.UnConfirmedAppointmentbyworker1 = UnConfirmedAppointmentbyworker;

            //if (li == null) { return NotFound(); }
            if (pageSize != 0)
                return workerAppointment.OrderByDescending(x=>x.id).Skip(pageKey).Take(pageSize).ToList();
            else return workerAppointment.OrderByDescending(x=>x.id).ToList();
            //return li;
        }

        //[HttpGet("{userId}")]
        //public async Task<ActionResult<WorkerAppointmentsCategories>> GetWorkerAppointmentsBasedOnStatusByUserId(int userId)
        //{
        //    WorkerAppointmentsCategories obj = new WorkerAppointmentsCategories();

        //    var ConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted == true && x.cancelledByUser == false).ToListAsync();
        //    var unConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted == false && x.cancelledByUser == false).ToListAsync();
        //    var cancelledAppointment = await _context.Appointment.Where(x => x.userId == userId && x.cancelledByUser == true).ToListAsync();

        //    obj.

        //    li.Add(unConfirmedAppointment);
        //    li.Add(cancelledAppointment);

        //    if (li == null) { return NotFound(); }
        //    return li;
        //}

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
            if (workerAppointment.cancelledByHealthWorker)
            {
                Patient patient = _context.Patient.Where(p=>p.userId == workerAppointment.patientId).FirstOrDefault();
                if (patient != null)
                {
                    patient.Balance += workerAppointment.servicePrice;
                }
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
            
            //if (workerAppointment.ConfirmHealthWorkerCome_ByPatient)
            //{
            //    AppointmentWorker appointmentWorker = await _context.AppointmentWorker.FirstOrDefaultAsync(e => e.workerId == workerAppointment.workerId);
            //    appointmentWorker.totalProfitFromRealAppointment += (workerAppointment.servicePrice * (workerAppointment.PercentageFromAppointmentPriceForApp / 100));
            //    _context.Entry(appointmentWorker).State = EntityState.Modified;
            //}

            workerAppointment.reservedAmountUntilConfirm = true;
            //Patient patient = _context.Patient.Where(x=>x.userId == workerAppointment.patientId).FirstOrDefault();
            //patient.Balance -= workerAppointment.servicePrice;
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

            //if (workerAppointment.ConfirmHealthWorkerCome_ByHimself)
            //{
            //    AppointmentWorker appointmentWorker = await _context.AppointmentWorker.FirstOrDefaultAsync(e => e.workerId == workerAppointment.workerId);
            //    appointmentWorker.totalProfitFromRealAppointment += (workerAppointment.servicePrice * (workerAppointment.PercentageFromAppointmentPriceForApp / 100));
            //    _context.Entry(appointmentWorker).State = EntityState.Modified;
            //}
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
        public async Task<IActionResult> DoesNotCome(int id)
        {
            WorkerAppointment workerAppointment = _context.WorkerAppointment.Where(x => x.id == id).FirstOrDefault();

            if (null == workerAppointment)
            {
                return NotFound();
            }
            var date = workerAppointment.appointmentDate.Split("/");
            var dateTime = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
            
            if (!(dateTime.AddHours(5) >= DateTime.Now) )
            {
                return BadRequest();
            }
            if (!workerAppointment.doesNotCome)
            {
                Patient patient = _context.Patient.Where(x => x.userId == workerAppointment.patientId).FirstOrDefault();
                patient.Balance += workerAppointment.servicePrice;
                workerAppointment.doesNotCome = true;
                workerAppointment.reservedAmountUntilConfirm = true;
                HealthcareWorker worker = _context.HealthcareWorker.Find(workerAppointment.workerId);
                worker.CountOfDoesNotCome += 1;
                if (worker.CountOfDoesNotCome >= 3)
                {
                    worker.active = false;
                    User user = _context.User.Find(worker.userId);
                    user.isActiveAccount = false;
                }
                _context.Entry(workerAppointment).State = EntityState.Modified;

                
            }

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
            Patient patient = _context.Patient.Where(x=> x.userId == workerAppointment.patientId).FirstOrDefault();
            patient.Balance = patient.Balance - workerAppointment.servicePrice;
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
