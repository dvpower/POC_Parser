using POC837Parser.DataModels;
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

        public List<NM1Name> BillingProviderNames { get; set; } = new List<NM1Name>();
        public ProviderContactInformation ProviderContactInformation { get; set; }  // 2010AA
        public CurrencyInformation ProviderCurrencyInformation { get; set; }

        public List<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
        public List<ReferenceIdentificationObj> AdditionalReferenceInformation { get; set; } = new List<ReferenceIdentificationObj>();

        public List<ProviderInformation> ProviderInformation { get; set; } = new List<ProviderInformation>();
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

    public class CurrencyInformation
    {
        public string EntityIdentifierCode { get; set; } // Always "85" for Billing Provider
        public string CurrencyCode { get; set; } // e.g., "USD", "CAD"
        public decimal? ExchangeRate { get; set; } // Nullable as it's optional
    }

    public class ProviderInformation
    {
        public string ProviderCode { get; set; } // PRV01 (e.g., "BI" for Billing, "AT" for Attending)
        
        public string ProviderCodeDescription { get; set; }
        public string ReferenceIdentificationQualifier { get; set; } // PRV02 (typically "PXC" for taxonomy)
        public string ReferenceIdentificationQualifierDescription { get; set; }       
        public string ProviderTaxonomyCode { get; set; } // PRV03
    }



    //public class PayToAddress
    //{
    //    public string EntityIdentifierCode { get; set; }
    //    public string EntityIdentifierCodeDescription { get; set; }
    //    public string AddressLine1 { get; set; }
    //    public string AddressLine2 { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string ZipCode { get; set; }
    //    public string CountryCode { get; set; }
    //}

    //public class PayToPlanName
    //{
    //    public string EntityIdentifierCode { get; set; }
    //    public string EntityIdentifierCodeDescription { get; set; }
    //    public string PlanName { get; set; }
    //}
}
