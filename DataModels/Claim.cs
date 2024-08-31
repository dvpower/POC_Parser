using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace _837ParserPOC.DataModels
{
    public class Claim
    {
        public ClaimInformation ClaimInfo { get; set; }
        public ClaimAmount ClaimAmount { get; set; }
        public List<DiagnosisCode> DiagnosisCodes { get; set; } = new List<DiagnosisCode>();
        public List<ServiceLine> ServiceLines { get; set; } = new List<ServiceLine>();
    }

    public class ClaimInformation
    {
        public string ClaimId { get; set; }
        public decimal TotalClaimChargeAmount { get; set; }
        public string PatientControlNumber { get; set; }
        public string ClaimFrequencyTypeCode { get; set; }
        public ServiceLocation HealthCareServiceLocationInformation { get; set; }

        /// <summary>
        /// Indicates whether the provider agrees to accept payment from the insurance as payment in full (minus patient's responsibility) (Y/N)
        /// </summary>
        public string ProviderAcceptAssignmentCode { get; set; }
        public string ProviderAcceptAssignmentCodeDescription { get; set; }

        /// <summary>
        /// Indicates the status of the benefits assignment certification document
        /// </summary>
        public string BenefitsAssignmentCertificationIndicator { get; set; }
        public string BenefitsAssignmentCertificationIndicatorDescription { get; set; }

        /// <summary>
        /// Indicates whether the provider has permission to release medical information
        /// </summary>
        public string ReleaseOfInformationCode { get; set; }
        public string ReleaseOfInformationCodeDescription { get; set; }

        /// <summary>
        /// Indicates the source or existence of the patient's signature on file
        /// </summary>
        public string PatientSignatureSourceCode { get; set; }
        public string PatientSignatureSourceCodeDescription { get; set; }

        //        public string ProviderSignatureOnFile { get; set; }
        //       public string AssignmentOfBenefitsIndicator { get; set; }
        //        public string BenefitsAssignmentCertificationIndicator { get; set; }
        //        public string ReleaseOfInformationCode { get; set; }
        public DateTime ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
    }

    public class ServiceLocation
    {
        public string FacilityType { get; set; }
        public string FacilityTypeDescription { get; set; }
        public string BillClassification { get; set; }
        public string BillClassificationDescription { get; set; }
        public string Frequency { get; set; }
        public string FrequencyDescription { get; set; }

    }



    public class ClaimAmount
    {
        public decimal? PatientAmountPaid { get; set; }
        public decimal? TotalPurchasedServiceAmount { get; set; }
    }

    public class DiagnosisCode
    {
        public string QualifierCode { get; set; }
        public string QualifierDescription { get; set; }
        public string Code { get; set; }
    }
}
