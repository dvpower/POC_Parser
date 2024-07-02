using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class TransactionSetParser
    {
        private readonly STParser _stParser;
        private readonly BHTParser _bhtParser;
        private readonly HierarchicalStructureParser _hierarchicalStructureParser;
        private readonly SEParser _seParser;

        public TransactionSetParser()
        {
            _stParser = new STParser();
            _bhtParser = new BHTParser();
            _hierarchicalStructureParser = new HierarchicalStructureParser();
            _seParser = new SEParser();
        }

        public TransactionSet Parse(string[] lines)
        {
            var transactionSet = new TransactionSet();

            string stLine = _stParser.FindSTSegment(lines);
            string bhtLine = _bhtParser.FindBHTSegment(lines);
            string seLine = _seParser.FindSESegment(lines);

            if (stLine == null || seLine == null)
            {
                throw new InvalidOperationException("ST or SE segment not found");
            }

            transactionSet.Header = _stParser.Parse(stLine);
            transactionSet.BeginningOfHierarchicalTransaction = _bhtParser.Parse(bhtLine);
            transactionSet.HierarchicalStructure = _hierarchicalStructureParser.Parse(lines);
            transactionSet.Trailer = _seParser.Parse(seLine);

            // Sanity checks
            if (transactionSet.Header.TransactionSetControlNumber != transactionSet.Trailer.TransactionSetControlNumber)
            {
                throw new InvalidOperationException("Transaction Set Control Numbers in ST and SE do not match");
            }

            if (transactionSet.BeginningOfHierarchicalTransaction.HierarchicalStructureCode != "0019")
            {
                throw new InvalidOperationException("Invalid Hierarchical Structure Code for 837 transaction");
            }


            // Other parsing logic will be added here later

            return transactionSet;
        }
    }
}
