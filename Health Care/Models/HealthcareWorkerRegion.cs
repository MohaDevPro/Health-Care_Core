using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareWorkerRegion
    {
        public int id { get; set; }
        
        public HealthcareWorker HealthcareWorker { get; set; }
        public int RegionId { get; set; }
    }
}
