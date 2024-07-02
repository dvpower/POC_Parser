using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.DataModels
{
    public class FunctionalGroup
    {
        // Identifies the type of transactions in this group
        public string FunctionalIdentifierCode { get; set; }

        // Code identifying the sender
        public string ApplicationSenderCode { get; set; }

        // Code identifying the receiver
        public string ApplicationReceiverCode { get; set; }

        // Date the group was prepared
        public DateTime GroupDate { get; set; }

        // Time the group was prepared
        public TimeSpan GroupTime { get; set; }

        // Assigned number for this group
        public string GroupControlNumber { get; set; }

        // Code identifying the agency responsible for the standard
        public string ResponsibleAgencyCode { get; set; }

        // Version of the standard used
        public string VersionReleaseIndustryIdentifierCode { get; set; }

        // Number of transaction sets in this group
        public int NumberOfTransactionSetsIncluded { get; set; }
    }
}
