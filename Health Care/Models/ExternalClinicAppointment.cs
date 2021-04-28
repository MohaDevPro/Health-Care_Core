using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ExternalClinicAppointment
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public string appointmentTime { get; set; } // put it here to check the time of clinic available time
        public string appointmentDate { get; set; }
        
    }
}
