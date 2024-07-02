using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return new ClaimInformation
            {
                ClaimId = elements[1],
                TotalClaimChargeAmount = decimal.Parse(elements[2]),
                PatientControlNumber = elements.Length > 3 ? elements[3] : null,
                ClaimFrequencyTypeCode = elements.Length > 4 ? elements[4] : null,
                ProviderSignatureOnFile = elements.Length > 6 ? elements[6] : null,
                AssignmentOfBenefitsIndicator = elements.Length > 7 ? elements[7] : null,
                BenefitsAssignmentCertificationIndicator = elements.Length > 8 ? elements[8] : null,
                ReleaseOfInformationCode = elements.Length > 9 ? elements[9].TrimEnd('~') : null
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
                    diagnosisCodes.Add(new DiagnosisCode
                    {
                        Qualifier = codeElements[0],
                        Code = codeElements[1]
                    });
                }
            }

            return diagnosisCodes;
        }
    }
}
