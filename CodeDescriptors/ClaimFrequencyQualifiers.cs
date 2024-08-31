public static class ClaimFrequencyQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "0", "Non-Payment/Zero Claim" },
        { "1", "Admit Through Discharge Claim" },
        { "2", "Interim - First Claim" },
        { "3", "Interim - Continuing Claim" },
        { "4", "Interim - Last Claim" },
        { "5", "Late Charge Only" },
        { "6", "Adjustment of Prior Claim" },
        { "7", "Replacement of Prior Claim" },
        { "8", "Void/Cancel of Prior Claim" },
        { "9", "Final Claim for a Home Health PPS Episode" },
        { "A", "Hospital Admission Notice" },
        { "B", "Hospice/Medicare Coordinated Care Demonstration/Religious Non-Medical Health Care Institution Demonstration Notice" },
        { "C", "Hospice Change of Provider Notice" },
        { "D", "Hospice/Medicare Coordinated Care Demonstration/Religious Non-Medical Health Care Institution Demonstration Termination/Revocation Notice" },
        { "E", "Hospice Change of Ownership" },
        { "F", "Beneficiary Initiated Adjustment Claim" },
        { "G", "CWF Initiated Adjustment Claim" },
        { "H", "CMS Initiated Adjustment Claim" },
        { "I", "Intermediary Initiated Adjustment Claim" },
        { "J", "Initiated Adjustment Claim-Other" },
        { "K", "OIG Initiated Adjustment Claim" },
        { "M", "MSP Initiated Adjustment Claim" },
        { "P", "QIO Initiated Adjustment Claim" },
        { "Q", "Claim Submitted for Reconsideration (RA request)" },
        { "X", "Special/Adjustment Claim (as Defined by CMS)" }
    };

    public static string GetDescription(string frequencyCode)
    {
        return Descriptions.TryGetValue(frequencyCode, out var description)
            ? description
            : "Unknown Frequency";
    }
}