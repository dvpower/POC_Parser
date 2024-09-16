using POC837Parser.DataModels;

namespace POC837Parser.Parsers
{
    public class NameParser
    {

        private NM1Name _name;

        public NM1Name Parse(string line)
        {
            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');


            this._name = new  NM1Name
            {
                EntityIdentifierCode = elements[1],
                EntityIdentifierDescription = EntityIdentifierCodeQualifiers.GetDescription(elements[1].ToString()),
                EntityTypeQualifier = elements.Length > 2 ? elements[2] : null,
                LastNameOrOrgName = elements.Length > 3 ? elements[3] : null,
                FirstName = elements.Length > 4 ? elements[4] : null,
                MiddleName = elements.Length > 5 ? elements[5] : null,
                NamePrefix = elements.Length > 6 ? elements[6] : null,
                NameSuffix = elements.Length > 7 ? elements[7] : null,
                IdentificationCodeQualifier = elements.Length > 8 ? elements[8] : null,
                IdentificationCode = elements.Length > 9 ? elements[9] : null
            };

            return this._name;
        }

        public override string ToString()
        {
            return $"{_name.EntityIdentifierDescription}";
        }

    }
}
