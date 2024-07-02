﻿using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class ProviderNameParser
    {
        public ProviderName Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*"))
            {
                throw new ArgumentException("Invalid NM1 segment for Provider Name");
            }

            string[] elements = line.Split('*');

            return new ProviderName
            {
                EntityIdentifierCode = elements[1],
                EntityTypeQualifier = elements[2],
                ProviderLastOrOrganizationName = elements[3],
                ProviderFirstName = elements.Length > 4 ? elements[4] : null,
                ProviderMiddleName = elements.Length > 5 ? elements[5] : null,
                ProviderNamePrefix = elements.Length > 6 ? elements[6] : null,
                ProviderNameSuffix = elements.Length > 7 ? elements[7] : null,
                IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
                ProviderIdentifier = elements.Length > 9 ? elements[9].TrimEnd('~') : null
            };
        }
    }

    public class ProviderAddressParser
    {
        public ProviderAddress Parse(string[] lines)
        {
            string n3Line = Array.Find(lines, l => l.StartsWith("N3*"));
            string n4Line = Array.Find(lines, l => l.StartsWith("N4*"));

            if (n3Line == null || n4Line == null)
            {
                throw new ArgumentException("Missing N3 or N4 segment for Provider Address");
            }

            string[] n3Elements = n3Line.Split('*');
            string[] n4Elements = n4Line.Split('*');

            return new ProviderAddress
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

    public class ProviderContactInformationParser
    {
        public ProviderContactInformation Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("PER*"))
            {
                throw new ArgumentException("Invalid PER segment for Provider Contact Information");
            }

            string[] elements = line.Split('*');

            return new ProviderContactInformation
            {
                ContactFunctionCode = elements[1],
                ContactName = elements.Length > 2 ? elements[2] : null,
                CommunicationNumberQualifier = elements.Length > 3 ? elements[3] : null,
                CommunicationNumber = elements.Length > 4 ? elements[4].TrimEnd('~') : null
            };
        }
    }

    public class PayToAddressParser
    {
        public PayToAddress Parse(string[] lines)
        {
            string n3Line = Array.Find(lines, l => l.StartsWith("N3*"));
            string n4Line = Array.Find(lines, l => l.StartsWith("N4*"));

            if (n3Line == null || n4Line == null)
            {
                throw new ArgumentException("Missing N3 or N4 segment for Pay-To Address");
            }

            string[] n3Elements = n3Line.Split('*');
            string[] n4Elements = n4Line.Split('*');

            return new PayToAddress
            {
                EntityIdentifierCode = "87",  // Hardcoded as per 837 specification
                AddressLine1 = n3Elements[1],
                AddressLine2 = n3Elements.Length > 2 ? n3Elements[2].TrimEnd('~') : null,
                City = n4Elements[1],
                State = n4Elements[2],
                ZipCode = n4Elements[3],
                CountryCode = n4Elements.Length > 4 ? n4Elements[4].TrimEnd('~') : null
            };
        }
    }

    public class PayToPlanNameParser
    {
        public PayToPlanName Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*"))
            {
                throw new ArgumentException("Invalid NM1 segment for Pay-To Plan Name");
            }

            string[] elements = line.Split('*');

            return new PayToPlanName
            {
                EntityIdentifierCode = elements[1],
                PlanName = elements[3].TrimEnd('~')
            };
        }
    }
}
