using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class District
    {
        public int ID { get; set; }
        public int GovernorateID { get; set; }
        public string Name { get; set; }
        public bool active { get; set; }
    }
}
