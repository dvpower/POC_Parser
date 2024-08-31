namespace POC837Parser.Parsers
{
    public class SubscriberInformationParser
    {
        public SubscriberInformation Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("SBR*"))
            {
                throw new ArgumentException("Invalid SBR segment for Subscriber Information");
            }

            string[] elements = line.Split('*');
            return new SubscriberInformation
            {
                PayerResponsibilitySequenceNumberCode = elements[1],
                IndividualRelationshipCode = elements.Length > 2 ? elements[2] : null,
                ReferenceIdentification = elements.Length > 3 ? elements[3] : null,
                Name = elements.Length > 4 ? elements[4] : null,
                InsuranceTypeCode = elements.Length > 5 ? elements[5] : null,
                CoordinationOfBenefitsCode = elements.Length > 6 ? elements[6] : null,
                YesNoConditionOrResponseCode = elements.Length > 7 ? elements[7] : null,
                EmploymentStatusCode = elements.Length > 8 ? elements[8] : null,
                ClaimFilingIndicatorCode = elements.Length > 9 ? elements[9].TrimEnd('~') : null
            };
        }
    }

    public class SubscriberInformation
    {
        public string PayerResponsibilitySequenceNumberCode { get; set; }
        public string IndividualRelationshipCode { get; set; }
        public string ReferenceIdentification { get; set; }
        public string Name { get; set; }
        public string InsuranceTypeCode { get; set; }
        public string CoordinationOfBenefitsCode { get; set; }
        public string YesNoConditionOrResponseCode { get; set; }
        public string EmploymentStatusCode { get; set; }
        public string ClaimFilingIndicatorCode { get; set; }

        public string PayerResponsibilityDescription => GetPayerResponsibilityDescription(PayerResponsibilitySequenceNumberCode);
        public string ClaimFilingIndicatorDescription => GetClaimFilingIndicatorDescription(ClaimFilingIndicatorCode);

        public string IndividualRelationshipDescription => GetIndividualRelationshipDescription(IndividualRelationshipCode);


        private string GetPayerResponsibilityDescription(string code)
        {
            return code switch
            {
                "P" => "Primary",
                "S" => "Secondary",
                "T" => "Tertiary",
                _ => "Unknown"
            };
        }

        private string GetIndividualRelationshipDescription(string code)
        {
            return code switch
            {
                "01" => "Spouse",
                "18" => "Self",
                "19" => "Child",
                "20" => "Employee",
                "21" => "Unknown",
                "39" => "Organ Donor",
                "40" => "Cadaver Donor",
                "53" => "Life Partner",
                "G8" => "Other Relationship",
                _ => "Unknown Relationship"
            };
        }

        private string GetClaimFilingIndicatorDescription(string code)
        {
            return code switch
            {
                "MB" => "Medicare Part B",
                "MA" => "Medicare Part A",
                "MC" => "Medicaid",
                "CI" => "Commercial Insurance",
                "HM" => "Health Maintenance Organization (HMO)",
                "OF" => "Other Federal Program",
                _ => "Unknown"
            };
        }
    }
}
