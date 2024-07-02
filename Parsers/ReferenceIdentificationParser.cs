using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class ReferenceIdentificationParser
    {
        public ReferenceIdentificationObj Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("REF*"))
            {
                throw new ArgumentException("Invalid REF segment");
            }

            string[] elements = line.Split('*');

            return new ReferenceIdentificationObj
            {
                ReferenceIdentificationQualifier = elements[1],
                ReferenceIdentification = elements[2],
                Description = elements.Length > 3 ? elements[3].TrimEnd('~') : null
            };
        }
    }
}
