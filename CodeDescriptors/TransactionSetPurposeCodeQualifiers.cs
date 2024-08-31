public static class TransactionSetPurposeCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "00", "Original" },
        { "15", "Re-submission" },
        { "18", "Reissue" },
        { "19", "Information Copy" },
        { "22", "Information Copy/Response" },
        { "01", "Cancel" },
        { "02", "Add" },
        { "03", "Delete" },
        { "04", "Change" },
        { "05", "Replace" },
        { "06", "Confirmation" },
        { "07", "Duplicate" },
        { "08", "Status" },
        { "11", "Response" },
        { "13", "Request" },
        { "14", "Advance Notification" },
        { "16", "Proposed" },
        { "17", "Cancel, to be Reissued" },
        { "20", "Final Transmission" },
        { "21", "Transaction on Hold" },
        { "23", "Not Found" },
        { "24", "Refused" },
        { "25", "Not Accepted" },
        { "27", "Verify" },
        { "28", "Query" },
        { "30", "Continuation" },
        { "31", "Resequenced Continuation" },
        { "33", "Create" },
        { "34", "Update" },
        { "35", "Cancel Request" }
    };

    public static string GetDescription(string purposeCode)
    {
        return Descriptions.TryGetValue(purposeCode, out var description)
            ? description
            : "Unknown Transaction Set Purpose";
    }

    public static bool IsValid(string purposeCode)
    {
        return Descriptions.ContainsKey(purposeCode);
    }

    public static string NormalizeCode(string purposeCode)
    {
        // If the code is 4 digits and starts with '00', return the last two digits
        if (purposeCode.Length == 4 && purposeCode.StartsWith("00"))
        {
            return purposeCode.Substring(2);
        }
        return purposeCode;
    }
}