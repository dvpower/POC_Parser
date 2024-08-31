public static class FacilityTypeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "1", "Hospital" },
        { "2", "Skilled Nursing Facility" },
        { "3", "Home Health Agency" },
        { "4", "Religious Non-Medical Health Care Institution" },
        { "5", "Reserved for National Assignment" },
        { "6", "Intermediate Care Facility" },
        { "7", "Clinic or Hospital-Based Renal Dialysis Facility" },
        { "8", "Special Facility or Hospital ASC Surgery" },
        { "9", "Reserved for National Assignment" }
    };

    public static string GetDescription(string facilityTypeCode)
    {
        return Descriptions.TryGetValue(facilityTypeCode, out var description)
            ? description
            : "Unknown Facility Type";
    }
}