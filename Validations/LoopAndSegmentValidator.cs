﻿using _837ParserPOC.DataModels;
using POC837Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Validations
{
    public class LoopAndSegmentValidator : BaseValidator
    {
        public override ValidationResult Validate(EDI837Result result)
        {
            var validationResult = new ValidationResult { IsValid = true };

            foreach (var transactionSet in result.TransactionSets)
            {
                foreach (var billingProvider in transactionSet.HierarchicalStructure.BillingProviders)
                {
                  

                    foreach (var subscriber in billingProvider.Subscribers)
                    {
                        

                        foreach (var claim in subscriber.Claims)
                        {
                            Validate2300Loop(claim, validationResult);

                            foreach (var serviceLine in claim.ServiceLines)
                            {
                                Validate2400Loop(serviceLine, validationResult);
                            }
                        }
                    }
                }
            }

            return validationResult;
        }



        private void Validate2300Loop(Claim claim, ValidationResult validationResult)
        {
            if (string.IsNullOrWhiteSpace(claim.ClaimInfo?.ClaimId))
                AddError(validationResult, "Missing Claim ID in 2300 loop");

            if (claim.ClaimInfo?.TotalClaimChargeAmount <= 0)
                AddError(validationResult, $"Invalid Total Claim Charge Amount {claim.ClaimInfo?.TotalClaimChargeAmount} in 2300 loop");

            if (claim.ClaimInfo?.ServiceDateFrom == DateTime.MinValue)
                AddError(validationResult, "Missing Service Date From in 2300 loop");

            if (claim.DiagnosisCodes == null || claim.DiagnosisCodes.Count == 0)
                AddError(validationResult, "Missing Diagnosis Codes in 2300 loop");

            // Add more 2300 loop specific validations here
        }

        private void Validate2400Loop(ServiceLine serviceLine, ValidationResult validationResult)
        {
            if (string.IsNullOrWhiteSpace(serviceLine.ProcedureCode))
                AddError(validationResult, "Missing Procedure Code in 2400 loop");

            if (serviceLine.ChargeAmount <= 0)
                AddError(validationResult, $"Invalid Line Item Charge Amount {serviceLine.ChargeAmount} in 2400 loop");

            if (serviceLine.ServiceUnitCount <= 0)
                AddError(validationResult, $"Invalid Quantity {serviceLine.ServiceUnitCount} in 2400 loop");

            // Add more 2400 loop specific validations here
        }
    }
}
