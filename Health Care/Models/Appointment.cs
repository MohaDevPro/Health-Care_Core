using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Appointment
    {
        public int id { get; set; }
        public int userId { get; set; } // patient id
        public int distnationClinicId { get; set; } // Distnation id (clinic inside hospital or external clinic ... )
        public int doctorId { get; set; } 
        public int appointmentPrice { get; set; } 
        public string appointmentDate { get; set; }
        public string TypeOfAppointment { get; set; }
        public bool appointmentForUserHimself { get; set; }
        public string appointmentStartFrom { get; set; }
        public string appointmentUntilTo { get; set; }
        public string patientName { get; set; }
        public string patientphone { get; set; }
        public string visitReason { get; set; }
        public bool PatientComeToAppointment { get; set; }

        public bool Paid { get; set; }
        public bool Accepted { get; set; }
        public bool cancelledByUser { get; set; }

    }
}
