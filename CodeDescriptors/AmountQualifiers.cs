public static class AmountQualifiers
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "A8", "Noncovered Charges - Actual" },
        { "AU", "Coverage Amount" },
        { "B6", "Allowed Amount" },
        { "D", "Payor Amount Paid" },
        { "DY", "Per Day Limit" },
        { "EAF", "Expected Expenditure Amount" },
        { "F3", "Patient Responsibility" },
        { "F4", "Postage Claimed" },
        { "F5", "Patient Amount Paid" },
        { "GA", "Outlier Amount" },
        { "I", "Interest" },
        { "KH", "Deduction Amount" },
        { "NL", "Negative Ledger Balance" },
        { "T", "Tax" },
        { "T2", "Total Claim Before Taxes" },
        { "ZK", "Federal Medicare or Medicaid Payment Mandate - Category 1" },
        { "ZL", "Federal Medicare or Medicaid Payment Mandate - Category 2" },
        { "ZM", "Federal Medicare or Medicaid Payment Mandate - Category 3" },
        { "ZN", "Federal Medicare or Medicaid Payment Mandate - Category 4" },
        { "ZO", "Federal Medicare or Medicaid Payment Mandate - Category 5" },
        { "N1", "Net Worth" },
        { "PB", "Rate Per Bed" },
        { "R", "Spend Down" },
        { "RS", "Rate Per Service" },
        { "S", "Service Charge" },
        { "TT", "Total" },
        { "AAE", "Deductible Amount" },
        { "AAF", "Coinsurance Amount" },
        { "AAG", "Copay Amount" },
        { "AAH", "Estimated Responsibility Amount" },
        { "AAI", "Prior Payment Amount" },
        { "C1", "Co-Payment Amount" },
        { "D2", "Inpatient Deductible - Full Amount" },
        { "D3", "Inpatient Deductible - Time Period Amount" },
        { "EBA", "Expected Expenditure Amount, Best" },
        { "EBD", "Expected Expenditure Amount, Worst" },
        { "FAO", "Final Allowed Amount" },
        { "G3", "Fee Schedule Amount" },
        { "GT", "Goods and Services Tax" },
        { "IGT", "Intergovernmental Transfer Amount" },
        { "IL", "Incentive Payment Amount" },
        { "N", "Excess Non-Covered" },
        { "NE", "Non-Eligible Professional Component Amount" },
        { "P3", "Periodic Payment Amount" },
        { "P4", "Per Visit Amount" },
        { "PBP", "Prior Balance Payment" },
        { "SI", "Susceptible Interest" },
        { "X", "Payment Adjustment" }
    };

    public static string GetDescription(string qualifierCode)
    {
        return Descriptions.TryGetValue(qualifierCode, out var description)
            ? description
            : $"Unknown Amount Type {qualifierCode}";
    }

    public static bool IsValid(string transactionTypeCode)
    {
        return Descriptions.ContainsKey(transactionTypeCode);
    }
}