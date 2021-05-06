using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinic
    {
        public int id { get; set; }
        //public string Name { get; set; }
        //public string Picture { get; set; }
        //public string Description { get; set; }
        public int hospitalId { get; set; } //from user table
        public int clinicId { get; set; } //from clinicType table

        public int appointmentPrice { get; set; }
        public int numberOfAvailableAppointment { get; set; }

    }
}
