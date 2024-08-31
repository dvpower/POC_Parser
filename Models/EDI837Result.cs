using _837ParserPOC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POC837Parser.Models
{
    public class EDI837Result
    {
        /// <summary>
        /// A unique ID that identifies this submission
        /// </summary>
        [JsonPropertyName("submissionID ")]
        public Guid SubmissionID { get; set; }


        [JsonPropertyName("interchangeControlHeader")]
        public InterchangeControlHeader InterchangeControlHeader { get; set; }

        [JsonPropertyName("interchangeControlTrailer")]
        public InterchangeControlTrailer InterchangeControlTrailer { get; set; }

        [JsonPropertyName("functionalGroup")]
        public FunctionalGroup FunctionalGroup { get; set; }

        [JsonPropertyName("transactionSets")]
        public List<TransactionSet> TransactionSets { get; set; }


    }
}
