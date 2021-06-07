using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Region
    {
        [Key]
        public int ID { get; set; }
        public int DistrictID { get; set; }
        public string Name { get; set; }    
    }
}
