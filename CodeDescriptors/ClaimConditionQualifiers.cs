public static class ClaimConditionQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "07", "Ambulance Certification" },
        { "E1", "Employment Status" },
        { "E2", "Condition Indicator/Disability" },
        { "E3", "Accident Related" },
        { "E4", "Pregnancy Indicator" },
        { "E5", "Patient Condition" },
        { "E6", "Homebound Indicator" },
        { "70", "Condition Indicator" },
        { "AM", "Durable Medical Equipment (DME)" },
        { "09", "Special Program Indicator" },
        { "ZZ", "Generic or User-Defined" },

    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : $"Unknown Condition Code {qualifierCode}";
    }

    public static bool IsValid(string transactionTypeCode)
    {
        return Descriptions.ContainsKey(transactionTypeCode);
    }
}