namespace POC837Parser.Parsers.Extractors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ClaimsExtractor
    {
        public static List<List<string>> ExtractClaims(string[] lines)
        {
            var claims = new List<List<string>>();
            var currentSubscriber = new List<string>();
            var currentClaim = new List<string>();
            bool inSubscriberBlock = false;
            bool inClaimBlock = false;

            foreach (var line in lines)
            {
                if (line.StartsWith("HL*") && line.Split('*')[3] == "22")
                {
                    // Start of a new Billing Provider (HL*2)
                    inSubscriberBlock = false;
                    inClaimBlock = false;
                    currentSubscriber.Clear();
                    currentClaim.Clear();
                }
                else if (line.StartsWith("HL*") && line.Split('*')[3] == "23")
                {
                    // Start of a new Subscriber (HL*3)
                    if (inClaimBlock && currentClaim.Any())
                    {
                        claims.Add(new List<string>(currentClaim));
                    }
                    inSubscriberBlock = true;
                    inClaimBlock = false;
                    currentSubscriber.Clear();
                    currentClaim.Clear();
                    currentSubscriber.Add(line);
                }
                else if (inSubscriberBlock)
                {
                    if (line.StartsWith("CLM*"))
                    {
                        // Start of a new claim
                        if (inClaimBlock && currentClaim.Any())
                        {
                            claims.Add(new List<string>(currentClaim));
                        }
                        inClaimBlock = true;
                        currentClaim.Clear();
                        //currentClaim.AddRange(currentSubscriber);
                        currentClaim.Add(line);
                    }
                    else if (inClaimBlock)
                    {
                        currentClaim.Add(line);
                    }
                    else
                    {
                        currentSubscriber.Add(line);
                    }
                }
            }

            // Add the last claim if there is one
            if (inClaimBlock && currentClaim.Any())
            {
                claims.Add(new List<string>(currentClaim));
            }

            return claims;
        }
    }
}
