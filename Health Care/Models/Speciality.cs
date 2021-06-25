using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Speciality
    {
        public int id { get; set; }
        public string specialityName { get; set; }
        public int BasicSpecialityID { get; set; }
        public bool isBasic { get; set; }
        public bool active { get; set; }
    }
}
