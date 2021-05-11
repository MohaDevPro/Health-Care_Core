using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AppointmentDoctorClinic
    {
        public int id { get; set; }
        public int clinicId { get; set; }
        public int doctorId { get; set; }
        public string appointmentDate { get; set; }
        public int numberOfAvailableAppointment { get; set; }
        public int numberOfRealAppointment { get; set; }

    }
}
