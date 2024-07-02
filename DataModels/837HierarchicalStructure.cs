using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class HierarchicalStructure
    {
        public List<BillingProvider> BillingProviders { get; set; } = new List<BillingProvider>();
    }
}
