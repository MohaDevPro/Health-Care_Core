using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ChronicDisease
    {
        public int ID { get; set; }

        public string name { get; set; }
        public Patient Patient { get; set; }

    }
}
