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
        //public string contractFor { get; set; } //عامل صحي ، دكتور، مستشفى ....
        public int contractFor { get; set; } // حسب الroleid

        //public string contractPath { get; set; } // contract document
        public string note { get; set; }
        public bool active { get; set; }
        public ICollection<ContractTerm> ContractTerms { get; set; }
    }
}
