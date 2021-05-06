using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinicDoctorViewModel
    {
        public User HospitalInfo { get; set; }
        public ClinicType ClinicInfo { get; set; }
        public List<ClinicDoctor> ClinicDoctorInfo { get; set; }
        public List<HospitalClinic> HospitalClinicInfoList { get; set; }
    }
}
