public static class EmploymentStatusCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "1", "Full-time" },
        { "2", "Part-time" },
        { "3", "Not Employed" },
        { "4", "Self-employed" },
        { "5", "Retired" },
        { "6", "On Active Military Duty" },
        { "7", "Unknown" },
        { "8", "Disabled" },
        { "9", "Student" },
        { "10", "Leave of Absence" },
        { "11", "Seasonal" },
        { "12", "Volunteer" },
        { "13", "Apprenticeship" },
        { "14", "Internship" },
        { "15", "Contractor" },
        { "16", "Temporary" },
        { "17", "Regular" },
        { "18", "Permanent" },
        { "19", "Probationary" },
        { "20", "Casual" },
        { "21", "Inactive" },
        { "22", "Terminated" },
        { "23", "Laid Off" },
        { "24", "Suspended" },
        { "25", "Furloughed" },
        { "26", "Deceased" },
        { "27", "On Strike" },
        { "28", "Maternity Leave" },
        { "29", "Medical Leave" },
        { "30", "Personal Leave" },
        { "31", "Sabbatical" },
        { "99", "Other" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : "Unknown Employment Code Qualifier";
    }

    public static bool IsValid(string qualifierCode)
    {
        return Descriptions.ContainsKey(qualifierCode);
    }
}