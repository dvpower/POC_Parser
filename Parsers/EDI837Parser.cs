using _837ParserPOC.DataModels;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _837ParserPOC.Parsers
{
    public class EDI837Parser
    {
        private readonly InterchangeControlHeaderParser _interchangeParser;
        private readonly FunctionalGroupParser _functionalGroupParser;
        private readonly TransactionSetParser _transactionSetParser;
        private readonly EDI837Preprocessor _preprocessor;

        public EDI837Parser()
        {
            _interchangeParser = new InterchangeControlHeaderParser();
            _functionalGroupParser = new FunctionalGroupParser();
            _transactionSetParser = new TransactionSetParser();
            _preprocessor = new EDI837Preprocessor();
        }
        public string ToJson(EDI837Result result)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(result, options);
        }


        /// <summary>
        /// Represents the parsed and response of a 837 text submission.
        /// </summary>
        public EDI837Result ParseEDI837(string rawContent)
        {
            string[] segments = _preprocessor.PreprocessEDI837(rawContent);

            var result = new EDI837Result();

            // Parse ISA segment
            string isaSegment = Array.Find(segments, s => s.StartsWith("ISA" + _preprocessor.GetElementSeparator()));
            if (isaSegment == null)
            {
                throw new InvalidOperationException("ISA segment not found");
            }
            result.InterchangeControlHeader = ParseInterchangeControlHeader(isaSegment);

            // Parse IEA segment
            string ieaSegment = Array.FindLast(segments, s => s.StartsWith("IEA" + _preprocessor.GetElementSeparator()));
            if (ieaSegment == null)
            {
                throw new InvalidOperationException("IEA segment not found");
            }

            result.InterchangeControlTrailer = ParseInterchangeControlTrailer(ieaSegment);


            // Now parse the rest of the file using the preprocessed segments          
            result.FunctionalGroup = _functionalGroupParser.Parse(segments);
            result.TransactionSets = ParseTransactionSets(segments);

            return result;
        }

        private InterchangeControlHeader ParseInterchangeControlHeader(string isaSegment)
        {
            string[] elements = isaSegment.TrimEnd(_preprocessor.GetSegmentTerminator()).Split(_preprocessor.GetElementSeparator());

            if (elements.Length < 17)
            {
                throw new InvalidOperationException("ISA segment does not contain the expected number of elements");
            }

            return new InterchangeControlHeader
            {
                AuthorizationInformationQualifier = elements[1],
                AuthorizationInformation = elements[2],
                SecurityInformationQualifier = elements[3],
                SecurityInformation = elements[4],
                InterchangeIdQualifier = elements[5],
                InterchangeSenderId = elements[6],
                InterchangeIdQualifierReceiver = elements[7],
                InterchangeReceiverId = elements[8],
                InterchangeDate = EDIParserHelper.ParseDate(elements[9]),
                InterchangeTime = EDIParserHelper.ParseTime(elements[10]),
                InterchangeControlStandardsIdentifier = elements[11],
                InterchangeControlVersionNumber = elements[12],
                InterchangeControlNumber = elements[13],
                AcknowledgmentRequested = elements[14],
                UsageIndicator = elements[15],
                ComponentElementSeparator = elements[16][0] // Assuming this is a single character
            };
        }

        private InterchangeControlTrailer ParseInterchangeControlTrailer(string ieaSegment)
        {
            string[] elements = ieaSegment.TrimEnd(_preprocessor.GetSegmentTerminator()).Split(_preprocessor.GetElementSeparator());

            return new InterchangeControlTrailer
            {
                NumberOfIncludedFunctionalGroups = int.Parse(elements[1]),
                InterchangeControlNumber = elements[2],
            };
        }

        private List<TransactionSet> ParseTransactionSets(string[] lines)
        {
            var transactionSets = new List<TransactionSet>();
            int currentIndex = 0;

            while (currentIndex < lines.Length)
            {
                if (lines[currentIndex].StartsWith("ST*837"))
                {
                    int endIndex = Array.FindIndex(lines, currentIndex, l => l.StartsWith("SE*"));
                    if (endIndex == -1)
                    {
                        throw new InvalidOperationException("SE segment not found for transaction set");
                    }

                    string[] transactionSetLines = lines.Skip(currentIndex).Take(endIndex - currentIndex + 1).ToArray();
                    var transactionSet = _transactionSetParser.Parse(transactionSetLines);
                    transactionSets.Add(transactionSet);

                    currentIndex = endIndex + 1;
                }
                else
                {
                    currentIndex++;
                }
            }

            return transactionSets;
        }
    }



}
