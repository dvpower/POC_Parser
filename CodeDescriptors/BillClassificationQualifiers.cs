public static class BillClassificationQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "1", "Inpatient (including Medicare Part A)" },
        { "2", "Inpatient (Medicare Part B only)" },
        { "3", "Outpatient" },
        { "4", "Other (for hospital-referenced diagnostic services or home health not under a plan of treatment)" },
        { "5", "Intermediate Care - Level I" },
        { "6", "Intermediate Care - Level II" },
        { "7", "Intermediate Care - Level III" },
        { "8", "Swing Beds" },
        { "9", "Reserved for National Assignment" }
    };

    public static string GetDescription(string classificationCode)
    {
        return Descriptions.TryGetValue(classificationCode, out var description)
            ? description
            : "Unknown Classification";
    }
}