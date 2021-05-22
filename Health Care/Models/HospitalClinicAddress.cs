using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinicAddress
    {
        public int id { get; set; }
        public int hospitalOrClinicId { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string detailedAddress { get; set; }
    }
}
