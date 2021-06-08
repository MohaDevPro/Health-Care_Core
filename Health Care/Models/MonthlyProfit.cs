using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class MonthlyProfit
    {
        public int id { get; set; }
        public int ProfitFromTheAppId { get; set; }
        public int FullProfitFromTheAppId { get; set; }
        public int MonthlyProfitForApp { get; set; }
        public int MonthlyProfitForClinincThroughApp { get; set; }

    }
}
