using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinicDoctorViewModel
    {
        public HospitalClinic HospitalInfo { get; set; } //done
        //public List<ClinicType> ClinicInfo { get; set; }
        //public List<ClinicDoctor> ClinicDoctorInfo { get; set; }
        public List<ExternalClinic> HospitalClinicInfoList { get; set; }
    }
}
