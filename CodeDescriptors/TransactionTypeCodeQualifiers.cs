public static class TransactionTypeCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "CH", "Chargeable (Claim Submission)" },
        { "RP", "Reporting" },
        { "HS", "Health Services Review" },
        { "PT", "Patient Information" },
        { "IN", "Information" },
        { "AR", "Adjustment Request" },
        { "RO", "Reporting Only" },
        { "AD", "Adjustment" },
        { "TH", "Third Party Information" },
        { "18", "Original" },
        { "00", "Original (Alternate)" },
        { "19", "Information Copy" },
        { "13", "Request" },
        { "11", "Response" },
        { "22", "Information Copy/Response" },
        { "01", "Cancel" },
        { "02", "Add" },
        { "03", "Delete" },
        { "04", "Change" },
        { "05", "Replace" },
        { "15", "Re-submission" }
    };

    public static string GetDescription(string transactionTypeCode)
    {
        return Descriptions.TryGetValue(transactionTypeCode, out var description)
            ? description
            : "Unknown Transaction Type";
    }

    public static bool IsValid(string transactionTypeCode)
    {
        return Descriptions.ContainsKey(transactionTypeCode);
    }
}