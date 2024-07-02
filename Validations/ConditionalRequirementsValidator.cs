//using _837ParserPOC.DataModels;
//using _837ParserPOC.Parsers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _837ParserPOC.Validations
//{

//    public class ConditionalRequirementValidator : BaseValidator
//    {
//        public override ValidationResult Validate(EDI837Result result)
//        {
//            var validationResult = new ValidationResult { IsValid = true };

//            foreach (var transactionSet in result.TransactionSets)
//            {
//                foreach (var billingProvider in transactionSet.HierarchicalStructure.BillingProviders)
//                {
//                    foreach (var subscriber in billingProvider.Subscribers)
//                    {
//                        ValidateSubscriber(subscriber, validationResult);

//                        foreach (var claim in subscriber.Claims)
//                        {
//                            ValidateClaim(claim, validationResult);

//                            foreach (var serviceLine in claim.ServiceLines)
//                            {
//                                ValidateServiceLine(serviceLine, claim, validationResult);
//                            }
//                        }
//                    }
//                }
//            }

//            return validationResult;
//        }

//        private void ValidateSubscriber(Subscriber subscriber, ValidationResult validationResult)
//        {
//            // If the subscriber is not the patient, patient information must be present
//            if (subscriber.SubscriberName.SubscriberLastName != subscriber.Patients.FirstOrDefault()?.PatientName.PatientLastName)
//            {
//                if (subscriber.Patients.Count == 0 || subscriber.Patients[0].PatientName == null)
//                {
//                    AddError(validationResult, "Patient information is required when subscriber is not the patient");
//                }
//            }

//            // Check for coordination of benefits in the claims
//            foreach (var claim in subscriber.Claims)
//            {
//                ValidateCoordinationOfBenefits(claim, validationResult);
//            }
//        }

//        private void ValidateCoordinationOfBenefits(Claim claim, ValidationResult validationResult)
//        {
//            // Check if there's any indicator of other insurance in the claim
//            bool hasOtherInsurance = !string.IsNullOrWhiteSpace(claim.OtherInsuranceInformation) ||
//                                     claim.OtherPayerPaidAmount > 0 ||
//                                     (claim.PayerName != null && claim.PayerName.EntityIdentifierCode == "TT"); // TT often indicates tertiary payer

//            if (hasOtherInsurance)
//            {
//                // Check if all required other insurance information is present
//                if (string.IsNullOrWhiteSpace(claim.OtherInsuranceInformation) ||
//                    claim.OtherPayerPaidAmount <= 0 ||
//                    string.IsNullOrWhiteSpace(claim.OtherPayerClaimFilingIndicator))
//                {
//                    AddError(validationResult, $"Incomplete other payer information for claim {claim.ClaimInfo.ClaimId} when coordination of benefits is indicated");
//                }
//            }
//        }


//        private void ValidateClaim(Claim claim, ValidationResult validationResult)
//        {
//            // If claim is related to an accident, accident details are required
//            if (claim.ClaimInfo.AccidentRelatedIndicator == "Y")
//            {
//                if (string.IsNullOrWhiteSpace(claim.AccidentDate) || string.IsNullOrWhiteSpace(claim.AccidentState))
//                {
//                    AddError(validationResult, $"Accident details are required for claim {claim.ClaimInfo.ClaimId} when accident related indicator is Y");
//                }
//            }

//            // If claim has an attachment, attachment information is required
//            if (claim.ClaimInfo.AttachmentIndicator == "Y")
//            {
//                if (string.IsNullOrWhiteSpace(claim.AttachmentControlNumber))
//                {
//                    AddError(validationResult, $"Attachment control number is required for claim {claim.ClaimInfo.ClaimId} when attachment indicator is Y");
//                }
//            }

//            // If claim is for ambulance service, origin and destination information is required
//            if (claim.ClaimInfo.AmbulanceServiceIndicator == "Y")
//            {
//                if (string.IsNullOrWhiteSpace(claim.AmbulanceOriginInfo) || string.IsNullOrWhiteSpace(claim.AmbulanceDestinationInfo))
//                {
//                    AddError(validationResult, $"Ambulance origin and destination information is required for claim {claim.ClaimInfo.ClaimId} when ambulance service indicator is Y");
//                }
//            }
//        }

//        private void ValidateServiceLine(ServiceLine serviceLine, Claim claim, ValidationResult validationResult)
//        {
//            // If service is for a drug, NDC information is required
//            if (serviceLine.ProcedureCode.StartsWith("J") || serviceLine.ProcedureCode.StartsWith("Q"))
//            {
//                if (string.IsNullOrWhiteSpace(serviceLine.NationalDrugCode))
//                {
//                    AddError(validationResult, $"National Drug Code (NDC) is required for drug-related procedure code {serviceLine.ProcedureCode} in claim {claim.ClaimInfo.ClaimId}");
//                }
//            }

//            // If units of service are greater than 1, unit price should be present
//            if (serviceLine.Quantity > 1 && !serviceLine.UnitPrice.HasValue)
//            {
//                AddWarning(validationResult, $"Unit price should be present when quantity is greater than 1 for service line in claim {claim.ClaimInfo.ClaimId}");
//            }

//            // If a modifier is present, ensure it's valid for the procedure code
//            if (!string.IsNullOrWhiteSpace(serviceLine.ProcedureModifier1))
//            {
//                if (!IsValidModifierForProcedure(serviceLine.ProcedureCode, serviceLine.ProcedureModifier1))
//                {
//                    AddWarning(validationResult, $"Modifier {serviceLine.ProcedureModifier1} may not be valid for procedure code {serviceLine.ProcedureCode} in claim {claim.ClaimInfo.ClaimId}");
//                }
//            }
//        }

//        private bool IsValidModifierForProcedure(string procedureCode, string modifier)
//        {
//            // This method should contain logic to check if the modifier is valid for the given procedure code
//            // You might want to use a lookup table or external service for this check
//            // For now, we'll just return true as a placeholder
//            return true;
//        }
//    }
//}