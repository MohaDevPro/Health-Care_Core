using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class WorkerAppointment
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public int serviceId { get; set; }
        public int workerId { get; set; }
        public int regionId { get; set; }
        public bool acceptedAppointment { get; set; }
    }
}
