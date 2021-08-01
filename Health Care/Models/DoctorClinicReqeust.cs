using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class DoctorClinicReqeust
    {
        public int id { get; set; }

        public int FromID { get; set; }

        public int ToID { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsCanceled { get; set; }

        public string CancelResoun { get; set; }

        public int ClinicID { get; set; }// if the request from hospital this specifiy Clinic inside the hospital




    }
}
