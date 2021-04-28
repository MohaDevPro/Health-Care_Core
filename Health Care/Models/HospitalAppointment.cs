using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalAppointment
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public string appointmentTime { get; set; } // put it here to check the time of clinic available time
        public string appointmentDate { get; set; }
        public int clinicId { get; set; }
        public int doctorid { get; set; }
        public bool Paid { get; set; }
        public bool UserComeToAppointment { get; set; } // doctor confirm or hospital confirm

    }
}
