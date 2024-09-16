using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    //public class PayToAddressNameParser
    //{
    //    public PayToAddressName Parse(string line)
    //    {
    //        if (string.IsNullOrEmpty(line) || !line.StartsWith("NM1*87*"))
    //        {
    //            throw new ArgumentException("Invalid NM1*87 segment for Pay-to Address Name");
    //        }

    //        string[] elements = line.Split('*');

    //        return new PayToAddressName
    //        {
    //            EntityIdentifierCode = elements[1],
    //            EntityTypeQualifier = elements[2],
    //            OrganizationName = elements.Length > 3 ? elements[3] : null,
    //            NameLastName = elements.Length > 3 ? elements[3] : null, // For person names, this would be the last name
    //            NameFirstName = elements.Length > 4 ? elements[4] : null,
    //            NameMiddleName = elements.Length > 5 ? elements[5] : null,
    //            NamePrefix = elements.Length > 6 ? elements[6] : null,
    //            NameSuffix = elements.Length > 7 ? elements[7] : null,
    //            IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
    //            IdentificationCode = elements.Length > 9 ? elements[9].TrimEnd('~') : null
    //        };
    //    }
    //}
}
