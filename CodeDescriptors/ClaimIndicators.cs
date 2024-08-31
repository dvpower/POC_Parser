public static class ClaimIndicators
{
    public static class ProviderAcceptAssignment
    {
        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "Y", "Yes, provider accepts assignment" },
            { "N", "No, provider does not accept assignment" }
        };
    }

    public static class BenefitsAssignmentCertification
    {
        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "A", "Certification on file" },
            { "B", "Certification not on file" },
            { "C", "Not assigned" },
            { "P", "Assignment refused" }
        };
    }

    public static class ReleaseOfInformation
    {
        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "Y", "Yes, provider has a signed statement permitting release of medical billing data" },
            { "N", "No, provider does not have a signed statement permitting release of medical billing data" },
            { "I", "Informed consent to release medical information for conditions or diagnoses regulated by federal statutes" }
        };
    }

    public static class PatientSignatureSource
    {
        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "Y", "Signed statement on file" },
            { "N", "No signed statement on file" },
            { "M", "Signed statement on file, faxed" },
            { "O", "Signed statement on file, electronic" }
        };
    }

    public static string GetDescription(Dictionary<string, string> dict, string code)
    {
        return dict.TryGetValue(code, out var description) ? description : "Unknown Code";
    }
}