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
    public class AppointmentsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public AppointmentsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointment()
        {
            return await _context.Appointment.Where(a => a.active == true).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDisabled()
        {
            return await _context.Appointment.Where(a=>a.active == false).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllPatintComeToAppointment(int doctorId,string dateFrom,string dateTo)
        {

            var allorders = _context.Appointment.AsQueryable();
            var splitStartDate = dateFrom.Split('/');
            var splitfinishDate = dateTo.Split('/');
            var requestDates = HelpCalcolator.getListOfDays(new List<int>() { Convert.ToInt32(splitStartDate[0]), Convert.ToInt32(splitStartDate[1]), Convert.ToInt32(splitStartDate[2]) }, new List<int>() { Convert.ToInt32(splitfinishDate[0]), Convert.ToInt32(splitfinishDate[1]), Convert.ToInt32(splitfinishDate[2]) });
            allorders = allorders.Where(x => requestDates.Contains(x.appointmentDate));
            var result =await  allorders.ToListAsync();
            var data = from appointment in result
                       where appointment.PatientComeToAppointment == true && appointment.doctorId == doctorId
                       select appointment;
            return data.ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllPatintComeToAppointmentForDoctorClinic(int doctorId,int clinicId, string dateFrom, string dateTo)
        {

            var allorders = _context.Appointment.AsQueryable();
            var splitStartDate = dateFrom.Split('/');
            var splitfinishDate = dateTo.Split('/');
            var requestDates = HelpCalcolator.getListOfDays(new List<int>() { Convert.ToInt32(splitStartDate[0]), Convert.ToInt32(splitStartDate[1]), Convert.ToInt32(splitStartDate[2]) }, new List<int>() { Convert.ToInt32(splitfinishDate[0]), Convert.ToInt32(splitfinishDate[1]), Convert.ToInt32(splitfinishDate[2]) });
            allorders = allorders.Where(x => requestDates.Contains(x.appointmentDate));
            var result = await allorders.ToListAsync();
            var data = from appointment in result
                       where appointment.PatientComeToAppointment == true && appointment.doctorId == doctorId && appointment.distnationClinicId==clinicId
                       select appointment;


            return data.ToList();
        }

        [HttpPut]
        //[Authorize(Roles = "admin, service")]
        public async Task<IActionResult> RestoreService(List<Appointment> appointement)
        {
            if (appointement.Count == 0)
                return NoContent();

            try
            {
                foreach (Appointment item in appointement)
                {
                    var s = _context.Appointment.Where(s => s.id == item.id).FirstOrDefault();
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
        // GET: api/Appointments/5
        [HttpGet("{userId}/{isAccepted}/{cancelByUser}/{canceledBySecretary}/{isPaid}/{pageKey}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnUserId(int userId, bool isAccepted, bool cancelByUser, bool canceledBySecretary,bool isPaid,int pageKey,int pageSize)
        {
            var appointment = await(from appointments in  _context.Appointment join
                                    Clinic in _context.ExternalClinic on appointments.distnationClinicId equals Clinic.id
                                    join doctor in _context.Doctor on appointments.doctorId equals doctor.id
                                    where appointments.userId==userId 
                                    select new {
                                        appointment=appointments,
                                        Doctor=new {Clinic.id,Clinic.Name},
                                        doctorName=doctor.name
                                    }
                                    
                                    ).ToListAsync();
            if (cancelByUser || canceledBySecretary)
                appointment= appointment.Where(x => x.appointment.cancelledByUser || x.appointment.cancelledByClinicSecretary).OrderByDescending(x=>x.appointment.id).ToList();
            else if (isPaid)
                appointment= appointment.Where(x => x.appointment.Paid).OrderByDescending(x=>x.appointment.id).ToList();
            else if(isAccepted) 
                appointment= appointment.Where(x => x.appointment.Accepted&& x.appointment.Paid==false).OrderByDescending(x => x.appointment.id).ToList();
            else appointment= appointment.Where(x => x.appointment.Accepted == false && x.appointment.cancelledByUser == false && x.appointment.cancelledByClinicSecretary == false).OrderByDescending(x=>x.appointment.id).ToList();



            if (pageSize != 0)
                return appointment.Skip(pageKey).Take(pageSize).ToList();
            else return appointment;
            //if (appointment == null) { return NotFound(); }
            //return appointment;
        }
        [HttpGet("{appointmentDoctorClinicid}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentBasedOnappointmentDoctorClinicid(int appointmentDoctorClinicid)
        {
            var appointment = await _context.Appointment.Where(x => x.appointmentDoctorClinicId == appointmentDoctorClinicid+ "" && x.active == true).ToListAsync();
            if (appointment == null) { return NotFound(); }
            return appointment;
        }

        // GET: api/Appointments/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnStatusByUserId(int userId)
        {
            List <List<Appointment>> li = new List <List<Appointment>>();

            var ConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted==true && x.cancelledByUser == false && x.active == true).ToListAsync();
            var unConfirmedAppointment = await _context.Appointment.Where(x => x.userId == userId && x.Accepted==false && x.cancelledByUser== false && x.active == true).ToListAsync();
            var cancelledAppointment = await _context.Appointment.Where(x => x.userId == userId && x.cancelledByUser== true && x.active == true).ToListAsync();
            
            li.Add(ConfirmedAppointment);
            li.Add(unConfirmedAppointment);
            li.Add(cancelledAppointment);
            
            if (li == null) { return NotFound(); }
            return li;
        }


        // GET: api/Appointments/5
        [HttpGet("{clinicId}/{status}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnClinicId(int clinicId,int status)
        {
            List<List<Appointment>> li = new List<List<Appointment>>();

            var appointment = await _context.Appointment.ToListAsync();

            var ConfirmedAppointment = appointment.Where(x => x.distnationClinicId == clinicId
            && x.Accepted == true && x.cancelledByUser == false && x.cancelledByClinicSecretary== false && x.active == true).OrderBy(x=> x.appointmentDate).ToList();

            var unConfirmedAppointment = appointment.Where(x => x.distnationClinicId == clinicId
            && x.Accepted == false && x.cancelledByUser == false && x.cancelledByClinicSecretary== false && x.active == true).OrderBy(x => x.appointmentDate).ToList();

            var cancelledAppointmentByUser = appointment.Where(x => x.distnationClinicId == clinicId
            && x.cancelledByUser == true && x.active == true).OrderBy(x => x.appointmentDate).ToList();
            
            var cancelledAppointmentBySecretary = appointment.Where(x => x.distnationClinicId == clinicId
            && x.cancelledByUser == false && x.cancelledByClinicSecretary == true && x.active == true).OrderBy(x => x.appointmentDate).ToList();
            
            li.Add(ConfirmedAppointment);
            li.Add(unConfirmedAppointment);
            li.Add(cancelledAppointmentByUser);
            li.Add(cancelledAppointmentBySecretary);
            if (status != 5)
            {
               return li[status];
            }

            if (li == null) { return NotFound(); }
            return li;
        }
        [HttpGet("{doctorid}/{status}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointmentBasedOnDoctorid(int doctorid, int status)
        {
            List<List<Appointment>> li = new List<List<Appointment>>();

            var appointment = await _context.Appointment.ToListAsync();

            var ConfirmedAppointment = appointment.Where(x => x.doctorId == doctorid
            && x.Accepted == true && x.cancelledByUser == false && x.cancelledByClinicSecretary == false && x.active == true).OrderBy(x => x.appointmentDate).ToList();

            var unConfirmedAppointment = appointment.Where(x => x.doctorId == doctorid
            && x.Accepted == false && x.cancelledByUser == false && x.cancelledByClinicSecretary == false && x.active == true).OrderBy(x => x.appointmentDate).ToList();

            var cancelledAppointmentByUser = appointment.Where(x => x.doctorId == doctorid
            && x.cancelledByUser == true && x.active == true).OrderBy(x => x.appointmentDate).ToList();

            var cancelledAppointmentBySecretary = appointment.Where(x => x.doctorId == doctorid
            && x.cancelledByUser == false && x.cancelledByClinicSecretary == true && x.active == true).OrderBy(x => x.appointmentDate).ToList();

            li.Add(ConfirmedAppointment);
            li.Add(unConfirmedAppointment);
            li.Add(cancelledAppointmentByUser);
            li.Add(cancelledAppointmentBySecretary);
            if (status != 5)
            {
                return li[status];
            }

            if (li == null) { return NotFound(); }
            return li;
        }
        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentBasedOnDate(string month, string day, string year, int clinicId, int doctorId)
        {
            string searchDate = day + "/" + month + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId && x.active == true ).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId},{code}")]
        public async Task<ActionResult<Appointment>> getAppointmentBasedOnCode(string month, string day, string year, int clinicId, int doctorId,string code)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentList = await _context.Appointment
                .FirstOrDefaultAsync(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId && x.Accepted == true && x.Paid == false && x.active == true && x.CodeConfirmation==code);

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetConfirmedAppointmentBasedOnDate(string month, string day, string year, int clinicId, int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId 
                       && x.Accepted==true&& x.Paid == false && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{day}/{month}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentBasedOnDateToCancelbyClinic(string day, string month, string year, int clinicId, int doctorId)
        {
            string searchDate = day + "/" + month + "/" + year;

            var appointmentList = await _context.Appointment
                .Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId 
                && x.cancelledByUser==false && x.cancelledByClinicSecretary== false && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("{Month}/{doctorid}")]
        public async Task<ActionResult<object>> GetAppointmentMonthRecords(int Month,int doctorid)
        {
            var appointmentOfMonth = _context.Appointment.Where(x => x.appointmentDate.Contains("/" + Month + "/")).ToList();
            var NoRepittedPatientAppointment = new List<Appointment>();
            //foreach (var i in appointmentOfMonth)
            //{
            //    if (NoRepittedPatientAppointment.FirstOrDefault(x => x.patientId == i.patientId && x.workerId == i.workerId) == null)
            //    {
            //        NoRepittedPatientAppointment.Add(i);
            //    }

            //}
            var doctors = _context.Doctor.Where(x => x.active).ToList();
            if (appointmentOfMonth.Count() > 0 && doctors.Count() > 0)
            {
                var MonthRecords = new List<object>();
                if (doctorid != 0)
                {
                    doctors = doctors.Where(x => x.id == doctorid).ToList();
                }
                foreach (var doctor in doctors)
                {
                    var subAppointment = appointmentOfMonth.Where(x => x.doctorId == doctor.id &&x.PatientComeToAppointment);
                    var subNoRepittedPatientAppointment = appointmentOfMonth.Where(x => x.doctorId == doctor.id);

                    var TotalyPrice = subAppointment.Select(x => x.appointmentPrice).Sum();
                    var BinefetOfApp = subAppointment.Select(x => x.appointmentPrice * x.PercentageFromAppointmentPriceForApp / 100).Sum();
                    var BinefetOfHealthWorker = subAppointment.Select(x => x.appointmentPrice * (100 - x.PercentageFromAppointmentPriceForApp) / 100).Sum();
                    MonthRecords.Add(new
                    {
                        healthWorkerid = doctor.id,
                        NumberOfServices = subNoRepittedPatientAppointment.Count(),
                        HealthWorkerName = doctor.name,
                        NumberOfUsers = subAppointment.Count(),
                        TotalyPrice ,
                        BinefetOfApp ,
                        BinefetOfHealthWorker ,
                    });
                }

                return MonthRecords;
            }
            return new List<object>();

        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment>> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.id)
            {
                return BadRequest();
            }

             _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppointment", new { id = appointment.id }, appointment);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> ConfirmByPatient(int id)
        {
            Appointment appointment = _context.Appointment.Where(x => x.id == id).FirstOrDefault();
            if (null == appointment)
            {
                return NotFound();
            }
            appointment.PatientComeToAppointment = true;
            appointment.Paid = true;
            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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
         
        // POST: api/Appointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            Random random = new Random();
            DateTime d = DateTime.Now;
            appointment.CodeConfirmation = $"{d.ToString("dd")}{d.ToString("MM")}{d.ToString("yyyy")}-{random.Next(1000000, 9999999)}";
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.id }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment.active = false;
            //_context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        [HttpGet("{day}/{month}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<List<Appointment>>> DeleteAppointmentBasedOnDate(string day, string month, string year, int clinicId, int doctorId)
        {
            string searchDate = day + "/" + month + "/" + year;
            var appointmentList = await _context.Appointment.Where(x => x.appointmentDate == searchDate && x.distnationClinicId == clinicId && x.doctorId == doctorId
                && x.cancelledByUser == false && x.cancelledByClinicSecretary == false && x.active == true).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }
            var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic.FirstOrDefaultAsync(x => x.appointmentDate == searchDate && x.clinicId == clinicId && x.doctorId == doctorId);
            appointmentDoctorClinicObj.numberOfRealAppointment-= appointmentList.Count;

            foreach (var appointment in appointmentList)
            {
                appointment.active = false;
                
                await _context.SaveChangesAsync();
                
            }

            return appointmentList;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.id == id);
        }
    }
}
