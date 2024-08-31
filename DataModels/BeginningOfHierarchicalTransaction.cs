using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class BeginningOfHierarchicalTransaction
    {
        public string HierarchicalStructureCode { get; set; }
        public string TransactionSetPurposeCode { get; set; }
        public string TransactionSetPurposeCodeDescription { get; set; }
        public string ReferenceIdentification { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeCodeDescription { get; set; }
    }
}
