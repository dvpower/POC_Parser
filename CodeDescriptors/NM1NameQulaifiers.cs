//public static class NM1NameEntityQualifiers
//{
//    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
//    {
//        { "41", "Submitter" },
//        { "40", "Receiver" },
//        { "85", "Billing Provider" },
//        { "IL", "Insured or Subscriber" },
//        { "PR", "Payer" },
//        { "QC", "Patient" },
//        { "71", "Attending Provider" },
//        { "72", "Operating Provider" },
//        { "73", "Other Provider" },
//        { "82", "Rendering Provider" },
//        { "77", "Service Facility Location" },
//        { "DN", "Referring Provider" },
//        { "PE", "Pay-to Provider" },
//        { "87", "Pay-to Plan Name" },
//    };

//    public static string GetDescription(string entityIdentifierCode)
//    {
//        return Descriptions.TryGetValue(entityIdentifierCode, out var description)
//            ? description
//            : "Unknown Entity Identifier";
//    }
//}