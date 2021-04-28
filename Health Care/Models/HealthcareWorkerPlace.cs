using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareWorkerPlace
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int workePlaceid { get; set; } // from placetoworktable
        public char shiftAM_PM { get; set; } // when to work in workplace
        public string startTime { get; set; } // when to work in workplace
        public string endTime { get; set; } // when to work in workplace
        public string Day { get; set; } // when to work in workplace
    }
}
