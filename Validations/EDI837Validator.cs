using POC837Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Validations
{
    public class EDI837Validator
    {
        private readonly List<BaseValidator> _validators;

        public EDI837Validator()
        {
            _validators = new List<BaseValidator>
        {
            new StructuralValidator(),
            new ControlNumberValidator(),
            new LoopAndSegmentValidator(),
            // new ConditionalRequirementValidator()
            // Add more validators here as you create them
        };
        }

        public ValidationResult ValidateEDI837(EDI837Result result)
        {
            var finalResult = new ValidationResult { IsValid = true };

            foreach (var validator in _validators)
            {
                var validationResult = validator.Validate(result);
                finalResult.IsValid &= validationResult.IsValid;
                finalResult.Errors.AddRange(validationResult.Errors);
                finalResult.Warnings.AddRange(validationResult.Warnings);
            }

            return finalResult;
        }
    }
}
