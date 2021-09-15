using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using System.Security.Cryptography.X509Certificates;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentDoctorClinicsController : ControllerBase
    {
        //To Get 5 Days
        [HttpGet("{month}/{day}/{year}")]
        public List<String> GetDatesBetween(string month, string day, string year)
        {
            string startDate = month + "/" + day + "/" + year  ;
            DateTime sd = DateTime.ParseExact(startDate, "M/d/yyyy", null);
            List<DateTime> allDates = new List<DateTime>();
            List<String> allDatesString = new List<String>();

            for (DateTime date = sd ; date <= sd.AddDays(5) ; date = date.AddDays(1))
            {   
                allDates.Add(date.Date);
                var x = date.Date.Month + "/" + date.Date.Day + "/" + date.Date.Year ;
                allDatesString.Add(x);
            }

            return allDatesString ;
        }

        private readonly Health_CareContext _context;

        public AppointmentDoctorClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentDoctorClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDoctorClinic>>> GetAppointmentDoctorClinic()
        {
            return await _context.AppointmentDoctorClinic.ToListAsync();
        }

        [HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> GetAppointmentDoctorClinicBasedOnDate(string month, string day, string year,int clinicId , int doctorId)
        {
            string searchDate = month + "/" + day + "/" + year;

            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FirstOrDefaultAsync(x=>x.appointmentDate== searchDate
            &&x.clinicId==clinicId&&x.doctorId==doctorId);

            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }
            return appointmentDoctorClinic;
        }
        //remove
        //[HttpGet("{month}/{day}/{year}/{clinicId}/{doctorId}")]
        //public async Task<ActionResult<List<DateAppointmentClinicDoctorViewModel>>> GetAppointmentDoctorClinicBasedOnDateAndClinic(string month, string day, string year, int clinicId,int doctorId)
        //{
        //    //List<> = List<>();
        //    List<String> DatesListFor5Days = GetDatesBetween(month, day, year);
        //    List<DateAppointmentClinicDoctorViewModel> li = new List<DateAppointmentClinicDoctorViewModel>();

        //    for(var x = 0; x< DatesListFor5Days.Count; x++)
        //    {
        //        var splittedDate = DatesListFor5Days[x].Split('/');
        //        string searchDate = splittedDate[0] + "/" + splittedDate[1] + "/" + splittedDate[2];

        //        var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic.FirstOrDefaultAsync(x => x.appointmentDate == searchDate && x.clinicId == clinicId && x.doctorId == doctorId);

        //        li.Add(new DateAppointmentClinicDoctorViewModel(){ date = searchDate , appointmentDoctorClinicObj = appointmentDoctorClinicObj });
        //    }

            
            
        //    if (li == null) { return NotFound(); }
        //    return li;
        //}
        //new one
        [HttpGet("{day}/{month}/{year}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> GetAppointmentDoctorClinicBasedOnDateAndClinic(string day, string month, string year, int clinicId, int doctorId)
        {
            string searchDate = day + "/" + month + "/" + year;
            var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic.FirstOrDefaultAsync(x => x.appointmentDate == searchDate && x.clinicId == clinicId && x.doctorId == doctorId);
            if (appointmentDoctorClinicObj == null) { return NotFound(); }
            return appointmentDoctorClinicObj;
        }

        [HttpGet("{hospitalid}/{clinicid}/{doctorid}/{startdate}/{finishdate}")]
        public async Task<ActionResult<IEnumerable<object>>> GetOrderByQuery(int hospitalid, int clinicid, int doctorid, string startdate, string finishdate)
        {
            var allorders = _context.AppointmentDoctorClinic.AsQueryable();
            if (hospitalid != 0)
            {
                var clinicOnHospitalIDS = _context.ExternalClinic.Where(x => x.userId == hospitalid).Select(x => x.id).ToList();
                allorders = allorders.Where(x => clinicOnHospitalIDS.Contains(x.clinicId));
            }
            if (clinicid != 0)
            {
                allorders = allorders.Where(x => x.clinicId == clinicid);
            }
            if (doctorid != 0)
            {
                allorders = allorders.Where(x => x.doctorId == doctorid);
            }
            var splitStartDate = startdate.Split('-');
            var splitfinishDate = finishdate.Split('-');
            var requestDates = HelpCalcolator.getListOfDays(new List<int>() { Convert.ToInt32(splitStartDate[0]), Convert.ToInt32(splitStartDate[1]), Convert.ToInt32(splitStartDate[2]) }, new List<int>() { Convert.ToInt32(splitfinishDate[0]), Convert.ToInt32(splitfinishDate[1]), Convert.ToInt32(splitfinishDate[2]) });
            allorders = allorders.Where(x => requestDates.Contains(x.appointmentDate));
            var doctors = await _context.Doctor.ToListAsync();
            var clinics = await _context.ExternalClinic.ToListAsync();
            var execuate = await allorders.ToListAsync();
            var test = from doctor in doctors
                       join appointment in execuate on doctor.id equals appointment.doctorId
                       join clinic in clinics on appointment.clinicId equals clinic.id
                       select new 
                       {
                           id = appointment.id,
                           //Totaly = orders.TotalyWithDiscount,
                           Date = appointment.appointmentDate,
                           ClinicName = clinic.Name,
                           DoctorName = doctor.name,
                           appointment.numberOfRealAppointment,
                           appointment.totalProfitFromRealAppointment,

                       };


            return test.ToList();
        }
        // GET: api/AppointmentDoctorClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> GetAppointmentDoctorClinic(int id)
        {
            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FindAsync(id);

            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }

            return appointmentDoctorClinic;
        }

        // PUT: api/AppointmentDoctorClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentDoctorClinic(int id, AppointmentDoctorClinic appointmentDoctorClinic)
        {
            if (id != appointmentDoctorClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(appointmentDoctorClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentDoctorClinicExists(id))
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

        // POST: api/AppointmentDoctorClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppointmentDoctorClinic>> PostAppointmentDoctorClinic(AppointmentDoctorClinic appointmentDoctorClinic)
        {
            _context.AppointmentDoctorClinic.Add(appointmentDoctorClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentDoctorClinic", new { id = appointmentDoctorClinic.id }, appointmentDoctorClinic);
        }

        // DELETE: api/AppointmentDoctorClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppointmentDoctorClinic>> DeleteAppointmentDoctorClinic(int id)
        {
            var appointmentDoctorClinic = await _context.AppointmentDoctorClinic.FindAsync(id);
            if (appointmentDoctorClinic == null)
            {
                return NotFound();
            }

            _context.AppointmentDoctorClinic.Remove(appointmentDoctorClinic);
            await _context.SaveChangesAsync();

            return appointmentDoctorClinic;
        }

        private bool AppointmentDoctorClinicExists(int id)
        {
            return _context.AppointmentDoctorClinic.Any(e => e.id == id);
        }

        //[HttpGet("{clinicId}/{doctorId}")]
        //public async Task<ActionResult<object>> GetProfitBasedOnClinicAndDoctor(int clinicId , int doctorId)
        //{

        //    var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == clinicId);
            
        //    DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

        //    var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic
        //        .Where(x => x.clinicId == clinicId && x.doctorId == doctorId).ToListAsync();

        //    //var appointmentDoctorClinicObj = await (from contract in _context.UserContract
        //    //                                        join appointmentdoctorclinic in _context.AppointmentDoctorClinic on contract.userId equals appointmentdoctorclinic.clinicId
        //    //                                        where contract.userId == clinicId
        //    //                                        //select new
        //    //                                        //{
        //    //                                        //    contract = contract,
        //    //                                        //    appointmentdoctorclinic = appointmentdoctorclinic
        //    //                                        //}
        //    //                                        ).ToListAsync();
        //    int profitSum=0;
        //    foreach (var item in appointmentDoctorClinicObj)
        //    {
        //        profitSum += item.totalProfitFromRealAppointment;
        //    };
        //    int appointmentSum=0;
        //    foreach (var item in appointmentDoctorClinicObj)
        //    {
        //        appointmentSum += item.numberOfRealAppointment;
        //    };
        //    var appointmentDoctorClinicObj2 = new
        //    {
        //        contractDate = userContractObj.contractStartDate,
        //        profitUntilNow = profitSum,
        //        numberOfRealAppointmentUntilNow = appointmentSum,
        //        //contract = userContractObj,
        //        //appointmentdoctorclinic = appointmentDoctorClinicObj
        //    };

        //    if (appointmentDoctorClinicObj2 == null) { return NotFound(); }
        //    return appointmentDoctorClinicObj2;
        //}

        [HttpGet("{hospitalId}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<object>> GetProfitBasedOnClinicAndDoctor(int hospitalId, int clinicId,int doctorId)
        {
            var appointmentDoctorClinicObjs = await _context.AppointmentDoctorClinic.ToListAsync();
            if (hospitalId != 0)
            {
                //userid = hospitalId --- id = clinicId
                var clinicOnHospitalIDS = _context.ExternalClinic.Where(x => x.userId == hospitalId).Select(x => x.id).ToList();
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => clinicOnHospitalIDS.Contains(x.clinicId)).ToList();
            }
            if (clinicId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.clinicId == clinicId).ToList();
            }
            if (doctorId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.doctorId == doctorId).ToList();
            }
            //final user id
            int final_userId = (hospitalId != 0) ? hospitalId : clinicId;

            var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == final_userId);
            DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

            //var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic
            //    .Where(x => x.clinicId == clinicId && x.doctorId == doctorId).ToListAsync();

            List<AppointmentDoctorClinic> newAppointmentDoctorClinicObj = new List<AppointmentDoctorClinic>();
            foreach (var item in appointmentDoctorClinicObjs)
            {
                DateTime appointmentDoctorClinicObjDate = DateTime.ParseExact(item.appointmentDate, "d/M/yyyy", null);

                if (appointmentDoctorClinicObjDate.CompareTo(contractstartdate)>=0)
                    newAppointmentDoctorClinicObj.Add(item);
            }

            int profitSum = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                profitSum += item.totalProfitFromRealAppointment;
            };
            int appointmentSum = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                appointmentSum += item.numberOfRealAppointment;
            };
            var appointmentDoctorClinicObj2 = new
            {
                contractDate = userContractObj.contractStartDate,
                profitUntilNow = profitSum,
                numberOfRealAppointmentUntilNow = appointmentSum,
                //contract = userContractObj,
                //appointmentdoctorclinic = appointmentDoctorClinicObj
            };

            if (appointmentDoctorClinicObj2 == null) { return NotFound(); }
            return appointmentDoctorClinicObj2;

            ////return contractstartdate.ToShortDateString();
            //if (newAppointmentDoctorClinicObj == null) { return Content("bbbb"); }
            //return newAppointmentDoctorClinicObj;
        }

        //[HttpGet("{hospitalId}/{clinicId}/{doctorId}")]
        //public async Task<ActionResult<object>> GetProfitBasedOnClinicAndDoctor2(int hospitalId, int clinicId, int doctorId)
        //{
        //    var appointmentDoctorClinicObjs = await _context.AppointmentDoctorClinic.ToListAsync();

        //    if (hospitalId != 0)
        //    {
        //        //userid = hospitalId --- id = clinicId
        //        var clinicOnHospitalIDS = _context.ExternalClinic.Where(x => x.userId == hospitalId).Select(x => x.id).ToList();
        //        appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => clinicOnHospitalIDS.Contains(x.clinicId)).ToList();
        //    }
        //    if (clinicId != 0)
        //    {
        //        appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.clinicId == clinicId).ToList();
        //    }
        //    if (doctorId != 0)
        //    {
        //        appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.doctorId == doctorId).ToList();
        //    }

        //    var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == clinicId);
        //    DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

        //    //var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic
        //    //    .Where(x => x.clinicId == clinicId && x.doctorId == doctorId).ToListAsync();

        //    List<AppointmentDoctorClinic> newAppointmentDoctorClinicObj = new List<AppointmentDoctorClinic>();
        //    foreach (var item in appointmentDoctorClinicObjs)
        //    {
        //        DateTime appointmentDoctorClinicObjDate = DateTime.ParseExact(item.appointmentDate, "d/M/yyyy", null);

        //        if (appointmentDoctorClinicObjDate.CompareTo(contractstartdate) >= 0)
        //            newAppointmentDoctorClinicObj.Add(item);
        //    }

        //    int profitSum = 0;
        //    foreach (var item in newAppointmentDoctorClinicObj)
        //    {
        //        profitSum += item.totalProfitFromRealAppointment;
        //    };
        //    int appointmentSum = 0;
        //    foreach (var item in newAppointmentDoctorClinicObj)
        //    {
        //        appointmentSum += item.numberOfRealAppointment;
        //    };
        //    var appointmentDoctorClinicObj2 = new
        //    {
        //        contractDate = userContractObj.contractStartDate,
        //        profitUntilNow = profitSum,
        //        numberOfRealAppointmentUntilNow = appointmentSum,
        //        //contract = userContractObj,
        //        //appointmentdoctorclinic = appointmentDoctorClinicObj
        //    };

        //    if (appointmentDoctorClinicObj2 == null) { return NotFound(); }
        //    return appointmentDoctorClinicObj2;

        //    ////return contractstartdate.ToShortDateString();
        //    //if (newAppointmentDoctorClinicObj == null) { return Content("bbbb"); }
        //    //return newAppointmentDoctorClinicObj;
        //}

        [HttpGet("{hospitalId}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<object>> GetProfitforClinicOrHospital(int hospitalId, int clinicId, int doctorId)
        {
            var appointmentDoctorClinicObjs = await _context.AppointmentDoctorClinic.ToListAsync();
            int medicalExaminationPercentage = await _context.ProfitRatios.Select(e => e.medicalExaminationPercentage).FirstOrDefaultAsync();
            if (hospitalId != 0)
            {
                //userid = hospitalId --- id = clinicId
                var clinicOnHospitalIDS = _context.ExternalClinic.Where(x => x.userId == hospitalId).Select(x => x.id).ToList();
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => clinicOnHospitalIDS.Contains(x.clinicId)).ToList();
            }
            if (clinicId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.clinicId == clinicId).ToList();
            }
            if (doctorId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.doctorId == doctorId).ToList();
            }
            //final user id
            int final_userId = (hospitalId != 0) ? hospitalId : clinicId;

            var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == final_userId);
            DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

            //var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic
            //    .Where(x => x.clinicId == clinicId && x.doctorId == doctorId).ToListAsync();

            List<AppointmentDoctorClinic> newAppointmentDoctorClinicObj = new List<AppointmentDoctorClinic>();
            foreach (var item in appointmentDoctorClinicObjs)
            {
                DateTime appointmentDoctorClinicObjDate = DateTime.ParseExact(item.appointmentDate, "d/M/yyyy", null);

                if (appointmentDoctorClinicObjDate.CompareTo(contractstartdate) >= 0)
                    newAppointmentDoctorClinicObj.Add(item);
            }

            int profitSumforAppAdmin = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                profitSumforAppAdmin += item.totalProfitFromRealAppointment;
            };

            int totalAmount = profitSumforAppAdmin * 100 / medicalExaminationPercentage;
            int profitSumforuser = totalAmount - profitSumforAppAdmin;


            int appointmentSum = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                appointmentSum += item.numberOfRealAppointment;
            };
            var appointmentDoctorClinicObj2 = new
            {
                contractDate = userContractObj.contractStartDate,
                profitforAppUntilNow = profitSumforAppAdmin,
                profitforUserUntilNow = profitSumforuser,
                TotalAmountUntilNow = totalAmount,
                numberOfRealAppointmentUntilNow = appointmentSum,
                //contract = userContractObj,
                //appointmentdoctorclinic = appointmentDoctorClinicObj
            };

            if (appointmentDoctorClinicObj2 == null) { return NotFound(); }
            return appointmentDoctorClinicObj2;

            ////return contractstartdate.ToShortDateString();
            //if (newAppointmentDoctorClinicObj == null) { return Content("bbbb"); }
            //return newAppointmentDoctorClinicObj;
        }

        [HttpGet("{hospitalId}/{clinicId}/{doctorId}")]
        public async Task<ActionResult<object>> GetProfitforWorker(int hospitalId, int clinicId, int doctorId)
        {
            var appointmentDoctorClinicObjs = await _context.AppointmentDoctorClinic.ToListAsync();
            int medicalExaminationPercentage = await _context.ProfitRatios.Select(e => e.medicalExaminationPercentage).FirstOrDefaultAsync();
            if (hospitalId != 0)
            {
                //userid = hospitalId --- id = clinicId
                var clinicOnHospitalIDS = _context.ExternalClinic.Where(x => x.userId == hospitalId).Select(x => x.id).ToList();
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => clinicOnHospitalIDS.Contains(x.clinicId)).ToList();
            }
            if (clinicId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.clinicId == clinicId).ToList();
            }
            if (doctorId != 0)
            {
                appointmentDoctorClinicObjs = appointmentDoctorClinicObjs.Where(x => x.doctorId == doctorId).ToList();
            }
            //final user id
            int final_userId = (hospitalId != 0) ? hospitalId : clinicId;

            var userContractObj = await _context.UserContract.FirstOrDefaultAsync(x => x.userId == final_userId);
            DateTime contractstartdate = DateTime.ParseExact(userContractObj.contractStartDate, "d/M/yyyy", null);

            //var appointmentDoctorClinicObj = await _context.AppointmentDoctorClinic
            //    .Where(x => x.clinicId == clinicId && x.doctorId == doctorId).ToListAsync();

            List<AppointmentDoctorClinic> newAppointmentDoctorClinicObj = new List<AppointmentDoctorClinic>();
            foreach (var item in appointmentDoctorClinicObjs)
            {
                DateTime appointmentDoctorClinicObjDate = DateTime.ParseExact(item.appointmentDate, "d/M/yyyy", null);

                if (appointmentDoctorClinicObjDate.CompareTo(contractstartdate) >= 0)
                    newAppointmentDoctorClinicObj.Add(item);
            }

            int profitSumforAppAdmin = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                profitSumforAppAdmin += item.totalProfitFromRealAppointment;
            };

            int totalAmount = profitSumforAppAdmin * 100 / medicalExaminationPercentage;
            int profitSumforuser = totalAmount - profitSumforAppAdmin;


            int appointmentSum = 0;
            foreach (var item in newAppointmentDoctorClinicObj)
            {
                appointmentSum += item.numberOfRealAppointment;
            };
            var appointmentDoctorClinicObj2 = new
            {
                contractDate = userContractObj.contractStartDate,
                profitforAppUntilNow = profitSumforAppAdmin,
                profitforUserUntilNow = profitSumforuser,
                TotalAmountUntilNow = totalAmount,
                numberOfRealAppointmentUntilNow = appointmentSum,
                //contract = userContractObj,
                //appointmentdoctorclinic = appointmentDoctorClinicObj
            };

            if (appointmentDoctorClinicObj2 == null) { return NotFound(); }
            return appointmentDoctorClinicObj2;

            ////return contractstartdate.ToShortDateString();
            //if (newAppointmentDoctorClinicObj == null) { return Content("bbbb"); }
            //return newAppointmentDoctorClinicObj;
        }
    }
}
