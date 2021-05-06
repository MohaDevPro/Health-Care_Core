using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinicDoctor
    {
        public int id { get; set; }
        public int hospitalClinicId { get; set; }
        public int doctorId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string Day { get; set; }
    }
}
