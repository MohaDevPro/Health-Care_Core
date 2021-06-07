using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthWorkerRequestByUser
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public string RequestTime { get; set; }
        public string RequestDate { get; set; }
    }
}
