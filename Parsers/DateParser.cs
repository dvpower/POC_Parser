using _837ParserPOC.DataModels;
using POC837Parser.DataModels;
using System.Globalization;

namespace POC837Parser.Parsers
{
    public class DateParser
    {
        public void ParseServiceDates(string line, ClaimInformation claimInfo)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DTP*434*"))
            {
                throw new ArgumentException("Invalid DTP segment for Service Dates");
            }
            line = line.EndsWith("~") ? line[..^1] : line;

            string[] elements = line.Split('*');
            string[] dates = elements[3].Split('-');

            claimInfo.ServiceDateFrom = DateTime.ParseExact(dates[0], "yyyyMMdd", CultureInfo.InvariantCulture);
            if (dates.Length > 1)
            {
                claimInfo.ServiceDateTo = DateTime.ParseExact(dates[1], "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }

        public DateOrTimeElement Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DTP*"))
            {
                throw new ArgumentException("Invalid DTP segment");
            }

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new DateOrTimeElement
            {
                DateTimeQualifier = elements[1],
                DateTimeQualifierDescription = DateTimeQualifiers.GetDescription(elements[1]),
                DateTimePeriodFormatQualifier = elements.Length > 2 ? elements[2] : null,
                DateTimePeriod = elements.Length > 3 ? elements[3] : null,
            };
        }

    }

}
