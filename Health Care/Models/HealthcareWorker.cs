using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareAppworktime
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string BackGroundPicture { get; set; }
        public string Description { get; set; } 
        public string identificationImage { get; set; }
        public int specialityId { get; set; }
        public string graduationCertificateImage { get; set; }
        public ICollection<HealthcareWorkerService> HealthcareWorkerServices { get; set; }

    }
}
