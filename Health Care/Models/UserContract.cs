using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class UserContract
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int contractId { get; set; }
        public string contractStartDate { get; set; } // 1 year
        public string contractEndDate { get; set; }

        //public virtual User user { get; set; }

        //public ICollection<Role> Roles { get; set; }
    }
}
