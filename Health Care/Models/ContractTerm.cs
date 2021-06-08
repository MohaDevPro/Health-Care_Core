using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ContractTerm
    {
        public int id { get; set; }
        public int contractID { get; set; }
        public string term { get; set; }


        public Contract Contract { get; set; }
    }
}
