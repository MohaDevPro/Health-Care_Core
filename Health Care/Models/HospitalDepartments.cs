using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalDepartments
    {
        public int id { get; set; }
        public int DepatmentsOfHospitalID { get; set; }
        public int Hospitalid { get; set; }
        public string Picture { get; set; }
        public string Background { get; set; }


    }
}
