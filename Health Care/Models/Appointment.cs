using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Appointment
    {
        public int id { get; set; }
        public int userId { get; set; } // patient id
        public int distnationUserId { get; set; } // Distnation id (hospital or clinic ... )
        public string appointmentTime { get; set; } // put it here to check the time of clinic available time
        public string appointmentDate { get; set; }
        public bool Paid { get; set; }
        public bool UserComeToAppointment { get; set; }

    }
}
