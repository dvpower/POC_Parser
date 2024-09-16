using POC837Parser.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{

    public class ServiceLine
    {
        public string LineNumber { get; set; } // From LX segment
        public string RevenueCode { get; set; }
        public string ProcedureCode { get; set; }
        public decimal ChargeAmount { get; set; }
        public string UnitOrBasisOfMeasurement { get; set; }
        public decimal ServiceUnitCount { get; set; }
        public string ProcedureModifier1 { get; set; }
        public string ProcedureModifier2 { get; set; }
        public string ProcedureModifier3 { get; set; }
        public string ProcedureModifier4 { get; set; }
        public string Description { get; set; }
        public string ProductOrServiceId { get; set; }
        public List<PWKSegment> Paperwork { get; set; } = new List<PWKSegment>();
        public List<DateOrTimeElement> Dates { get; set; } = new List<DateOrTimeElement>();
        public List<ReferenceIdentificationObj> ReferenceInformation { get; set; } = new List<ReferenceIdentificationObj>();
        public List<Amount> MonetaryAmounts { get; set; } = new List<Amount>();
        public List<NoteElement> Notes { get; set; } = new List<NoteElement>();
    }  


    //public class DTPSegment
    //{
    //    public string DateTimeQualifier { get; set; }
    //    public string DateTimePeriodFormatQualifier { get; set; }
    //    public string DateTimePeriod { get; set; }
    //}

    //public class REFSegment
    //{
    //    public string ReferenceIdentificationQualifier { get; set; }
    //    public string ReferenceIdentification { get; set; }
    //    public string Description { get; set; }
    //}

    //public class AMTSegment
    //{
    //    public string AmountQualifierCode { get; set; }
    //    public decimal MonetaryAmount { get; set; }
    //    public string CreditDebitFlagCode { get; set; }
    //}

    public class ServiceLineOLD
    {
        public string LineNumber { get; set; }
        public int SequenceNumber { get; set; }
        public string RevenueCode { get; set; }
        public string ProcedureCode { get; set; }
        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
        public decimal ChargeAmount { get; set; }
        public string UnitOrBasisOfMeasurement { get; set; }
        public decimal ServiceUnitCount { get; set; }
        public DateTime ServiceDateFrom { get; set; }
        public DateTime ServiceDateTo { get; set; }
    }

}
