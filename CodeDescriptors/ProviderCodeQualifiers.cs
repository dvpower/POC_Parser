public static class ProviderCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "AD", "Admitting" },
        { "AT", "Attending" },
        { "BI", "Billing" },
        { "CO", "Consulting" },
        { "CV", "Covering" },
        { "H", "Hospital" },
        { "HH", "Home Health Care" },
        { "LA", "Laboratory" },
        { "OT", "Other Physician" },
        { "PC", "Primary Care Physician" },
        { "PE", "Performing" },
        { "PT", "Pay-to" },
        { "R", "Referring" },
        { "RB", "Responsible Billing" },
        { "RP", "Rendering Provider" },
        { "SB", "Submitting" },
        { "SK", "Skilled Nursing Facility" },
        { "SU", "Supervising" },
        { "TQ", "Ordering" },
        { "TC", "Taking Charge" },
        { "OP", "Operating" },
        { "RF", "Referring" },
        { "NP", "Non-physician" },
        { "FA", "Facility" },
        { "SL", "Service Location" },
        { "DN", "Referring Provider" },
        { "DK", "Ordering Provider" },
        { "DQ", "Supervising Provider" },
        { "82", "Rendering Provider" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : $"Unknown Provider Code Qualifier {qualifierCode}";
    }

    public static bool IsValid(string qualifierCode)
    {
        return Descriptions.ContainsKey(qualifierCode);
    }
}