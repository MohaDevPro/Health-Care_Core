﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class HospitalClinicWorkTime
    {
        public int id { get; set; }
        public int HospitalClinicId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
