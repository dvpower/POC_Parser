using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{

    using System;

    public class SEParser
    {
        public TransactionSetTrailer Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("SE*"))
            {
                throw new ArgumentException("Invalid SE segment");
            }

            string[] elements = line.Split('*');

            if (elements.Length != 3)
            {
                throw new ArgumentException("SE segment has incorrect number of elements");
            }

            return new TransactionSetTrailer
            {
                NumberOfIncludedSegments = int.Parse(elements[1]),
                TransactionSetControlNumber = elements[2].TrimEnd('~')
            };
        }

        public string FindSESegment(string[] lines)
        {
            return Array.Find(lines, l => l.StartsWith("SE*"));
        }
    }
}
