using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ProfitFromTheApp
    {
        public int id { get; set; }
        public int hospitalId { get; set; } //if hospital 0 means it's an external clinic else clinic 
        public int clinicId { get; set; }
        public int doctorId { get; set; }
        public int sumOfAppointment { get; set; }
        public int profit { get; set; }

        //public int userId { get; set; }
        //public int balance { get; set; }

        

    }
}
