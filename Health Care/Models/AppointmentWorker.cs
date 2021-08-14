using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AppointmentWorker
    {
        public int id { get; set; }
        public int workerId { get; set; }
        public string appointmentDate { get; set; }
        public int numberOfAvailableAppointment { get; set; }
        public int numberOfRealAppointment { get; set; }
        public int totalProfitFromRealAppointment { get; set; }

    }
}
