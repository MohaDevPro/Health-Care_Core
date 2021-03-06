using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class AppWorktime
    {
        public int id { get; set; }
        public int userId { get; set; } // doctor id
        public int ExternalClinicId { get; set; }
        public string shiftAM_PM { get; set; }
        public int day { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public bool IsAdditional { get; set; }
        public string RealOpenTime { get; set; }
        public string RealClossTime { get; set; }
        public int numberofAvailableAppointment { get; set; }

        public virtual ExternalClinic ExternalClinic { get; set; }
    }
}
