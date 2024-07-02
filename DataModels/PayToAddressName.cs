using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class PayToAddressName
    {
        public string EntityIdentifierCode { get; set; } // Should be "87" for Pay-to Address Name
        public string EntityTypeQualifier { get; set; }
        public string OrganizationName { get; set; }
        public string NamePrefix { get; set; }
        public string NameFirstName { get; set; }
        public string NameMiddleName { get; set; }
        public string NameLastName { get; set; }
        public string NameSuffix { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string IdentificationCode { get; set; }
    }
}
