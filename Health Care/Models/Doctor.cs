using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Doctor
    {
        public int id { get; set; }
        public string name { get; set; }
        public int specialityId { get; set; }
        public string detailedSpecialityId { get; set; }
    }
}
