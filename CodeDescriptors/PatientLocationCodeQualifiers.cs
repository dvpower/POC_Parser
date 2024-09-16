public static class PatientLocationCodeQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "01", "On Campus" },
        { "02", "Off Campus" },
        { "03", "Outpatient" },
        { "04", "Diagnosis Related Group (DRG) Exempt Unit" },
        { "05", "Intensive Care Unit" },
        { "06", "Residential Facility" },
        { "07", "Skilled Nursing Facility" },
        { "08", "Emergency Room" },
        { "09", "Clinic" },
        { "10", "Home" },
        { "11", "Office" },
        { "12", "Ambulatory Surgical Center" },
        { "13", "Birthing Center" },
        { "14", "Inpatient Psychiatric Facility" },
        { "15", "Residential Substance Abuse Treatment Facility" },
        { "16", "Independent Laboratory" },
        { "17", "Nursing Home" },
        { "18", "Assisted Living Facility" },
        { "19", "School" },
        { "20", "Custodial Care Facility" },
        { "21", "Inpatient Rehabilitation Facility" },
        { "22", "Hospice" },
        { "23", "Ambulance - Land" },
        { "24", "Ambulance - Air or Water" },
        { "25", "Inpatient Hospital" },
        { "26", "Psychiatric Residential Treatment Center" },
        { "27", "Community Mental Health Center" },
        { "28", "Correctional Facility" },
        { "29", "Adult Day Care" },
        { "30", "Adult Foster Care" },
        { "31", "Urgent Care Facility" },
        { "32", "Intermediate Care Facility/Individuals with Intellectual Disabilities" },
        { "33", "Residential Treatment Facility" },
        { "34", "Partial Hospitalization Program" },
        { "35", "Comprehensive Outpatient Rehabilitation Facility" },
        { "36", "End-Stage Renal Disease Treatment Facility" },
        { "37", "Telehealth" },
        { "99", "Other Unlisted Facility" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : "Unknown Location Code Qualifier";
    }

    public static bool IsValid(string qualifierCode)
    {
        return Descriptions.ContainsKey(qualifierCode);
    }
}