using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ExternalClinic
    {
        public int id { get; set; }
        public int ClinicTypeId { get; set; }
        public int clinicId { get; set; } //from user table

    }
}
