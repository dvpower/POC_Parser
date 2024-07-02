using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{

    public class TransactionSet
    {
        public TransactionSetHeader Header { get; set; }
        public BeginningOfHierarchicalTransaction BeginningOfHierarchicalTransaction { get; set; }
        public HierarchicalStructure HierarchicalStructure { get; set; }
        public TransactionSetTrailer Trailer { get; set; }
        // Other properties will be added here later
    }

    public class TransactionSetHeader
    {
        public string TransactionSetIdentifierCode { get; set; } // Should be "837" for our case
        public string TransactionSetControlNumber { get; set; }
        public string ImplementationConventionReference { get; set; }
    }

    public class TransactionSetTrailer
    {
        public int NumberOfIncludedSegments { get; set; }
        public string TransactionSetControlNumber { get; set; }
    }
}
