using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HealthcareWorker
    {
        public int id { get; set; }
        public int userId { get; set; }
        public byte[] identificationImage { get; set; }
        public int specialityId { get; set; }
        public byte[] graduationCertificateImage { get; set; }
    }
}
