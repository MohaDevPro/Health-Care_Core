using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class UserRequestVM
    {
        public User user { get; set; }
        public DoctorClinicReqeust request { get; set; }
    }
}
