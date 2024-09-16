using POC837Parser.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class Patient
    {
        public HierarchicalLevel HL { get; set; }
        public List<NM1Name> PatientNames { get; set; } = new List<NM1Name>();     
        
        public PatientDemographicInfo PatientDemographicInfo { get; set; } // 2010CA

        public PatientInformation PatientInfo { get; set; } 

        public List<ReferenceIdentificationObj> AdditionalReferenceInformation { get; set; } = new List<ReferenceIdentificationObj>();

        public List<Claim> Claims { get; set; } = new List<Claim>();
    }

    public class PatientInformation
    {
        public string IndividualRelationshipCode { get; set; }
        public string RelationshipCodeDescription { get; set; }
        public string PatientLocationCode { get; set; }
        public string PatientLocationCodeDescription { get; set; }
        public string EmploymentStatusCode { get; set; }
        public string EmploymentStatusCodeDescription { get; set; }
        public string StudentStatusCode { get; set; }
        public string DateOfDeath { get; set; }
        public string UnitOrBasisForMeasurementCode { get; set; }
        public string Weight { get; set; }
        public string PregnancyIndicator { get; set; }
        public string DateLastMenstrualPeriod { get; set; }
    }

    public class PatientAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class PatientDemographicInfo
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
