public static class EntityIdentifierCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "28", "Carrier" },
        { "31", "Postal Mailing Address" },
        { "36", "Employer" },
        { "40", "Receiver" },
        { "41", "Submitter" },
        { "45", "Drop-off Location" },
        { "51", "Legal Representative" },
        { "55", "Prior Arrangement" },
        { "56", "Contract Administrator" },
        { "57", "Other Physician" },
        { "58", "Insured or Subscriber" },
        { "61", "Prior Arrangement" },
        { "71", "Attending Provider" },
        { "72", "Operating Provider" },
        { "73", "Other Provider" },
        { "74", "Corrected Priority Payer" },
        { "77", "Service Location" },
        { "80", "Hospital" },
        { "82", "Rendering Provider" },
        { "85", "Billing Provider" },
        { "87", "Pay-to Provider" },
        { "1P", "Provider" },
        { "2B", "Third-Party Administrator" },
        { "3D", "Dependent" },
        { "AY", "Ambulance Pick-up Location" },
        { "DN", "Referring Provider" },
        { "DQ", "Supervising Provider" },
        { "FA", "Facility" },
        { "GM", "Guardian" },
        { "GW", "Group" },
        { "GB", "Other Insured" },
        { "IL", "Insured or Subscriber" },
        { "LI", "Liability Insurer" },
        { "LR", "Legal Representative" },
        { "P3", "Primary Care Provider" },
        { "P5", "Plan Sponsor" },
        { "PE", "Pay To Plan" },
        { "PR", "Payer" },
        { "PW", "Pickup Address" },
        { "QC", "Patient" },
        { "QD", "Responsible Party" },
        { "TT", "Transfer To" },
        { "TU", "Third Party Repricing Organization (TPO)" },
        { "X3", "Utilization Management Organization" },
        { "Y2", "Managed Care Organization" },
        { "1I", "Interested Party" },
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