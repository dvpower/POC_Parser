namespace POC837Parser.DataModels
{
    public class NoteElement
    {
        public string NoteReferenceCode { get; set; }
        public string Description { get; set; }

        public string NoteTypeDescription => GetNoteTypeDescription(NoteReferenceCode);

        private static readonly Dictionary<string, string> NoteTypeDescriptions = new Dictionary<string, string>
        {
            { "ADD", "Additional Information" },
            { "CER", "Certification Narrative" },
            { "DCP", "Goals, Rehabilitation Potential, or Discharge Plans" },
            { "TPO", "Third Party Organization Notes" },
            { "GEN", "General" },
            { "PMT", "Payment" },
            { "SPI", "Supplemental Payment Information" },
            { "ALG", "Allergies" },
            { "DGN", "Diagnosis Description" },
            { "DME", "Durable Medical Equipment" },
            { "MED", "Medications" },
            { "NTR", "Nutritional Requirements" },
            { "ODT", "Orders for Disciplines and Treatments" },
            { "RHB", "Functional Limitations, Reason Homebound, or Both" },
            { "RLH", "Reasons Left Home" },
            { "RNH", "Times and Reasons of Visits" },
            { "SET", "Unusual Home, Social Environment, or Both" },
            { "SFM", "Safety Measures" },
            { "SPT", "Supplementary Plan of Treatment" },
            { "UPI", "Updated Information" },
            { "OTH", "Other" },
            { "REC", "Recommendations" },
            { "ADJ", "Adjustment" },
            { "PCS", "Present on Admission" },
            { "LAB", "Laboratory Results" },
            { "PRE", "Prescription" },
            { "CTR", "Contrast Information" },
            { "OPR", "Operative Report" },
            { "RAD", "Radiology Report" },
            { "TES", "Test Results" },
            { "PAN", "Pre-Authorization Number" },
            { "DIS", "Discharge Summary" },
            { "SIG", "Signature on File" }
        };

        private string GetNoteTypeDescription(string code)
        {
            return NoteTypeDescriptions.TryGetValue(code, out var description)
                ? description
                : "Unknown Note Type";
        }
    }
}
