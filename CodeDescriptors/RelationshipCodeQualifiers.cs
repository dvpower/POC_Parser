public static class RelationshipCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "01", "Spouse" },
        { "18", "Self" },
        { "19", "Child" },
        { "20", "Employee" },
        { "21", "Unknown" },
        { "22", "Handicapped Dependent" },
        { "23", "Sponsored Dependent" },
        { "24", "Dependent of a Minor Dependent" },
        { "29", "Significant Other" },
        { "32", "Mother" },
        { "33", "Father" },
        { "34", "Other Adult" },
        { "36", "Emancipated Minor" },
        { "39", "Organ Donor" },
        { "40", "Cadaver Donor" },
        { "41", "Injured Plaintiff" },
        { "43", "Child Where Insured Has No Financial Responsibility" },
        { "53", "Life Partner" },
        { "G8", "Other Relationship" },
        { "G9", "Other Relative" },
        { "31", "Grandfather or Grandmother" },
        { "38", "Collateral Dependent" },
        { "48", "Stepchild" },
        { "50", "Foster Child" },
        { "51", "Son or Daughter" },
        { "52", "Step Son or Step Daughter" },
        { "72", "Nephew or Niece" },
        { "73", "Foster Parent" },
        { "74", "Grandchild" },
        { "75", "Grandfather" },
        { "76", "Grandmother" },
        { "77", "Uncle" },
        { "78", "Aunt" },
        { "79", "Cousin" },
        { "80", "Adopted Child" },
        { "81", "Adopted Daughter" },
        { "82", "Adopted Son" },
        { "83", "Adoptive Father" },
        { "84", "Adoptive Mother" },
        { "92", "Ward" },
        { "95", "Trustee" },
        { "96", "Trust" },
        { "97", "Trust Estate" },
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : "Unknown Relationship Code Qualifier";
    }

    public static bool IsValid(string qualifierCode)
    {
        return Descriptions.ContainsKey(qualifierCode);
    }
}