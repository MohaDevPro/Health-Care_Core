using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ExternalClinic
    {
        public int id { get; set; }
        public int ClinicTypeId { get; set; }// عيادة أسنان ، عيادة أمراض هضمية
        public int userId { get; set; } //clinicId
        public int doctorId { get; set; } //to get the doctor owner
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int appointmentPrice { get; set; }
        public int numberOfAvailableAppointment { get; set; }

    }
}
