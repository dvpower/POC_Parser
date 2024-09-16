public static class ReferenceIdentificationQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "0B", "State License Number" },
        { "1A", "Blue Cross Provider Number" },
        { "1B", "Blue Shield Provider Number" },
        { "1C", "Medicare Provider Number" },
        { "1D", "Medicaid Provider Number" },
        { "1G", "Provider UPIN Number" },
        { "1H", "CHAMPUS Identification Number" },
        { "1J", "Facility ID Number" },
        { "1L", "Group or Policy Number" },
        { "1W", "Member Identification Number" },
        { "23", "Client Number" },
        { "26", "Union Number" },
        { "3H", "Case Number" },
        { "4A", "Personal Identification Number (PIN)" },
        { "6O", "Cross Reference Number" },
        { "6P", "Group Number" },
        { "9A", "Repriced Claim Reference Number" },
        { "9C", "Adjusted Repriced Claim Reference Number" },
        { "9F", "Referral Number" },
        { "9K", "Servicer" },
        { "AY", "Taxpayer ID Number" },
        { "BB", "Authorization Number" },
        { "BLT", "Billing Type" },
        { "CE", "Class of Contract Code" },
        { "CT", "Contract Number" },
        { "EA", "Medical Record Identification Number" },
        { "EI", "Employer's Identification Number" },
        { "EJ", "Patient Account Number" },
        { "F8", "Original Reference Number" },
        { "FJ", "Line Item Control Number" },
        { "FY", "Claim Office Number" },
        { "G1", "Prior Authorization Number" },
        { "G2", "Provider Commercial Number" },
        { "G3", "Predetermination of Benefits Identification Number" },
        { "HPI", "Health Plan Identifier" },
        { "IG", "Insurance Policy Number" },
        { "IK", "Member Number" },
        { "LU", "Location Number" },
        { "N5", "Provider Plan Network Identification Number" },
        { "NPI", "National Provider Identifier" },
        { "Q4", "Prior Identifier Number" },
        { "SY", "Social Security Number" },
        { "TJ", "Federal Taxpayer's Identification Number" },
        { "X4", "Clinical Laboratory Improvement Amendment Number" },
        { "XZ", "Pharmacy Prescription Number" },
        { "YY", "Geographic Number" },
        { "ZH", "Clinic Number" },
        { "ZZ", "Mutually Defined" },
        { "2U", "Payer Identification Number" },
        { "ABB", "Authorized Entity" },
        { "D9", "Claim Number" },
        { "E9", "Attachment Code" },
        { "EO", "Submitter Identification Number" },
        { "P4", "Project Code" },
        { "PID", "Program Identification Number" },
        { "RB", "Rate code number" },
        { "Y4", "Agency Claim Number" }, 
            { "PXC", "Health Care Provider Taxonomy Code" }, // Additional
            { "B3", "Preferred Provider Organization Number" },
            { "FH", "Clinic Number" },
            { "G5", "Provider Site Number" },
            { "U3", "Unique Supplier Identification Number (USIN)" },
            { "X5", "State Industrial Accident Provider Number" },
            { "PRN", "Provider Number" },
            { "HPI___", "HIPAA National Provider Identifier" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : $"Unknown Reference Identification Qualifier {qualifierCode}";
    }

    public static bool IsValid(string qualifierCode)
    {
        return Descriptions.ContainsKey(qualifierCode);
    }
}