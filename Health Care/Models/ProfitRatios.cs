using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ProfitRatios
    {
        public int id { get; set; }
        public int medicalExaminationPercentage { get; set; } // الكشف
        public int servicePercentage { get; set; } // الخدمة الطبية
    }
}
