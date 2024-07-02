using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class InterchangeControlHeader
    {
        public string AuthorizationInformationQualifier { get; set; }
        public string AuthorizationInformation { get; set; }
        public string SecurityInformationQualifier { get; set; }
        public string SecurityInformation { get; set; }
        public string InterchangeIdQualifier { get; set; }
        public string InterchangeSenderId { get; set; }
        public string InterchangeIdQualifierReceiver { get; set; }
        public string InterchangeReceiverId { get; set; }
        public DateTime InterchangeDate { get; set; }
        public TimeSpan InterchangeTime { get; set; }
        public string InterchangeControlStandardsIdentifier { get; set; }
        public string InterchangeControlVersionNumber { get; set; }
        public string InterchangeControlNumber { get; set; }
        public string AcknowledgmentRequested { get; set; }
        public string UsageIndicator { get; set; }
        public string RepetitionSeparator { get; set; }
        public char ComponentElementSeparator { get; set; }
    }
}
