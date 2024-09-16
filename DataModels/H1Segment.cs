namespace POC837Parser.DataModels
{

    public class HISegment
    {
        public string Qualifier { get; set; }
        public string Code { get; set; }
        public string PresentOnAdmissionIndicator { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? EndDate { get; set; } // For date ranges

        public string GetQualifierDescription()
        {
            return Qualifier switch
            {
                "ABK" => "Principal Diagnosis",
                "ABF" => "Other Diagnosis",
                "APR" => "DRG (Diagnosis Related Group)",
                "BF" => "Diagnosis Code",
                "BJ" => "Admitting Diagnosis",
                "BN" => "External Cause of Injury Code",
                "BO" => "Condition Code",
                "BP" => "Occurrence Code",
                "BR" => "Occurrence Span Code",
                "BS" => "Patient Reason for Visit",
                "BT" => "Principal Procedure Code",
                "BU" => "HCPCS/CPT-4 Procedure",
                "CAC" => "External Cause of Injury",
                "DR" => "Diagnosis Related Group (DRG)",
                "PR" => "Procedure Code",
                "TC" => "ICD-10 Diagnosis",
                _ => "Unknown Qualifier"
            };
        }

        public override string ToString()
        {
            string result = $"{Qualifier}:{Code}";
            if (!string.IsNullOrEmpty(PresentOnAdmissionIndicator))
                result += $":{PresentOnAdmissionIndicator}";
            if (Date.HasValue)
                result += $":D8:{Date.Value:yyyyMMdd}";
            if (EndDate.HasValue)
                result += $"-{EndDate.Value:yyyyMMdd}";
            return result;
        }
    }
 
}
