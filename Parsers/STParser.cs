using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{  

    public class STParser
    {
        public TransactionSetHeader Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("ST*"))
            {
                throw new ArgumentException("Invalid ST segment");
            }

            string[] elements = line.Split('*');

            if (elements.Length < 3)
            {
                throw new ArgumentException("ST segment has too few elements");
            }

            return new TransactionSetHeader
            {
                TransactionSetIdentifierCode = elements[1],
                TransactionSetControlNumber = elements[2],
                ImplementationConventionReference = elements.Length > 3 ? elements[3].TrimEnd('~') : null
            };
        }

        public string FindSTSegment(string[] lines)
        {
            return Array.Find(lines, l => l.StartsWith("ST*"));
        }
    }
}
