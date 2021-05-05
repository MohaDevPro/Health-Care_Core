using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ExternalClinic
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int ClinicTypeId { get; set; }// عيادة أسنان ، عيادة أمراض هضمية
        public int userId { get; set; } //clinicId ==>Or HospitalID from user table #Mohamed KHaled
        public int doctorId { get; set; } //to get the doctor owner
        public int appointmentPrice { get; set; }
        public int numberOfAvailableAppointment { get; set; }

    }
}
