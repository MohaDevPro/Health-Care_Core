using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Service
    {
        public int id { get; set; }
        public string serviceName { get; set; }
        public int servicePrice { get; set; }

        public ICollection<HealthcareWorkerService> HealthcareWorkers { get; set; }
    }
}
