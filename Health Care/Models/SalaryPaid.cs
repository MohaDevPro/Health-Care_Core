using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class SalaryPaid
    {
        public int Id { get; set; }
        public int userID { get; set; }
        public string DateOfPay { get; set; }
        public int AmountOfSalary { get; set; }
    }
}   
