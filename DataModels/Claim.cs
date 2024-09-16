using Microsoft.AspNetCore.Http.HttpResults;
using POC837Parser.DataModels;
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

        public Claim()
        {
            this.ClaimId = Guid.NewGuid();
        }

        public Guid ClaimId { get; set; }
        public ClaimInformation ClaimInfo { get; set; }
        public ClaimLineItem ClaimLine { get; set; }

        public ClaimContractInfo ContractInfo { get; set; }

        public List<Amount> Amounts { get; set; } = new List<Amount>();
        public List<HISegment> DiagnosisCodes { get; set; } = new List<HISegment>();
        public List<ServiceLine> ServiceLines { get; set; } = new List<ServiceLine>();
        public List<NM1Name> Names { get; set; } = new List<NM1Name>();

        public List<ReferenceIdentificationObj> AdditionalReferenceInformation { get; set; } = new List<ReferenceIdentificationObj>();
        public List<DateOrTimeElement> ClaimDates { get; set; } = new List<DateOrTimeElement>();
        public List<NoteElement> ClaimNotes { get; set; } = new List<NoteElement>();

        public List<ClaimConditionInfo> ClaimConditions { get; set; } = new List<ClaimConditionInfo>();
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

    public class ClaimContractInfo
    {
        /// <summary>
        /// Contract Type Code (CN101)
        /// </summary>
        public string ContractTypeCode { get; set; }
        public string ContractTypeDescription { get; set; }

        /// <summary>
        /// Contract Amount (CN102)
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// Contract Percentage (CN103)
        /// </summary>
        public decimal? ContractPercentage { get; set; }

        /// <summary>
        /// Contract Code (CN104)
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// Terms Discount Percentage (CN105)
        /// </summary>
        public decimal? TermsDiscountPercentage { get; set; }

        /// <summary>
        /// Contract Version Identifier (CN106)
        /// </summary>
        public string ContractVersionIdentifier { get; set; }
    }

    public class ClaimConditionInfo
    {
        /// <summary>
        /// Code Category (CRC01) - The type of condition being reported.
        /// </summary>
        public string CodeCategory { get; set; }

        public string CodeCategoryDescription { get; set; }

        /// <summary>
        /// Yes/No Condition Indicator (CRC02) - Specifies if the condition applies (Y = Yes, N = No).
        /// </summary>
        public string ConditionIndicator { get; set; }

        /// <summary>
        /// Condition Code 1 (CRC03) - Additional condition code information.
        /// </summary>
        public string ConditionCode1 { get; set; }

        /// <summary>
        /// Condition Code 2 (CRC04) - Optional condition code for further detail.
        /// </summary>
        public string ConditionCode2 { get; set; }

        /// <summary>
        /// Condition Code 3 (CRC05) - Optional condition code for further detail.
        /// </summary>
        public string ConditionCode3 { get; set; }

        /// <summary>
        /// Condition Code 4 (CRC06) - Optional condition code for further detail.
        /// </summary>
        public string ConditionCode4 { get; set; }

        /// <summary>
        /// Condition Code 5 (CRC07) - Optional condition code for further detail.
        /// </summary>
        public string ConditionCode5 { get; set; }
    }

    public class ClaimLineItem
    {
        public string LineItemControlNumber { get; set; }
        public int Quantity { get; set; }
        public string UnitOrBasisForMeasureCode { get; set; }
        public decimal ChargeAmount { get; set; }
        public int Units { get; set; }
        public DateTime? DateOfService { get; set; }
        public string PlaceOfServiceCode { get; set; }
        public string TypeOfServiceCode { get; set; }
        public bool EpsdtIndicator { get; set; }
        public bool FamilyPlanningIndicator { get; set; }
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


    public class Amount
    {
        public string AmountQualifierCode { get; set; }
        public decimal MonetaryAmount { get; set; }
        public string CreditDebitFlagCode { get; set; }     
        public string Description { get; set; }
    }

    public class ClaimAmount
    {
        public decimal? PatientAmountPaid { get; set; }
        public decimal? TotalPurchasedServiceAmount { get; set; }
    }
}
