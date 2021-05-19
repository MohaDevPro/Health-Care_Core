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
        public string openTime { get; set; }
        public string clossTime { get; set; }


        public int period { get; set; }

        public int userId { get; set; }
    }
}
