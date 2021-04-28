using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class Contract
    {
        public int id { get; set; }
        /* ودا يكون شي ثايت في التطبيق يسوي موافقة ولو وافق ينحط الآي دي حق الكونتراكت واذا مافي الآي دي حق الكونتراكت يصير مايتفعل حسابه 
         * عقد عامل صحي ، عقد مستشفى */
        public string Type { get; set; } 
        
        public string contract { get; set; }

    }
}
