using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{


    public class ServiceLine
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

    public class ServiceLineOLD2
    {

        public string LineNumber { get; set; }
        public string RevenueCode { get; set; }
        public string ProcedureCode { get; set; }
        public decimal LineItemChargeAmount { get; set; }
        public string UnitOrBasisForMeasurement { get; set; }
        public int ServiceUnitCount { get; set; }
        public decimal? UnitRate { get; set; }
        public string LineItemDenialOrRejectCode { get; set; }
        public decimal? ServiceLinePaidAmount { get; set; }
        public decimal? NonCoveredChargeAmount { get; set; }
        // Reserved fields 10 and 11 are omitted
        public decimal? FacilityTaxAmount { get; set; }
        public decimal? PatientWeight { get; set; }
        public DateTime? HomeHealthStartDate { get; set; }
        public DateTime? HomeHealthEndDate { get; set; }
        public string HomeHealthOASISCode { get; set; }
        public int? HomeHealthDayCount { get; set; }
        public int? HomeHealthVisitCount { get; set; }
        public decimal? HomeHealthVisitHoursCount { get; set; }
        public string Description { get; set; }
    }


    public class ServiceLineOLD
    {
        public string LineNumber { get; set; }
        public string RevenueCode { get; set; } // Used in SV2
        public string ProcedureCode { get; set; }
        public string ProcedureModifier1 { get; set; }
        public string ProcedureModifier2 { get; set; }
        public string ProcedureModifier3 { get; set; }
        public string ProcedureModifier4 { get; set; }
        public string ProcedureDescription { get; set; } // Used in SV2
        public decimal LineItemChargeAmount { get; set; }
        public string UnitOrBasisForMeasurement { get; set; } // New field for SV1
        public decimal Quantity { get; set; }
        public decimal? UnitPrice { get; set; } // Keep this, but it's not used in SV1
        public DateTime ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
        public string PlaceOfServiceCode { get; set; }
        public List<string> DiagnosisCodePointers { get; set; } = new List<string>();
        public string EmergencyIndicator { get; set; } // New field for SV1
    }
}
