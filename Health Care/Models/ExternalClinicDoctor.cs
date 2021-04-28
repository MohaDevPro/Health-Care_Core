using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ExternalClinicDoctor
    {
        public int id { get; set; }
        public int doctorId { get; set; } // from user table
        public int externalClinicId { get; set; } //from usertable
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int appointmentPrice { get; set; }
        
    }
}
