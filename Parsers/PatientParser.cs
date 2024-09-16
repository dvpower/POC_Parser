using _837ParserPOC.DataModels;
using System.Globalization;

namespace _837ParserPOC.Parsers
{
    public class PatientDemographicInfoParser
    {
        public PatientDemographicInfo Parse(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.StartsWith("DMG*"))
            {
                throw new ArgumentException("Invalid DMG segment for Patient Demographic Info");
            }

            string[] elements = line.Split('*');

            return new PatientDemographicInfo
            {
                DateOfBirth = DateTime.ParseExact(elements[2], "yyyyMMdd", CultureInfo.InvariantCulture),
                Gender = elements[3].TrimEnd('~')
            };
        }
    }

    public class PatientInformationParser
    {
        public PatientInformation Parse(string line)
        {

            line = line.EndsWith("~") ? line[..^1] : line;
            string[] elements = line.Split('*');

            return new PatientInformation
            {
                IndividualRelationshipCode = elements[1],
                RelationshipCodeDescription = RelationshipCodeQualifiers.GetDescription(elements[1].ToString()),
                PatientLocationCode = elements.Length > 2 ? elements[2] : null,
                PatientLocationCodeDescription = elements.Length > 2 ? PatientLocationCodeQualifiers.GetDescription(elements[1].ToString()) : null,
                EmploymentStatusCode = elements.Length > 3 ? elements[3] : null,
                EmploymentStatusCodeDescription = elements.Length > 3 ? EmploymentStatusCodeQualifiers.GetDescription(elements[1].ToString()) : null,
                StudentStatusCode = elements.Length > 4 ? elements[4] : null,
                DateOfDeath = elements.Length > 5 ? elements[5] : null,
                UnitOrBasisForMeasurementCode = elements.Length > 6 ? elements[6] : null,
                Weight = elements.Length > 7 ? elements[7] : null,
                PregnancyIndicator = elements.Length > 8 ? elements[8] : null,
                DateLastMenstrualPeriod = elements.Length > 9 ? elements[9] : null
            };
        }
    }

}
