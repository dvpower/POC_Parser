using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class ReferenceIdentificationObj
    {
        public string ReferenceIdentificationQualifier { get; set; }  // 1
        public string ReferenceIdentificationQualifierDescription { get; set; }  // 1-DESC
        public string ReferenceIdentification { get; set; }  // 2

        public string Description { get; set; } // 3   A free-form description to clarify the related data elements and their content
        public string SecondaryReferenceIdentification { get; set; }  // 4
    }
}
