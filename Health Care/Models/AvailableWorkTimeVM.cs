using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AvailableWorkTimeVM
    {
        public bool sat { get; set; }
        public bool sun { get; set; }
        public bool mon { get; set; }
        public bool tue { get; set; }
        public bool wed { get; set; }
        public bool thur { get; set; }
        public bool fri { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public bool morningEvening { get; set; }
        public string shiftAMPM { get; set; }
        public string RealOpenTime { get; set; }
        public string RealClossTime { get; set; }
        public bool AM { get; set; }
        public bool PM { get; set; }
        public int clinicid { get; set; }
        public int userId { get; set; }
    }
}
