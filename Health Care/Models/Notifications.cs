using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Notifications
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string time { get; set; }
        public bool isRepeated { get; set; }
    }
}
