using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalAppointment
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public int clinicId { get; set; }
        public int doctorid { get; set; }

    }
}
