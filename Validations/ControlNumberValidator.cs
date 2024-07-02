using _837ParserPOC.DataModels;
using _837ParserPOC.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Validations
{
    public class ControlNumberValidator : BaseValidator
    {
        public override ValidationResult Validate(EDI837Result result)
        {
            var validationResult = new ValidationResult { IsValid = true };

            // Check ISA13 control number
            // Assuming the IEA segment is the last segment in the file
            //string ieaControlNumber = FindIEAControlNumber(result);
            //if (result.InterchangeControlHeader.InterchangeControlNumber != ieaControlNumber)
            //    AddError(validationResult, "Mismatch in Interchange Control Numbers between ISA and IEA segments");

            // Check GS06 control number
            // Assuming the GE segment is the second-to-last segment in the file
            //string geControlNumber = FindGEControlNumber(result);
            //if (result.FunctionalGroup.GroupControlNumber != geControlNumber)
            //    AddError(validationResult, "Mismatch in Group Control Numbers between GS and GE segments");

            // Check ST02 control numbers
            foreach (var transactionSet in result.TransactionSets)
            {
                string seControlNumber = FindSEControlNumber(transactionSet);
                if (transactionSet.Header.TransactionSetControlNumber != seControlNumber)
                    AddError(validationResult, $"Mismatch in Transaction Set Control Numbers for set {transactionSet.Header.TransactionSetControlNumber}");
            }

            return validationResult;
        }

        private string FindIEAControlNumber(EDI837Result result)
        {
            // This method should find the IEA segment and extract the control number
            // You'll need to implement this based on how you've stored the raw EDI data
            // For example, if you've kept the original lines:
            // return result.OriginalLines.LastOrDefault(l => l.StartsWith("IEA*"))?.Split('*')[2];
            throw new NotImplementedException("IEA control number extraction not implemented");
        }

        private string FindGEControlNumber(EDI837Result result)
        {
            // This method should find the GE segment and extract the control number
            // Similar to FindIEAControlNumber, but for the GE segment
            throw new NotImplementedException("GE control number extraction not implemented");
        }

        private string FindSEControlNumber(TransactionSet transactionSet)
        {
            // This method should find the SE segment for this transaction set and extract the control number
            // You might need to adjust this based on how you've structured your TransactionSet class
            return transactionSet.Trailer.TransactionSetControlNumber;
        }
    }
}
