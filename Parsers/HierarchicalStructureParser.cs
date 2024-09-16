using _837ParserPOC.DataModels;
using POC837Parser.DataModels;
using POC837Parser.Parsers;
using POC837Parser.Parsers.Extractors;
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
        private readonly NameParser _nameParser; 
        private readonly AddressParser _addressParser;
        private readonly ProviderContactInformationParser _providerContactInformationParser;
        private readonly ProviderInformationParser  _providerInformationParser;
        private readonly CurrencyInformationParser _currencyParser;
        private readonly SubscriberInformationParser _subscriberInformationParser;
        private readonly SubscriberDemographicInfoParser _subscriberDemographicInfoParser;        
        private readonly PatientDemographicInfoParser _patientDemographicInfoParser;
        private readonly PatientInformationParser _patientInformationParser;
        private readonly ClaimInformationParser _claimInformationParser;
        private readonly DateParser _claimDateParser;
        private readonly ClaimLineParser _claimLineParser;
        private readonly ClaimContractParser _claimContractParser;
        private readonly NoteParser _claimNoteParser;
        private readonly AmountParser _claimAmountParser;
        //private readonly DiagnosisCodeParser _diagnosisCodeParser;
        private readonly HISegmentParser _diagnosisCodeParser;
        private readonly ServiceLineParser _serviceLineParser;
        private readonly ReferenceIdentificationParser _referenceIdentificationParser;
        private readonly ConditionsParser _conditionsParser;


        public HierarchicalStructureParser()
        {
            _hlParser = new HLParser();
            _nameParser = new NameParser();
            _addressParser = new AddressParser();
            _currencyParser = new CurrencyInformationParser();
            _providerContactInformationParser = new ProviderContactInformationParser();
            _providerInformationParser = new ProviderInformationParser();
            _subscriberInformationParser = new SubscriberInformationParser();
            _subscriberDemographicInfoParser = new SubscriberDemographicInfoParser();            
            _patientDemographicInfoParser = new PatientDemographicInfoParser();
            _patientInformationParser = new PatientInformationParser();
            _claimInformationParser = new ClaimInformationParser();
            _claimDateParser = new DateParser();
            _claimAmountParser = new AmountParser();
            //_diagnosisCodeParser = new DiagnosisCodeParser();
            _diagnosisCodeParser = new HISegmentParser();
            _serviceLineParser = new ServiceLineParser();
            _referenceIdentificationParser = new ReferenceIdentificationParser();
            _claimNoteParser = new NoteParser();
            _claimContractParser = new ClaimContractParser();
            _conditionsParser = new ConditionsParser();
            _claimLineParser = new ClaimLineParser();
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
                    case "24": // Claim (if not included in subscriber)
                        Console.WriteLine("We have a HL Claim level -24-, but no processing for it");
                        throw new InvalidOperationException($"Unexpected Claim level -24- Hierarchical Level Code: {hl.HierarchicalLevelCode}");
                    
                    default:
                        throw new InvalidOperationException($"Unexpected Hierarchical Level Code: {hl.HierarchicalLevelCode}");
                }
            }

            //// Break into 4 segments 
            //var billingProvider = new List<string>();
            //var subscriber = new List<string>();
            //var patient = new List<string>();
            //var claim = new List<string>();

            //List<string> currentList = null;

            //foreach (var line in lines)
            //{
            //    if (line.StartsWith("HL*1**20"))
            //    {
            //        currentList = billingProvider;
            //    }
            //    else if (line.StartsWith("HL*2*1*22"))
            //    {
            //        currentList = subscriber;
            //    }
            //    else if (line.StartsWith("HL*3*2*23"))
            //    {
            //        currentList = patient;
            //    }
            //    else if (line.Contains("CLM*"))
            //    {
            //        currentList = claim;
            //    }

            //    if (currentList != null)
            //    {
            //        currentList.Add(line.Trim());
            //    }
            //}


            //currentProvider = new BillingProvider { HL = hl };
            //structure.BillingProviders.Add(currentProvider);
            //ParseBillingProviderLoops(billingProvider.ToArray(), hlIndex, currentProvider);

            //currentSubscriber = new Subscriber { HL = hl };
            //currentProvider?.Subscribers.Add(currentSubscriber);
            //ParseSubscriberLoops(subscriber.ToArray(), hlIndex, currentSubscriber);


            //currentPatient = new Patient { HL = hl };
            //currentSubscriber?.Patients.Add(currentPatient);
            //ParsePatientLoops(patient.ToArray(), hlIndex, currentPatient);

            //ParseClaims(claim.ToArray(), structure);


            // Parse claims after all hierarchical levels

            var allClaims = ClaimsExtractor.ExtractClaims( lines );
            foreach (var claimLines in allClaims)
            {
                var claim = ParseClaim(claimLines);
                structure.BillingProviders.First().Subscribers.First().Claims.Add(claim);
            }

           // ParseClaims(lines, structure);

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

                    var serviceLine = _serviceLineParser.Parse(serviceLineLines);
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

        private ServiceLine ParseServiceLine(List<string> lines)
        {
            var parser = new ServiceLineParser();            
            return parser.Parse(lines); 
        }


        private Claim ParseClaim(List<string> lines)
        {
            var claim = new Claim();

            // Seperate Claim level data from Service lines
            var claimLines = new ServiceLineExtractor().ProcessClaimData(lines);

            // Process the general Claim Lines
            foreach (var line in claimLines.GeneralInfo)
            {
                if (line.StartsWith("CLM*"))
                {
                    claim.ClaimInfo = _claimInformationParser.Parse(line);
                }
                else if (line.StartsWith("DTP*434*"))
                {
                    _claimDateParser.ParseServiceDates(line, claim.ClaimInfo);
                }
                else if (line.StartsWith("AMT*"))
                {
                    claim.Amounts.Add(_claimAmountParser.Parse(line));
                }
                else if (line.StartsWith("REF*"))
                {
                    claim.AdditionalReferenceInformation.Add(_referenceIdentificationParser.Parse(line));
                }
                else if (line.StartsWith("DTP*"))
                {
                    claim.ClaimDates.Add(_claimDateParser.Parse(line));
                }
                else if (line.StartsWith("CL1*"))  // Claim Line Item
                {
                    claim.ClaimLine = _claimLineParser.Parse(line);
                    // TODO claim information line on the claim
                }
                else if (line.StartsWith("CN1*"))
                {
                    claim.ContractInfo = _claimContractParser.Parse(line);
                }
                else if (line.StartsWith("NTE*"))
                {
                    claim.ClaimNotes.Add(_claimNoteParser.Parse(line));
                }
                else if (line.StartsWith("CRC*"))
                {
                    claim.ClaimConditions.Add(_conditionsParser.Parse(line));
                }
                else if (line.StartsWith("NM1*"))
                {
                    claim.Names.Add(_nameParser.Parse(line));
                }
                else if (line.StartsWith("HI*"))
                {
                    claim.DiagnosisCodes.AddRange(_diagnosisCodeParser.Parse(line));
                    //claim.Names.Add(_nameParser.Parse(line));
                }
            }

            // A claim can have multiple service lines, parse those 
            foreach (var serviceLine in claimLines.ServiceLines)
            {
                var svLine = ParseServiceLine(serviceLine);
                claim.ServiceLines.Add(svLine);
            }

            return claim;

        }
        //private void ParseClaims(string[] lines, HierarchicalStructure structure)
        //{
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        if (lines[i].StartsWith("CLM*"))
        //        {
        //            var claim = new Claim();
        //            claim.ClaimInfo = _claimInformationParser.Parse(lines[i]);
        //            // This is a new claim

        //            // Parse claim dates
        //            i++;
        //            if (lines[i].StartsWith("DTP*434*"))
        //            {
        //                _claimDateParser.ParseServiceDates(lines[i], claim.ClaimInfo);
        //            }

        //            // Parse claim amounts
        //            //claim.ClaimAmount = _claimAmountParser.Parse(lines.Skip(i).TakeWhile(l => !l.StartsWith("CLM*")).ToArray());

        //            if (lines[i].StartsWith("AMT*"))
        //            {
        //                claim.Amounts.Add(_claimAmountParser.Parse(lines[i]));
        //            }



        //            // Parse diagnosis codes
        //            var diagnosisLine = Array.Find(lines.Skip(i).ToArray(), l => l.StartsWith("HI*"));
        //            if (diagnosisLine != null)
        //            {
        //                claim.DiagnosisCodes = _diagnosisCodeParser.Parse(diagnosisLine);
        //            }

        //            // Parse service lines
        //            i = ParseServiceLines(lines, i + 1, claim);

        //            // Add claim to the appropriate patient or subscriber
        //            AddClaimToStructure(claim, structure);

        //            // Move to the next claim
        //            while (i < lines.Length && !lines[i].StartsWith("CLM*"))
        //            {
        //                i++;
        //            }
        //            i--; // Adjust for the loop increment
        //        }
        //    }
        //}

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

                //if (lines[i].StartsWith("NM1*QC")) // 2010CA Patient Name
                //{
                //    patient.PatientName = _patientNameParser.Parse(lines[i]);
                //}
                if (lines[i].StartsWith("NM1*")) // Name
                {
                    patient.PatientNames.Add(_nameParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("REF*")) // 2010AA Billing Provider Secondary Identification
                {
                    patient.AdditionalReferenceInformation.Add(_referenceIdentificationParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("N3*")) // 2010CA Patient Address
                {
                    var name = patient.PatientNames.Last();  // An address is associated with the most recent Name
                    if (name != null)
                    {
                        var addressLines = new List<string> { lines[i] };
                        if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                        {
                            addressLines.Add(lines[i + 1]);
                            i++; // Skip the N4 line in the next iteration
                        }
                        name.Address = _addressParser.Parse(addressLines.ToArray());
                    }
                }
                else if (lines[i].StartsWith("DMG*")) // 2010CA Patient Demographic Information
                {
                    patient.PatientDemographicInfo = _patientDemographicInfoParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("PAT*")) // Patient Information
                {
                    patient.PatientInfo = _patientInformationParser.Parse(lines[i]);
                }
                else
                {
                    Console.WriteLine($"We do not have support for {lines[i]}");
                }
                // Add more conditions here for other segments related to the Patient
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
                
                if (lines[i].StartsWith("NM1*")) // Name
                {
                    subscriber.SubscriberNames.Add(_nameParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("REF*")) // 2010AA Billing Provider Secondary Identification
                {
                    subscriber.AdditionalReferenceInformation.Add(_referenceIdentificationParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("SBR*")) // Loop 2000B (Subscriber Info)
                {
                    subscriber.SubscriberInformation = _subscriberInformationParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("N3*")) // 2010BA Subscriber Address
                {
                    var name = subscriber.SubscriberNames.Last();  // An address is associated with the most recent Name
                    if (name != null)
                    {
                        var addressLines = new List<string> { lines[i] };
                        if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                        {
                            addressLines.Add(lines[i + 1]);
                            i++; // Skip the N4 line in the next iteration
                        }
                        name.Address = _addressParser.Parse(addressLines.ToArray());
                    }
                }
                else if (lines[i].StartsWith("DMG*")) // 2010BA Subscriber Demographic Information
                {
                    subscriber.SubscriberDemographicInfo = _subscriberDemographicInfoParser.Parse(lines[i]);
                }
                else
                {
                    Console.WriteLine($"We do not have support for {lines[i]}");
                }

                // Add more conditions here for other segments related to the Subscriber
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

                if (lines[i].StartsWith("NM1")) //  Billing Provider Name
                {
                    provider.BillingProviderNames.Add(_nameParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("N3*")) 
                {                    
                    var name = provider.BillingProviderNames.Last();  // An address is associated with the most recent Name
                    if (name != null)
                    {
                        var addressLines = new List<string> { lines[i] };
                        if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                        {
                            addressLines.Add(lines[i + 1]);
                            i++; // Skip the N4 line in the next iteration
                        }
                        name.Address = _addressParser.Parse(addressLines.ToArray());
                    }
                }
                else if (lines[i].StartsWith("REF*")) // 2010AA Billing Provider Secondary Identification
                {          
                    provider.AdditionalReferenceInformation.Add(_referenceIdentificationParser.Parse(lines[i]));
                }
                else if (lines[i].StartsWith("PER*")) // 2010AA Billing Provider Contact Information
                {
                    provider.ProviderContactInformation = _providerContactInformationParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("CUR*")) // Currency 
                {
                    provider.ProviderCurrencyInformation= _currencyParser.Parse(lines[i]);
                }
                else if (lines[i].StartsWith("PRV*"))  
                {
                    provider.ProviderInformation.Add (_providerInformationParser.Parse(lines[i]));
                }

                //else if (lines[i].StartsWith("N3*") && provider.PayToAddress == null) // 2010AB Pay-to Address
                //{
                //    var addressLines = new List<string> { lines[i] };
                //    if (i + 1 < lines.Length && lines[i + 1].StartsWith("N4*"))
                //    {
                //        addressLines.Add(lines[i + 1]);
                //        i++; // Skip the N4 line in the next iteration
                //    }
                //    provider.PayToAddress = _payToAddressParser.Parse(addressLines.ToArray());
                //}    
                else
                {
                    Console.WriteLine($"We do not have support for {lines[i]}");
                }
                // Add more conditions here for other segments related to the Billing Provider
            }
        }
       
    }
}
