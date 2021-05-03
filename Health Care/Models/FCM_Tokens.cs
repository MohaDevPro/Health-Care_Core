using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class FCM_Tokens
    {
            public int ID { get; set; }
            public int UserID { get; set; }
            public string DeviceID { get; set; }
            public string Token { get; set; }
    }
}
