using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class PatientNameParser
    {
        public PatientName Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*QC*"))
            {
                throw new ArgumentException("Invalid NM1 segment for Patient Name");
            }

            string[] elements = line.Split('*');

            return new PatientName
            {
                EntityIdentifierCode = elements[1],
                EntityTypeQualifier = elements[2],
                PatientLastName = elements[3],
                PatientFirstName = elements.Length > 4 ? elements[4] : null,
                PatientMiddleName = elements.Length > 5 ? elements[5] : null,
                PatientNameSuffix = elements.Length > 7 ? elements[7] : null,
                IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
                PatientIdentifier = elements.Length > 9 ? elements[9].TrimEnd('~') : null
            };
        }
    }

    public class PatientAddressParser
    {
        public PatientAddress Parse(string[] lines)
        {
            string n3Line = Array.Find(lines, l => l.StartsWith("N3*"));
            string n4Line = Array.Find(lines, l => l.StartsWith("N4*"));

            if (n3Line == null || n4Line == null)
            {
                throw new ArgumentException("Missing N3 or N4 segment for Patient Address");
            }

            string[] n3Elements = n3Line.Split('*');
            string[] n4Elements = n4Line.Split('*');

            return new PatientAddress
            {
                AddressLine1 = n3Elements[1],
                AddressLine2 = n3Elements.Length > 2 ? n3Elements[2].TrimEnd('~') : null,
                City = n4Elements[1],
                State = n4Elements[2],
                ZipCode = n4Elements[3],
                CountryCode = n4Elements.Length > 4 ? n4Elements[4].TrimEnd('~') : null
            };
        }
    }

    public class PatientDemographicInfoParser
    {
        public PatientDemographicInfo Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DMG*"))
            {
                throw new ArgumentException("Invalid DMG segment for Patient Demographic Info");
            }

            string[] elements = line.Split('*');

            return new PatientDemographicInfo
            {
                DateOfBirth = DateTime.ParseExact(elements[2], "yyyyMMdd", CultureInfo.InvariantCulture),
                Gender = elements[3].TrimEnd('~')
            };
        }
    }
}
