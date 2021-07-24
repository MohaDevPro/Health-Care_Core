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
        public string NumberOfReceipt { get; set; }  // رقم الحوالة
        public string BalanceReceiptImage { get; set; } //صورة السند
        public int BalanceReceipt { get; set; } //مبلغ الشحن
        public string rechargeDate { get; set; }
        public bool ConfirmToAddBalance { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsRestore { get; set; }
        public string ResounOfCancel { get; set; }


    }
}
