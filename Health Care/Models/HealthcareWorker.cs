using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareWorker
    {
        public HealthcareWorker()
        {
            HealthcareWorkerRegions = new List<HealthcareWorkerRegion>();
        }
        public int id { get; set; }
        public int userId { get; set; }
        public int ReagionID { get; set; } // added
        public string Name { get; set; }
        public string Gender { get; set; } // added
        public string WorkPlace { get; set; } // added
        public int specialityId { get; set; }
        public int CountOfDoesNotCome { get; set; }
        public string Picture { get; set; }
        public string BackGroundPicture { get; set; }
        public string Description { get; set; } 
        public string identificationImage { get; set; }
        public string graduationCertificateImage { get; set; }
        public bool active { get; set; }
        public ICollection<HealthcareWorkerService> HealthcareWorkerServices { get; set; }
        public ICollection<HealthcareWorkerRegion> HealthcareWorkerRegions { get; set; }

    }
}
