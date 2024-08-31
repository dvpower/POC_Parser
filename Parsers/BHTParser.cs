using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _837ParserPOC.DataModels;

namespace _837ParserPOC.Parsers
{
    

    public class BHTParser
    {
        public BeginningOfHierarchicalTransaction Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("BHT*"))
            {
                throw new ArgumentException("Invalid BHT segment");
            }

            string[] elements = line.Split('*');

            if (elements.Length < 6)
            {
                throw new ArgumentException("BHT segment has too few elements");
            }


            var transactionTypeCode = elements.Length > 6 ? elements[6].TrimEnd('~') : null;
            return new BeginningOfHierarchicalTransaction
            {
                HierarchicalStructureCode = elements[1],
                TransactionSetPurposeCode = elements[2],
                TransactionSetPurposeCodeDescription = TransactionSetPurposeCodeQualifiers.GetDescription(elements[2]),
                ReferenceIdentification = elements[3],
                Date = EDIParserHelper.ParseDate(elements[4]),
                Time = EDIParserHelper.ParseTime(elements[5]),
                TransactionTypeCode = transactionTypeCode,
                TransactionTypeCodeDescription = TransactionTypeCodeQualifiers.GetDescription(transactionTypeCode)
        };
        }

        public string FindBHTSegment(string[] lines)
        {
            return Array.Find(lines, l => l.StartsWith("BHT*"));
        }
    }
}
