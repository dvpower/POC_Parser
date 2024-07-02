public class EDI837Preprocessor
{
    private char _segmentTerminator = '~';
    private char _elementSeparator = '*';

    public string[] PreprocessEDI837(string rawContent)
    {
        // Split the content into lines
        string[] lines = rawContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        // Find the ISA segment
        int isaIndex = Array.FindIndex(lines, line => line.StartsWith("ISA"));

        if (isaIndex == -1)
        {
            throw new InvalidOperationException("Invalid EDI file: ISA segment not found.");
        }

        // Extract the ISA segment and set separators
        string isaSegment = lines[isaIndex];
        _segmentTerminator = isaSegment[isaSegment.Length - 1];
        _elementSeparator = isaSegment[3];

        // Join all lines from ISA onwards
        string relevantContent = string.Join("", lines.Skip(isaIndex));

        // Remove any remaining line breaks
        relevantContent = relevantContent.Replace("\r", "").Replace("\n", "");

        // Split the content into segments
        string[] segments = relevantContent.Split(_segmentTerminator);

        // Process each segment
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = segments[i].Trim() + _segmentTerminator;
        }

        return segments.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
    }

    public char GetSegmentTerminator() => _segmentTerminator;
    public char GetElementSeparator() => _elementSeparator;
}