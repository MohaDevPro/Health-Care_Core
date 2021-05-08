using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Favorite
    {
        public int id { get; set; }
        public string type { get; set; }

        public int PatientId { get; set; }
        public int UserId { get; set; }


    }
}
