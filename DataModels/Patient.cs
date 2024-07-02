using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class Patient
    {
        public HierarchicalLevel HL { get; set; }
        public PatientName PatientName { get; set; } // 2010CA
        public PatientAddress PatientAddress { get; set; } // 2010CA
        public PatientDemographicInfo PatientDemographicInfo { get; set; } // 2010CA
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }

    public class PatientName
    {
        public string EntityIdentifierCode { get; set; }
        public string EntityTypeQualifier { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientNameSuffix { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string PatientIdentifier { get; set; }
    }

    public class PatientAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class PatientDemographicInfo
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
