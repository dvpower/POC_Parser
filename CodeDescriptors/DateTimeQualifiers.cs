public static class DateTimeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "001", "Service Date" },
        { "002", "Birth Date" },
        { "003", "Invoice Date" },
        { "036", "Expiration Date" },
        { "050", "Received Date" },
        { "090", "Report Start Date" },
        { "091", "Report End Date" },
        { "102", "Issue Date" },
        { "150", "Service Period Start" },
        { "151", "Service Period End" },
        { "198", "Admission Date" },
        { "232", "Discharge Date" },
        { "233", "First Contact Date" },
        { "272", "Date of Death" },
        { "304", "Latest Visit Date" },
        { "307", "Eligibility Begin Date" },
        { "318", "Added Date" },
        { "341", "Date of Payment" },
        { "435", "Creation Date" },
        { "441", "Prior Authorization Date" },
        { "454", "Effective Date" },
        { "472", "Service Date" },
        { "607", "Release Date" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : $"Unknown Date Time Qualifier {qualifierCode}";
    }

    public static bool IsValid(string transactionTypeCode)
    {
        return Descriptions.ContainsKey(transactionTypeCode);
    }
}