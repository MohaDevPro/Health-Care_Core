using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Doctor
    {
        public int id { get; set; }
        public int Userid { get; set; }

        public string name { get; set; }

        public string Pictue { get; set; }
        
    }
}
