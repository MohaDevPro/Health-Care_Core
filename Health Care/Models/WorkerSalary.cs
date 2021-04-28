using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class WorkerSalary
    {
        public int id { get; set; }
        public int workerId { get; set; } // from user
        public int salary { get; set; } // based on service he accomplished -- updated every time patient confirm that worker did the service
        public bool isPaid { get; set; }
    }
}
