using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class Subscriber
    {
        public HierarchicalLevel HL { get; set; }
        public SubscriberName SubscriberName { get; set; } // 2010BA
        public SubscriberAddress SubscriberAddress { get; set; } // 2010BA
        public SubscriberDemographicInfo SubscriberDemographicInfo { get; set; } // 2010BA
        public PayerName PayerName { get; set; } // 2010BB
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }

    public class SubscriberName
    {
        public string EntityIdentifierCode { get; set; }
        public string EntityTypeQualifier { get; set; }
        public string SubscriberLastName { get; set; }
        public string SubscriberFirstName { get; set; }
        public string SubscriberMiddleName { get; set; }
        public string SubscriberNameSuffix { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string SubscriberIdentifier { get; set; }
    }

    public class SubscriberAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class SubscriberDemographicInfo
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class PayerName
    {
        public string EntityIdentifierCode { get; set; }
        public string EntityTypeQualifier { get; set; }
        public string PayerOrganizationName { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string PayerIdentifier { get; set; }
    }
}
