using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class ChargeOrRechargeRequest
    {
        public int id { get; set; }
        public int userId { get; set; }
        public byte[] BalanceReceipt { get; set; } //صورة السند
        public string rechargeDate { get; set; }
        public bool ConfirmToAddBalance { get; set; }
    }
}
