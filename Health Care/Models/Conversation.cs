using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Conversation
    {
        public int id { get; set; }
        public int userIdFrom { get; set; }
        public int userIdTo { get; set; }
        public int appointmentId { get; set; }
        public string message { get; set; } // patient id
        public bool isRecived { get; set; } 
        public bool isReaded { get; set; } 
        public string messageDate { get; set; } // patient id
        public string messageTime { get; set; } // patient id
    }
}
