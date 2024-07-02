using _837ParserPOC.DataModels;
using System;

namespace _837ParserPOC.Parsers
{
    public class FunctionalGroupParser
    {
        public FunctionalGroup Parse(string[] lines)
        {
            string gsLine = EDIParserHelper.FindSegment(lines, "GS");
            string geLine = EDIParserHelper.FindSegment(lines, "GE");

            if (gsLine == null || geLine == null)
            {
                throw new InvalidOperationException("GS or GE segment not found in the file.");
            }

            string[] gsParts = EDIParserHelper.SplitSegment(gsLine);
            string[] geParts = EDIParserHelper.SplitSegment(geLine);

            if (gsParts.Length != 9 || geParts.Length != 3)
            {
                throw new InvalidOperationException("Invalid GS or GE segment format.");
            }

            var group = new FunctionalGroup
            {
                FunctionalIdentifierCode = gsParts[1],
                ApplicationSenderCode = gsParts[2],
                ApplicationReceiverCode = gsParts[3],
                GroupDate = EDIParserHelper.ParseDate(gsParts[4]),
                GroupTime = EDIParserHelper.ParseTime(gsParts[5]),
                GroupControlNumber = gsParts[6],
                ResponsibleAgencyCode = gsParts[7],
                VersionReleaseIndustryIdentifierCode = gsParts[8],
                NumberOfTransactionSetsIncluded = int.Parse(geParts[1])
            };

            PerformSanityChecks(group, geParts);

            return group;
        }

        private void PerformSanityChecks(FunctionalGroup group, string[] geParts)
        {
            if (group.GroupControlNumber != geParts[2].TrimEnd('~'))
            {
                throw new InvalidOperationException("GS and GE control numbers do not match.");
            }

            if (group.FunctionalIdentifierCode != "HC")
            {
                throw new InvalidOperationException("Invalid Functional Identifier Code for 837 transaction.");
            }

            // Add more sanity checks as needed
        }
    }
}
