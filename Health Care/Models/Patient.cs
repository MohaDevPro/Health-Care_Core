using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Patient
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public int Balance { get; set; }
        public string LastBalanceChargeDate { get; set; }
        public bool active { get; set; }
        public IList<ChronicDisease> ChronicDiseases { get; set; }

    }
}
