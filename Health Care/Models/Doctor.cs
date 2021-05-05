using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Doctor
    {
        public int id { get; set; }
        public int Userid { get; set; }

        public string name { get; set; }
        public string Picture { get; set; }

        public int appointmentPrice { get; set; }
        public int numberOfAvailableAppointment { get; set; }

    }
}
