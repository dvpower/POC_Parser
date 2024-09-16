
using POC837Parser.DataModels;

namespace POC837Parser.Parsers
{
    public class PaperworkParser
    {     
        public PWKSegment Parse(string line)
        {
            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new PWKSegment
            {
                ReportTypeCode = elements[1],
                ReportTransmissionCode = elements.Length > 2 ? elements[2] : null,
                ReportCopiesNeeded = elements.Length > 3 ? elements[3] : null,
                EntityIdentifierCode = elements.Length > 4 ? elements[4] : null,
                IdentificationCodeQualifier = elements.Length > 5 ? elements[5] : null,
                IdentificationCode = elements.Length > 6 ? elements[6] : null,
            };
        }
    }
}
