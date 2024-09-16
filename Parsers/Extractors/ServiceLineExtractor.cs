namespace POC837Parser.Parsers.Extractors
{

    public class ServiceLineExtractor
    {
        public class ClaimLevelData
        {
            public List<string> GeneralInfo { get; set; } = new List<string>();
            public List<List<string>> ServiceLines { get; set; } = new List<List<string>>();
        }

        public ClaimLevelData ProcessClaimData(List<string> claimSegments)
        {
            var result = new ClaimLevelData();
            var currentServiceLine = new List<string>();
            bool inServiceLine = false;

            foreach (var segment in claimSegments)
            {
                if (segment.StartsWith("LX*"))
                {
                    if (inServiceLine)
                    {
                        result.ServiceLines.Add(new List<string>(currentServiceLine));
                        currentServiceLine.Clear();
                    }
                    inServiceLine = true;
                    currentServiceLine.Add(segment);
                }
                else if (inServiceLine)
                {
                    currentServiceLine.Add(segment);
                }
                else
                {
                    result.GeneralInfo.Add(segment);
                }
            }

            // Add the last service line if there is one
            if (currentServiceLine.Any())
            {
                result.ServiceLines.Add(currentServiceLine);
            }

            return result;
        }

        public void PrintClaimLevelData(ClaimLevelData data)
        {
            Console.WriteLine("General Information:");
            foreach (var line in data.GeneralInfo)
            {
                Console.WriteLine($"  {line}");
            }

            Console.WriteLine("\nService Lines:");
            for (int i = 0; i < data.ServiceLines.Count; i++)
            {
                Console.WriteLine($"Service Line {i + 1}:");
                foreach (var line in data.ServiceLines[i])
                {
                    Console.WriteLine($"  {line}");
                }
                Console.WriteLine();
            }
        }
    }
}
