using _837ParserPOC.DataModels;
using POC837Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _837ParserPOC.Parsers
{
    public class HierarchicalStructureParser
    {
        private readonly HLParser _hlParser;
        private readonly ProviderNameParser _providerNameParser;
        private readonly ProviderAddressParser _providerAddressParser;
        private readonly ProviderContactInformationParser _providerContactInformationParser;
        private readonly PayToAddressParser _payToAddressParser;
        private readonly PayToPlanNameParser _payToPlanNameParser;
        private readonly SubscriberNameParser _subscriberNameParser;
        private readonly SubscriberInformationParser _subscriberInformationParser;
        private readonly SubscriberAddressParser _subscriberAddressParser;
        private readonly SubscriberDemographicInfoParser _subscriberDemographicInfoParser;
        private readonly PayerNameParser _payerNameParser;
        private readonly PatientNameParser _patientNameParser;
        private readonly PatientAddressParser _patientAddressParser;
        private readonly PatientDemographicInfoParser _patientDemographicInfoParser;
        private readonly ClaimInformationParser _claimInformationParser;
        private readonly ClaimDateParser _claimDateParser;
        private readonly ClaimAmountParser _claimAmountParser;
        private readonly DiagnosisCodeParser _diagnosisCodeParser;
        private readonly ServiceLineParser _serviceLineParser;
        private readonly ReferenceIdentificationParser _referenceIdentificationParser;
        private readonly PayToAddressNameParser _payToAddressNameParser;




        public HierarchicalStructureParser()
        {
            _hlParser = new HLParser();
            _providerNameParser = new ProviderNameParser();
            _providerAddressParser = new ProviderAddressParser();
            _providerContactInformationParser = new ProviderContactInformationParser();
            _payToAddressParser = new PayToAddressParser();
            _payToPlanNameParser = new PayToPlanNameParser();
            _subscriberNameParser = new SubscriberNameParser();
            _subscriberInformationParser = new SubscriberInformationParser();
            _subscriberAddressParser = new SubscriberAddressParser();
            _subscriberDemographicInfoParser = new SubscriberDemographicInfoParser();
            _payerNameParser = new PayerNameParser();
            _patientNameParser = new PatientNameParser();
            _patientAddressParser = new PatientAddressParser();
            _patientDemographicInfoParser = new PatientDemographicInfoParser();
            _claimInformationParser = new ClaimInformationParser();
            _claimDateParser = new ClaimDateParser();
            _claimAmountParser = new ClaimAmountParser();
            _diagnosisCodeParser = new DiagnosisCodeParser();
            _serviceLineParser = new ServiceLineParser();
            _referenceIdentificationParser = new ReferenceIdentificationParser();
            _payToAddressNameParser = new PayToAddressNameParser();
        }

        public HierarchicalStructure Parse(string[] lines)
        {
            var structure = new HierarchicalStructure();
            var hlSegments = _hlParser.FindHLSegments(lines);

            BillingProvider currentProvider = null;
            Subscriber currentSubscriber = null;
            Patient currentPatient = null;

            for (int i = 0; i < hlSegments.Length; i++)
            {
                var hlIndex = Array.IndexOf(lines, hlSegments[i]);
                var hl = _hlParser.Parse(hlSegments[i]);

                switch (hl.HierarchicalLevelCode)
                {
                    case "20": // Billing Provider
                        currentProvider = new BillingProvider { HL = hl };
                        structure.BillingProviders.Add(currentProvider);
                        ParseBillingProviderLoops(lines, hlIndex, currentProvider);
                        break;
                    case "22": // Subscriber
                        currentSubscriber = new Subscriber { HL = hl };
                        currentProvider?.Subscribers.Add(currentSubscriber);
                        ParseSubscriberLoops(lines, hlIndex, currentSubscriber);
                        break;
                    case "23": // Patient
                        currentPatient = new Patient { HL = hl };
                        currentSubscriber?.Patients.Add(currentPatient);
                        ParsePatientLoops(lines, hlIndex, currentPatient);
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected Hierarchical Level Code: {hl.HierarchicalLevelCode}");
                }
            }

            // Parse claims after all hierarchical levels
            ParseClaims(lines, structure);

            return structure;
        }

        private int ParseServiceLines(string[] lines, int startIndex, Claim claim)
        {
            int i = startIndex;
            while (i < lines.Length && !lines[i].StartsWith("CLM*"))
            {
                if (lines[i].StartsWith("LX*"))
                {
                    var serviceLineLines = new List<string>();
                    serviceLineLines.Add(lines[i]); // Add the LX* line itself
                    i++; // Move to the next line

                    // Collect lines until the next LX* or CLM* or end of array
                    while (i < lines.Length && !lines[i].StartsWith("LX*") && !lines[i].StartsWith("CLM*"))
                    {
                        serviceLineLines.Add(lines[i]);
                        i++;
                    }

                    var serviceLine = _serviceLineParser.Parse(serviceLineLines.ToArray());
                    claim.ServiceLines.Add(serviceLine);

                    // No need to decrement i here, as we want to check the next LX* or CLM*
                }
                else
                {
                    i++; // Move to the next line if it's not an LX* line
                }
            }
            return i;
        }


        private void ParseClaims(string[] lines, HierarchicalStructure structure)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("CLM*"))
                {
                    var claim = new Claim();
                    claim.ClaimInfo = _claimInformationParser.Parse(lines[i]);

                    // Parse claim dates
                    i++;
                    if (lines[i].StartsWith("DTP*434*"))
                    {
                        _claimDateParser.ParseServiceDates(lines[i], claim.ClaimInfo);
                    }

                    // Parse claim amounts
                    claim.ClaimAmount = _claimAmountParser.Parse(lines.Skip(i).TakeWhile(l => !l.StartsWith("CLM*")).ToArray());

                    // Parse diagnosis codes
                    var diagnosisLine = Array.Find(lines.Skip(i).ToArray(), l => l.StartsWith("HI*"));
                    if (diagnosisLine != null)
                    {
                        claim.DiagnosisCodes = _diagnosisCodeParser.Parse(diagnosisLine);
                    }

                    // Parse service lines
                    i = ParseServiceLines(lines, i + 1, claim);

                    // Add claim to the appropriate patient or subscriber
                    AddClaimToStructure(claim, structure);

                    // Move to the next claim
                    while (i < lines.Length && !lines[i].StartsWith("CLM*"))
                    {
                        i++;
                    }
                    i--; // Adjust for the loop increment
                }
            }
        }

        private void AddClaimToStructure(Claim claim, HierarchicalStructure structure)
        {
            // This is a simplified version. You might need to adjust this based on your specific requirements
            foreach (var provider in structure.BillingProviders)
            {
                foreach (var subscriber in provider.Subscribers)
                {
                    if (subscriber.Patients.Any())
                    {
                        subscriber.Patients.Last().Claims.Add(claim);
                    }
                    else
                    {
                        subscriber.Claims.Add(claim);
                    }
                }
            }
        }

        private void ParsePatientLoops(string[] lines, int startIndex, Patient patient)
        {
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("HL*") || lines[i].StartsWith("CLM*")) // Stop if we hit the next HL segment or a Claim
                {
                    break;
                }

                if (lines[i].StartsWith("NM1*QC")) // 2010CA Patient Name
                {
                    patient.PatientName = _patientNameParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("N3*")) // 2010CA Patient Address
                {
                    var addressLines = new List<string> { lines[i] };
                    if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                    {
                        addressLines.Add(lines[i + 1]);
                        i++; // Skip the N4 line in the next iteration
                    }
                    patient.PatientAddress = _patientAddressParser.Parse(addressLines.ToArray());
                }
                else if (lines[i].StartsWith("DMG*")) // 2010CA Patient Demographic Information
                {
                    patient.PatientDemographicInfo = _patientDemographicInfoParser.Parse(lines[i]);
                }
                // Add more conditions here for other segments related to the Patient
            }
        }

        private void ParsePatientLoopsOLD(string[] lines, ref int currentIndex, Patient patient)
        {
            while (currentIndex < lines.Length && !lines[currentIndex].StartsWith("HL*"))
            {
                if (lines[currentIndex].StartsWith("NM1*QC")) // 2010CA
                {
                    patient.PatientName = _patientNameParser.Parse(lines[currentIndex]);
                    currentIndex++;

                    if (lines[currentIndex].StartsWith("N3*"))
                    {
                        patient.PatientAddress = _patientAddressParser.Parse(new[] { lines[currentIndex], lines[currentIndex + 1] });
                        currentIndex += 2;
                    }

                    if (lines[currentIndex].StartsWith("DMG*"))
                    {
                        patient.PatientDemographicInfo = _patientDemographicInfoParser.Parse(lines[currentIndex]);
                        currentIndex++;
                    }
                }
                else
                {
                    currentIndex++;
                }
            }
        }

        private void ParseSubscriberLoops(string[] lines, int startIndex, Subscriber subscriber)
        {
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("HL*")) // Stop if we hit the next HL segment
                {
                    break;
                }

                if (lines[i].StartsWith("NM1*IL")) // 2010BA Subscriber Name
                {
                    subscriber.SubscriberName = _subscriberNameParser.Parse(lines[i]);
                }

                else if (lines[i].StartsWith("SBR*")) // Loop 2000B (Subscriber Info)
                {
                    subscriber.SubscriberInformation = _subscriberInformationParser.Parse(lines[i]);
                }

                else if (lines[i].StartsWith("N3*")) // 2010BA Subscriber Address
                {
                    var addressLines = new List<string> { lines[i] };
                    if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                    {
                        addressLines.Add(lines[i + 1]);
                        i++; // Skip the N4 line in the next iteration
                    }
                    subscriber.SubscriberAddress = _subscriberAddressParser.Parse(addressLines.ToArray());
                }
                else if (lines[i].StartsWith("DMG*")) // 2010BA Subscriber Demographic Information
                {
                    subscriber.SubscriberDemographicInfo = _subscriberDemographicInfoParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("NM1*PR")) // 2010BB Payer Name
                {
                    subscriber.PayerName = _payerNameParser.Parse(lines[i]);
                }
                // Add more conditions here for other segments related to the Subscriber
            }
        }

        private void ParseSubscriberLoopsOLD(string[] lines, ref int currentIndex, Subscriber subscriber)
        {
            while (currentIndex < lines.Length && !lines[currentIndex].StartsWith("HL*"))
            {
                if (lines[currentIndex].StartsWith("NM1*IL")) // 2010BA
                {
                    subscriber.SubscriberName = _subscriberNameParser.Parse(lines[currentIndex]);
                    currentIndex++;

                    if (lines[currentIndex].StartsWith("N3*"))
                    {
                        subscriber.SubscriberAddress = _subscriberAddressParser.Parse(new[] { lines[currentIndex], lines[currentIndex + 1] });
                        currentIndex += 2;
                    }

                    if (lines[currentIndex].StartsWith("DMG*"))
                    {
                        subscriber.SubscriberDemographicInfo = _subscriberDemographicInfoParser.Parse(lines[currentIndex]);
                        currentIndex++;
                    }
                }
                else if (lines[currentIndex].StartsWith("NM1*PR")) // 2010BB
                {
                    subscriber.PayerName = _payerNameParser.Parse(lines[currentIndex]);
                    currentIndex++;
                }
                else
                {
                    currentIndex++;
                }
            }
        }


        private void ParseBillingProviderLoops(string[] lines, int startIndex, BillingProvider provider)
        {
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("HL*")) // Stop if we hit the next HL segment
                {
                    break;
                }

                if (lines[i].StartsWith("NM1*85")) // 2010AA Billing Provider Name
                {
                    provider.ProviderName = _providerNameParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("N3*")) // 2010AA Billing Provider Address
                {
                    var addressLines = new List<string> { lines[i] };
                    if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                    {
                        addressLines.Add(lines[i + 1]);
                        i++; // Skip the N4 line in the next iteration
                    }
                    provider.ProviderAddress = _providerAddressParser.Parse(addressLines.ToArray());
                }
                else if (lines[i].StartsWith("REF*")) // 2010AA Billing Provider Secondary Identification
                {
                    var secondaryIdentification = _referenceIdentificationParser.Parse(lines[i]);
                    provider.SecondaryIdentifications.Add(secondaryIdentification);
                }
                else if (lines[i].StartsWith("PER*")) // 2010AA Billing Provider Contact Information
                {
                    provider.ProviderContactInformation = _providerContactInformationParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("NM1*87")) // 2010AB Pay-to Address Name
                {
                    provider.PayToAddressName = _payToAddressNameParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("N3*") && provider.PayToAddress == null) // 2010AB Pay-to Address
                {
                    var addressLines = new List<string> { lines[i] };
                    if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                    {
                        addressLines.Add(lines[i + 1]);
                        i++; // Skip the N4 line in the next iteration
                    }
                    provider.PayToAddress = _payToAddressParser.Parse(addressLines.ToArray());
                }
                else if (lines[i].StartsWith("NM1*PE")) // 2010AC Pay-to Plan Name
                {
                    provider.PayToPlanName = _payToPlanNameParser.Parse(lines[i]);
                }
                // Add more conditions here for other segments related to the Billing Provider
            }
        }

        private void ParseBillingProviderLoopsOLD(string[] lines, ref int currentIndex, BillingProvider provider)
        {
            while (currentIndex < lines.Length && !lines[currentIndex].StartsWith("HL*"))
            {
                if (lines[currentIndex].StartsWith("NM1*85")) // 2010AA
                {
                    provider.ProviderName = _providerNameParser.Parse(lines[currentIndex]);
                    currentIndex++;

                    if (lines[currentIndex].StartsWith("N3*"))
                    {
                        provider.ProviderAddress = _providerAddressParser.Parse(new[] { lines[currentIndex], lines[currentIndex + 1] });
                        currentIndex += 2;
                    }

                    if (lines[currentIndex].StartsWith("PER*"))
                    {
                        provider.ProviderContactInformation = _providerContactInformationParser.Parse(lines[currentIndex]);
                        currentIndex++;
                    }
                }
                else if (lines[currentIndex].StartsWith("NM1*87")) // 2010AB
                {
                    currentIndex++;

                    if (lines[currentIndex].StartsWith("N3*"))
                    {
                        provider.PayToAddress = _payToAddressParser.Parse(new[] { lines[currentIndex], lines[currentIndex + 1] });
                        currentIndex += 2;
                    }
                }
                else if (lines[currentIndex].StartsWith("NM1*PE")) // 2010AC
                {
                    provider.PayToPlanName = _payToPlanNameParser.Parse(lines[currentIndex]);
                    currentIndex++;
                }
                else
                {
                    currentIndex++;
                }
            }
        }
    }
}
