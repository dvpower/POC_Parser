public static class EntityIdentifierCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "IL", "Insured or Subscriber" },
        { "PR", "Payer" },
        { "1P", "Provider" },
        { "41", "Submitter" },
        { "40", "Receiver" },
        { "85", "Billing Provider" },
        { "87", "Pay-to Provider" },
        { "QC", "Patient" },
        { "71", "Attending Provider" },
        { "72", "Operating Provider" },
        { "73", "Other Provider" },
        { "82", "Rendering Provider" },
        { "FA", "Facility" },
        { "31", "Postal Mailing Address" },
        { "45", "Drop-off Location" },
        { "36", "Employer" },
        { "GW", "Group" },
        { "Y2", "Managed Care Organization" },
        { "GB", "Other Insured" },
        { "LR", "Legal Representative" },
        { "TT", "Transfer To" },
        { "DQ", "Supervising Provider" },
        { "PW", "Pickup Address" }
    };

    public static string GetDescription(string entityIdentifierCode)
    {
        return Descriptions.TryGetValue(entityIdentifierCode, out var description)
            ? description
            : "Unknown Entity Identifier";
    }

    public static bool IsValid(string entityIdentifierCode)
    {
        return Descriptions.ContainsKey(entityIdentifierCode);
    }
}