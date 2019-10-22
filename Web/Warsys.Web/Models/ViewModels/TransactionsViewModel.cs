using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warsys.Web.Models.ViewModels
{
    public class TransactionsViewModel
    {
        public string Flowmeter  { get; set; }

        public string TransactionNumber { get; set; }

        public string EndDate { get; set; }

        public string Receipt { get; set; }
        
        public string DensityAt15 { get; set; }

        public string VolumeAt15 { get; set; }

        public string Mass { get; set; }

        public string FlowDirection { get; set; }

        
    }
}
