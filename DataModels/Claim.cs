using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string ProviderSignatureOnFile { get; set; }
        public string AssignmentOfBenefitsIndicator { get; set; }
        public string BenefitsAssignmentCertificationIndicator { get; set; }
        public string ReleaseOfInformationCode { get; set; }
        public DateTime ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
    }

    public class ClaimAmount
    {
        public decimal? PatientAmountPaid { get; set; }
        public decimal? TotalPurchasedServiceAmount { get; set; }
    }

    public class DiagnosisCode
    {
        public string Qualifier { get; set; }
        public string Code { get; set; }
    }
}
