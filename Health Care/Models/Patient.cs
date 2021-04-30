using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Patient
    {
        public int id { get; set; }
        public int userId { get; set; }
        public char gender { get; set; }
        public string birthDate { get; set; }
        public string chronicDiseases { get; set; }
        public int Balance { get; set; }
        public string LastBalanceChargeDate { get; set; }
    }
}
