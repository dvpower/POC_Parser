using _837ParserPOC.DataModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using static ClaimIndicators;

namespace _837ParserPOC.Parsers
{
    public class ClaimInformationParser
    {
        public ClaimInformation Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("CLM*"))
            {
                throw new ArgumentException("Invalid CLM segment for Claim Information");
            }

            string[] elements = line.Split('*');

            var providerAcceptAssignmentCode = elements.Length > 6 ? elements[6] : null;
            var benefitsAssignmentCertificationIndicator = elements.Length > 7 ? elements[7] : null;
            var releaseOfInformationCode = elements.Length > 8 ? elements[8] : null;
            var patientSignatureSourceCode = elements.Length > 9 ? elements[9].TrimEnd('~') : null;

            return new ClaimInformation
            {
                ClaimId = elements[1],
                TotalClaimChargeAmount = decimal.Parse(elements[2]),
                PatientControlNumber = elements.Length > 3 ? elements[3] : null,
                ClaimFrequencyTypeCode = elements.Length > 4 ? elements[4] : null, 
                HealthCareServiceLocationInformation = ParseServiceLocation(elements.Length > 5 ? elements[5] : null),
                ProviderAcceptAssignmentCode = providerAcceptAssignmentCode,  // Provider Accept Assignment Code
                ProviderAcceptAssignmentCodeDescription = ClaimIndicators.GetDescription(ClaimIndicators.ProviderAcceptAssignment.Descriptions, providerAcceptAssignmentCode),  
                BenefitsAssignmentCertificationIndicator = benefitsAssignmentCertificationIndicator,  // Benefits Assignment Certification Indicator
                BenefitsAssignmentCertificationIndicatorDescription = ClaimIndicators.GetDescription(ClaimIndicators.BenefitsAssignmentCertification.Descriptions, benefitsAssignmentCertificationIndicator),  
                ReleaseOfInformationCode = releaseOfInformationCode,  // Release of Information Code
                ReleaseOfInformationCodeDescription = ClaimIndicators.GetDescription(ClaimIndicators.ReleaseOfInformation.Descriptions, releaseOfInformationCode),  
                PatientSignatureSourceCode = patientSignatureSourceCode,  // Patient Signature Source Code
                PatientSignatureSourceCodeDescription = ClaimIndicators.GetDescription(ClaimIndicators.PatientSignatureSource.Descriptions, patientSignatureSourceCode),  
            };
        }

        private ServiceLocation ParseServiceLocation(string locationInfo)
        {
            if (string.IsNullOrEmpty(locationInfo))
                return null;

            var parts = locationInfo.Split(':');
            if (parts.Length != 3)
                return null;


            return new ServiceLocation
            {
                FacilityType = parts[0][0].ToString(),
                FacilityTypeDescription = FacilityTypeQualifiers.GetDescription(parts[0][0].ToString()),
                BillClassification = parts[0][1].ToString(),
                BillClassificationDescription = BillClassificationQualifiers.GetDescription(parts[0][1].ToString()),
                Frequency = parts[2][0].ToString(),
                FrequencyDescription = ClaimFrequencyQualifiers.GetDescription(parts[2][0].ToString())
            };
        }

    }

    public class ClaimDateParser
    {
        public void ParseServiceDates(string line, ClaimInformation claimInfo)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DTP*434*"))
            {
                throw new ArgumentException("Invalid DTP segment for Claim Service Dates");
            }

            string[] elements = line.Split('*');
            string[] dates = elements[3].TrimEnd('~').Split('-');

            claimInfo.ServiceDateFrom = DateTime.ParseExact(dates[0], "yyyyMMdd", CultureInfo.InvariantCulture);
            if (dates.Length > 1)
            {
                claimInfo.ServiceDateTo = DateTime.ParseExact(dates[1], "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }
    }

    public class ClaimAmountParser
    {
        public ClaimAmount Parse(string[] lines)
        {
            var claimAmount = new ClaimAmount();

            foreach (var line in lines)
            {
                if (line.StartsWith("AMT*F5*"))
                {
                    string[] elements = line.Split('*');
                    claimAmount.PatientAmountPaid = decimal.Parse(elements[2].TrimEnd('~'));
                }
                else if (line.StartsWith("AMT*NE*"))
                {
                    string[] elements = line.Split('*');
                    claimAmount.TotalPurchasedServiceAmount = decimal.Parse(elements[2].TrimEnd('~'));
                }
            }

            return claimAmount;
        }
    }

    public class DiagnosisCodeParser
    {
        public List<DiagnosisCode> Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("HI*"))
            {
                throw new ArgumentException("Invalid HI segment for Diagnosis Codes");
            }

            string[] elements = line.Split('*');
            var diagnosisCodes = new List<DiagnosisCode>();

            for (int i = 1; i < elements.Length; i++)
            {
                string[] codeElements = elements[i].Split(':');
                if (codeElements.Length >= 2)
                {
                    string qualifierCode = codeElements[0];

                    diagnosisCodes.Add(new DiagnosisCode
                    {
                        QualifierCode = qualifierCode,
                        QualifierDescription = DiagnosisCodeQualifiers.GetDescription(qualifierCode),
                        Code = codeElements[1]  // ICD-10 code
                    });
                }
            }

            return diagnosisCodes;
        }
    }  
}
