using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthCareWorkerAppWorkTime
    {
        public int id { get; set; }
        public int userId { get; set; } // healthcareWorker id
        public char shiftAM_PM { get; set; }
        public int day { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string RealOpenTime { get; set; }
        public string RealClossTime { get; set; }
    }
}
