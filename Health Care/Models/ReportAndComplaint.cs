using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ReportAndComplaint
    {
        public int id { get; set; }
        public string Type { get; set; }
        public int userId { get; set; } //مقدم الطلب
        public string title { get; set; }
        public string description { get; set; }
        public string replyTextByAdmin { get; set; }
        public string ReportAndComplaintDate { get; set; }
        public string ReportAndComplaintTime { get; set; }
        public bool isAnswered { get; set; }

    }
}
