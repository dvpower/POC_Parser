using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    using _837ParserPOC.DataModels;
    using System;

    public class InterchangeControlHeaderParser
    {
        public InterchangeControlHeader Parse(string[] lines)
        {
            string isaLine = EDIParserHelper.FindSegment(lines, "ISA");
            string ieaLine = EDIParserHelper.FindSegment(lines, "IEA");

            if (isaLine == null || ieaLine == null)
            {
                throw new InvalidOperationException("ISA or IEA segment not found in the file.");
            }

            string[] isaParts = EDIParserHelper.SplitSegment(isaLine);
            string[] ieaParts = EDIParserHelper.SplitSegment(ieaLine);

            if (isaParts.Length != 17 || ieaParts.Length != 3)
            {
                throw new InvalidOperationException("Invalid ISA or IEA segment format.");
            }

            var header = new InterchangeControlHeader
            {
                AuthorizationInformationQualifier = isaParts[1],
                AuthorizationInformation = isaParts[2],
                SecurityInformationQualifier = isaParts[3],
                SecurityInformation = isaParts[4],
                InterchangeIdQualifier = isaParts[5],
                InterchangeSenderId = isaParts[6],
                InterchangeIdQualifierReceiver = isaParts[7],
                InterchangeReceiverId = isaParts[8],
                InterchangeDate = EDIParserHelper.ParseDate(isaParts[9]),
                InterchangeTime = EDIParserHelper.ParseTime(isaParts[10]),
                RepetitionSeparator = isaParts[11],
                InterchangeControlVersionNumber = isaParts[12],
                InterchangeControlNumber = isaParts[13],
                AcknowledgmentRequested = isaParts[14],
                UsageIndicator = isaParts[15],
                ComponentElementSeparator = isaParts[16][0]
            };

            PerformSanityChecks(header, ieaParts);

            return header;
        }

        private void PerformSanityChecks(InterchangeControlHeader header, string[] ieaParts)
        {
            if (header.InterchangeControlNumber != ieaParts[2].TrimEnd('~'))
            {
                throw new InvalidOperationException("ISA and IEA control numbers do not match.");
            }

            if (header.InterchangeControlVersionNumber != "00501")
            {
                throw new InvalidOperationException("Unsupported interchange control version number.");
            }

            // Add more sanity checks as needed
        }
    }
}
