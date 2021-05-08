using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareWorkerService
    {
        public int id { get; set; }
        public int HealthcareWorkerid { get; set; }
        public int userId { get; set; }
        public int serviceId { get; set; }
        public int Price { get; set; }
        //public ICollection<HealthcareWorker> HealthcareWorker { get; set; }
    }
}
