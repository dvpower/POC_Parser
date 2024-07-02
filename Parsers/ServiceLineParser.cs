using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class ServiceLineParser
    {
        public ServiceLine Parse(string[] lines)
        {
            var serviceLine = new ServiceLine();

            foreach (var line in lines)
            {
                if (line.StartsWith("LX*"))
                {
                    serviceLine.LineNumber = line.Split('*')[1].TrimEnd('~');
                }
                else if (line.StartsWith("SV1*")) // Professional (837P) and Dental (837D) claims
                {
                    ParseSV1Segment(line, serviceLine);
                }

                else if (line.StartsWith("SV2*")) // Institutional (837I) claims
                {
                    ParseSV2Segment(line, serviceLine);
                }
                else if (line.StartsWith("DTP*472*"))
                {
                    ParseServiceDates(line, serviceLine);
                }
            }

            return serviceLine;
        }

        private void ParseSV2Segment(string line, ServiceLine serviceLine)
        {
            string[] elements = line.Replace('~', ' ').Split('*');

            serviceLine.RevenueCode = elements[1]; /// e.g. 0305
            string[] procedureElements = elements[2].Split(':');
            serviceLine.ProcedureCode = procedureElements[1];  // e.g.  HC:85025
            serviceLine.ChargeAmount = decimal.Parse(elements[3]);
            serviceLine.UnitOrBasisOfMeasurement = elements[4];
            serviceLine.ServiceUnitCount = int.Parse(elements[5]);        

        }

        private void ParseSV1Segment(string line, ServiceLine serviceLine)
        {

            try
            {

                string[] elements = line.Replace('~', ' ').Split('*');

                //serviceLine.RevenueCode = elements[1]; /// e.g. 0305
                string[] procedureElements = elements[1].Split(':');
                serviceLine.ProcedureCode = procedureElements[1];  // e.g.  HC:85025
                serviceLine.ChargeAmount = decimal.Parse(elements[2]);
                serviceLine.UnitOrBasisOfMeasurement = elements[3];
                serviceLine.ServiceUnitCount = int.Parse(elements[4]);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("SV1 Not implemented (I think SV1 is a P(rofessionsal) Claim )", ex);
            }
            
        }
        private void ParseServiceDates(string line, ServiceLine serviceLine)
        {
            string[] elements = line.TrimEnd('~').Split('*');
            string[] dates = elements[3].TrimEnd('~').Split('-');

            serviceLine.ServiceDateFrom = DateTime.ParseExact(dates[0], "yyyyMMdd", CultureInfo.InvariantCulture);
            serviceLine.ServiceDateTo = DateTime.ParseExact(dates[dates.Length-1], "yyyyMMdd", CultureInfo.InvariantCulture); // May be a range
        }
    }
}
