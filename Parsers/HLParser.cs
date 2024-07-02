using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _837ParserPOC.DataModels;

namespace _837ParserPOC.Parsers
{
    public class HLParser
    {
        public HierarchicalLevel Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("HL*"))
            {
                throw new ArgumentException("Invalid HL segment");
            }

            string[] elements = line.Split('*');

            if (elements.Length < 4)
            {
                throw new ArgumentException("HL segment has too few elements");
            }

            return new HierarchicalLevel
            {
                HierarchicalIdNumber = elements[1],
                HierarchicalParentIdNumber = elements[2],
                HierarchicalLevelCode = elements[3],
                HierarchicalChildCode = elements.Length > 4 ? elements[4].TrimEnd('~') : null
            };
        }

        public string[] FindHLSegments(string[] lines)
        {
            return Array.FindAll(lines, l => l.StartsWith("HL*"));
        }
    }
}
