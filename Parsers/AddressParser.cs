using POC837Parser.DataModels;
using System;

namespace _837ParserPOC.Parsers
{
    public class AddressParser
    {

        

        public Address Parse(string[] lines)
        {
            string n3Line = Array.Find(lines, l => l.StartsWith("N3*"));
            n3Line = n3Line.EndsWith("~") ? n3Line[..^1] : n3Line;


            string n4Line = Array.Find(lines, l => l.StartsWith("N4*"));
            n4Line = n4Line.EndsWith("~") ? n4Line[..^1] : n4Line;

            if (n3Line == null || n4Line == null)
            {
                throw new ArgumentException("Missing N3 or N4 segment for Address");
            }

            string[] n3Elements = n3Line.Split('*');
            string[] n4Elements = n4Line.Split('*');
 
            return new Address
            {
                AddressLine1 = n3Elements[1],
                AddressLine2 = n3Elements.Length > 2 ? n3Elements[2] : null,
                City = n4Elements[1],
                StateOrProvinceCode = n4Elements[2],
                PostalCode = n4Elements[3],
                CountryCode = n4Elements.Length > 4 ? n4Elements[4] : null,
                LocationQualifier = n4Elements.Length > 5 ? n4Elements[5] : null,
                LocationIdentifier = n4Elements.Length > 5 ? n4Elements[5] : null
            };
        }

        public override string ToString()
        {
            return $"AddressParser";
        }


    }
}
