using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ReportAndComplaintViewModel
    {
        public List<ReportAndComplaint> AnsweredReportAndComplaint { get; set; }
        public List<ReportAndComplaint> UnAnsweredReportAndComplaint { get; set; }
    }
}
