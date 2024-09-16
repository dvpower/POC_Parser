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

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new ReferenceIdentificationObj
            { 
                ReferenceIdentificationQualifier = elements[1],
                ReferenceIdentificationQualifierDescription =  ReferenceIdentificationQualifiers.GetDescription(elements[1]),
                ReferenceIdentification= elements[2],
                Description = elements.Length > 3 ? elements[3] : null,
                SecondaryReferenceIdentification = elements.Length > 4 ? elements[4] : null // todo : this is a multipart 
            };
        }
    }
}
