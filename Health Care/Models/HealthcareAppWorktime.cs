using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AppWorktime
    {
        public int id { get; set; }
        public int userId { get; set; } // doctor id
        public int clinicId { get; set; }
        public char shiftAM_PM { get; set; }
        public int day { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string RealOpenTime { get; set; }
        public string RealClossTime { get; set; }

    }
}
