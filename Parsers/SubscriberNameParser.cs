using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class SubscriberNameParser
    {
        public SubscriberName Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*IL*"))
            {
                throw new ArgumentException("Invalid NM1 segment for Subscriber Name");
            }

            string[] elements = line.Split('*');

            return new SubscriberName
            {
                EntityIdentifierCode = elements[1],
                EntityTypeQualifier = elements[2],
                SubscriberLastName = elements[3],
                SubscriberFirstName = elements.Length > 4 ? elements[4] : null,
                SubscriberMiddleName = elements.Length > 5 ? elements[5] : null,
                SubscriberNameSuffix = elements.Length > 7 ? elements[7] : null,
                IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
                SubscriberIdentifier = elements.Length > 9 ? elements[9].TrimEnd('~') : null
            };
        }
    }

    public class SubscriberAddressParser
    {
        public SubscriberAddress Parse(string[] lines)
        {
            string n3Line = Array.Find(lines, l => l.StartsWith("N3*"));
            string n4Line = Array.Find(lines, l => l.StartsWith("N4*"));

            if (n3Line == null || n4Line == null)
            {
                throw new ArgumentException("Missing N3 or N4 segment for Subscriber Address");
            }

            string[] n3Elements = n3Line.Split('*');
            string[] n4Elements = n4Line.Split('*');

            return new SubscriberAddress
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

    public class SubscriberDemographicInfoParser
    {
        public SubscriberDemographicInfo Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DMG*"))
            {
                throw new ArgumentException("Invalid DMG segment for Subscriber Demographic Info");
            }

            string[] elements = line.Split('*');

            return new SubscriberDemographicInfo
            {
                DateOfBirth = DateTime.ParseExact(elements[2], "yyyyMMdd", CultureInfo.InvariantCulture),
                Gender = elements[3].TrimEnd('~')
            };
        }
    }

    public class PayerNameParser
    {
        public PayerName Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*PR*"))
            {
                throw new ArgumentException("Invalid NM1 segment for Payer Name");
            }

            string[] elements = line.Split('*');

            return new PayerName
            {
                EntityIdentifierCode = elements[1],
                EntityTypeQualifier = elements[2],
                PayerOrganizationName = elements[3],
                IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
                PayerIdentifier = elements.Length > 9 ? elements[9].TrimEnd('~') : null
            };
        }
    }
}
