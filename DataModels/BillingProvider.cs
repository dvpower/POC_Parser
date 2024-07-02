using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class BillingProvider
    {
        public HierarchicalLevel HL { get; set; }
        public ProviderName ProviderName { get; set; }  // 2010AA
        public ProviderAddress ProviderAddress { get; set; }  // 2010AA
        public ProviderContactInformation ProviderContactInformation { get; set; }  // 2010AA
        public PayToAddress PayToAddress { get; set; }  // 2010AB
        public PayToPlanName PayToPlanName { get; set; }  // 2010AC
        public List<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
        public List<ReferenceIdentificationObj> SecondaryIdentifications { get; set; } = new List<ReferenceIdentificationObj>();

        public PayToAddressName PayToAddressName { get; set; }

        public string GetTaxonomyCode()
        {
            return SecondaryIdentifications.FirstOrDefault(si => si.ReferenceIdentificationQualifier == "ZZ")?.ReferenceIdentification;
        }

        public string GetTaxIdentificationNumber()
        {
            return SecondaryIdentifications.FirstOrDefault(si => si.ReferenceIdentificationQualifier == "EI")?.ReferenceIdentification;
        }

    }

    public class ProviderName
    {
        public string EntityIdentifierCode { get; set; }
        public string EntityTypeQualifier { get; set; }
        public string ProviderLastOrOrganizationName { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderMiddleName { get; set; }
        public string ProviderNamePrefix { get; set; }
        public string ProviderNameSuffix { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string ProviderIdentifier { get; set; }
    }

    public class ProviderAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class ProviderContactInformation
    {
        public string ContactFunctionCode { get; set; }
        public string ContactName { get; set; }
        public string CommunicationNumberQualifier { get; set; }
        public string CommunicationNumber { get; set; }
    }

    public class PayToAddress
    {
        public string EntityIdentifierCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class PayToPlanName
    {
        public string EntityIdentifierCode { get; set; }
        public string PlanName { get; set; }
    }
}
