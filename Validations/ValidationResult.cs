using POC837Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Validations
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; } = new List<string>();
        public List<string> Warnings { get; } = new List<string>();
    }

    public abstract class BaseValidator
    {
        public abstract ValidationResult Validate(EDI837Result result);

        protected void AddError(ValidationResult validationResult, string error)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add(error);
        }

        protected void AddWarning(ValidationResult validationResult, string warning)
        {
            validationResult.Warnings.Add(warning);
        }
    }
}
