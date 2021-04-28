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
        public int distnationUserId { get; set; } // Distnation id
        public bool Paid { get; set; }
        public bool UserComeToAppointment { get; set; }

    }
}
