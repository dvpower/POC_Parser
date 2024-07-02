using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _837ParserPOC.DataModels;

namespace _837ParserPOC.Parsers
{
    

    public class BHTParser
    {
        public BeginningOfHierarchicalTransaction Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("BHT*"))
            {
                throw new ArgumentException("Invalid BHT segment");
            }

            string[] elements = line.Split('*');

            if (elements.Length < 6)
            {
                throw new ArgumentException("BHT segment has too few elements");
            }

            return new BeginningOfHierarchicalTransaction
            {
                HierarchicalStructureCode = elements[1],
                TransactionSetPurposeCode = elements[2],
                ReferenceIdentification = elements[3],
                Date = EDIParserHelper.ParseDate(elements[4]),
                Time = EDIParserHelper.ParseTime(elements[5]),
                TransactionTypeCode = elements.Length > 6 ? elements[6].TrimEnd('~') : null
            };
        }

        public string FindBHTSegment(string[] lines)
        {
            return Array.Find(lines, l => l.StartsWith("BHT*"));
        }

        //private DateTime ParseDate(string dateString)
        //{
        //    return DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        //}

        //private TimeSpan ParseTime(string timeString)
        //{
        //    return TimeSpan.ParseExact(timeString, "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
        //}
    }
}
