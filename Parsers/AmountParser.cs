using _837ParserPOC.DataModels;

namespace POC837Parser.Parsers
{
    public class AmountParser
    {
        public Amount Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("AMT*"))
            {
                throw new ArgumentException("Invalid AMT segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new Amount
            {
                AmountQualifierCode = elements[1],
                Description = AmountQualifiers.GetDescription(elements[1]),
                MonetaryAmount = elements.Length > 2 ? decimal.Parse(elements[2]) : 0,
                CreditDebitFlagCode = elements.Length > 3 ? elements[3] : null,
            };
        }
    }
}
