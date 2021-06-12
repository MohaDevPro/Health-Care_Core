using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Hospital
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string BackgoundImage { get; set; }

        public string Description { get; set; }
        public bool active { get; set; }
        public int hospitalId { get; set; } //from user table

        public ICollection<HospitalDepartments> hospitalDepartments { get; set; }
    }
}
