    public static class DiagnosisCodeQualifiers
    {
        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "BK", "Principal Diagnosis" },
        { "BF", "Other Diagnosis" },
        { "ABK", "Principal Diagnosis (ICD-10)" },
        { "ABF", "Other Diagnosis (ICD-10)" },
        { "APR", "Patient's Reason for Visit" },
        { "DR", "Admitting Diagnosis" },
        { "PR", "Principal Procedure Information" },
        { "BBR", "Related Procedure Information" },
        { "BBQ", "Related Procedure Information" },
        { "BR", "External Cause of Injury" },
        { "BH", "Occurrence Span" },
        { "BG", "Condition" },
        { "BI", "Occurrence" },
        { "BN", "Value" }
    };

        public static string GetDescription(string qualifierCode)
        {
            return Descriptions.TryGetValue(qualifierCode, out var description)
                ? description
                : "Unknown Qualifier";
        }
    }
