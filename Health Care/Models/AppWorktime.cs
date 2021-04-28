using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AppWorktime
    {
        public int id { get; set; }
        public int userId { get; set; }
        public char shiftAM_PM { get; set; }
        public string day { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
