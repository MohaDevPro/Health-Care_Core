using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class RechargeRequest
    {
        public int id { get; set; }
        public int userId { get; set; }
        public byte[] BalanceSanad { get; set; } //صورة السند
        public string rechargeDate { get; set; }
        public bool ConfirmToAdd { get; set; }
    }
}
