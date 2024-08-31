using POC837Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Validations
{
    public class StructuralValidator : BaseValidator
    {
        public override ValidationResult Validate(EDI837Result result)
        {
            var validationResult = new ValidationResult { IsValid = true };

            // Check for required segments
            if (result.InterchangeControlHeader == null)
                AddError(validationResult, "Missing Interchange Control Header (ISA segment)");

            if (result.FunctionalGroup == null)
                AddError(validationResult, "Missing Functional Group (GS segment)");

            if (result.TransactionSets == null || result.TransactionSets.Count == 0)
                AddError(validationResult, "Missing Transaction Sets (ST segments)");

            // Add more structural validations here

            return validationResult;
        }
    }
}
