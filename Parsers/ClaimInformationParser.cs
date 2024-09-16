using _837ParserPOC.DataModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using POC837Parser.DataModels;
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

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');
            
            var providerAcceptAssignmentCode = elements.Length > 6 ? elements[6] : null;
            var benefitsAssignmentCertificationIndicator = elements.Length > 7 ? elements[7] : null;
            var releaseOfInformationCode = elements.Length > 8 ? elements[8] : null;
            var patientSignatureSourceCode = elements.Length > 9 ? elements[9] : null;

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

    public class NoteParser
    {
        public NoteElement Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("NTE*"))
            {
                throw new ArgumentException("Invalid NTE segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new NoteElement
            {
               NoteReferenceCode = elements[1],
               Description = elements.Length > 2 ? elements[2] : null,
            };
        }
    }

    public class ClaimContractParser
    {
        public ClaimContractInfo Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("CN1*"))
            {
                throw new ArgumentException("Invalid CN1 segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new ClaimContractInfo
            {
                ContractTypeCode = elements[1],  // TODO : Contract type code info
                ContractTypeDescription = ContractCodeQualifiers.GetDescription(elements[1]),
                ContractAmount = elements.Length > 2 ? decimal.Parse(elements[2]) : null,
                ContractPercentage = elements.Length > 3 ? decimal.Parse(elements[3]) : null,
                ContractCode = elements.Length > 4 ? elements[4] : null,
                TermsDiscountPercentage = elements.Length > 5 ? decimal.Parse(elements[5]) : null,
                ContractVersionIdentifier = elements.Length > 6 ? elements[6] : null,
            };
        }
    }

    public class ClaimLineParser
    {
        public ClaimLineItem Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("CL1*"))
            {
                throw new ArgumentException("Invalid CL1 segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new ClaimLineItem
            {
                LineItemControlNumber = elements[1],
                Quantity = elements.Length > 2 ? int.Parse(elements[2]) : 0,
                UnitOrBasisForMeasureCode = elements.Length > 3 ? elements[3] : null,
                ChargeAmount = elements.Length > 4 ? decimal.Parse(elements[4]) : 0,
                Units = elements.Length > 5 ? int.Parse(elements[5]) : 0,
                DateOfService = elements.Length >65 ? DateTime.ParseExact(elements[6], "yyyyMMdd", CultureInfo.InvariantCulture) : null,
                PlaceOfServiceCode = elements.Length > 7 ? elements[7] : null,
                TypeOfServiceCode = elements.Length > 8 ? elements[8] : null,
                EpsdtIndicator = elements.Length > 9 ? bool.Parse(elements[9]) : false,
                FamilyPlanningIndicator = elements.Length > 10 ? bool.Parse(elements[10]) : false,
            };
        }
    }

    public class ConditionsParser
    {
        public ClaimConditionInfo Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("CRC*"))
            {
                throw new ArgumentException("Invalid CRC segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new ClaimConditionInfo
            {
                CodeCategory = elements[1],
                CodeCategoryDescription= ClaimConditionQualifiers.GetDescription(elements[1]),
                ConditionIndicator = elements.Length > 2 ? elements[2] : null,
                ConditionCode1 = elements.Length > 3 ? elements[3] : null,
                ConditionCode2 = elements.Length > 4 ? elements[4] : null,
                ConditionCode3 = elements.Length > 5 ? elements[5] : null,
                ConditionCode4 = elements.Length > 6 ? elements[6] : null,
                ConditionCode5 = elements.Length > 7 ? elements[7] : null
            };
        }
    }


    //public class ClaimAmountParser
    //{
    //    public ClaimAmount Parse(string[] lines)
    //    {
    //        var claimAmount = new ClaimAmount();

    //        foreach (var line in lines)
    //        {
    //            if (line.StartsWith("AMT*F5*"))
    //            {
    //                string[] elements = line.Split('*');
    //                claimAmount.PatientAmountPaid = decimal.Parse(elements[2].TrimEnd('~'));
    //            }
    //            else if (line.StartsWith("AMT*NE*"))
    //            {
    //                string[] elements = line.Split('*');
    //                claimAmount.TotalPurchasedServiceAmount = decimal.Parse(elements[2].TrimEnd('~'));
    //            }
    //        }

    //        return claimAmount;
    //    }
    //}

    public class HISegmentParser
    {
        public  List<HISegment> Parse(string hiSegment)
        {
            var result = new List<HISegment>();

            // Remove the "HI*" prefix and the "~" suffix
            hiSegment = hiSegment.Trim('~');
            if (hiSegment.StartsWith("HI*"))
            {
                hiSegment = hiSegment.Substring(3);
            }

            // Split the segment into individual entries
            var entries = hiSegment.Split('*');

            foreach (var entry in entries)
            {
                var parts = entry.Split(':');
                var segment = new HISegment
                {
                    Qualifier = parts[0]
                };

                if (parts.Length > 1)
                    segment.Code = parts[1];

                if (parts.Length > 2)
                {
                    if (parts[2] == "RD8" && parts.Length > 3)
                    {
                        var dates = parts[3].Split('-');
                        if (DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                        {
                            segment.Date = startDate;
                        }
                        if (dates.Length > 1 && DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                        {
                            segment.EndDate = endDate;
                        }
                    }
                    else
                    {
                        segment.PresentOnAdmissionIndicator = parts[2];
                    }
                }

                if (parts.Length > 3 && parts[2] != "RD8")
                {
                    if (parts[3] == "D8" && parts.Length > 4)
                    {
                        if (DateTime.TryParseExact(parts[4], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                        {
                            segment.Date = date;
                        }
                    }
                }

                result.Add(segment);
            }

            return result;
        }
    }



    public class DiagnosisCodeParser
    {
        public List<HISegment> Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("HI*"))
            {
                throw new ArgumentException("Invalid HI segment for Diagnosis Codes");
            }

            string[] elements = line.Split('*');
            var diagnosisCodes = new List<HISegment>();

            for (int i = 1; i < elements.Length; i++)
            {
                string[] codeElements = elements[i].Split(':');
                if (codeElements.Length >= 2)
                {
                    string qualifierCode = codeElements[0];

                    diagnosisCodes.Add(new HISegment
                    {
                        Qualifier = qualifierCode,
                        //QualifierDescription = DiagnosisCodeQualifiers.GetDescription(qualifierCode),
                        Code = codeElements[1]  // ICD-10 code
                    });
                }
            }

            return diagnosisCodes;
        }
    }  
}
