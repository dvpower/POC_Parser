using System;
using System.Globalization;

public static class EDIParserHelper
{
    public static DateTime ParseDate(string dateString)
    {

        string[] formats = { "yyMMdd", "yyyyMMdd" };


        return DateTime.ParseExact(dateString, formats, CultureInfo.InvariantCulture);
    }

    public static TimeSpan ParseTime(string timeString)
    {
        string[] formats = { "HHmmss", "HHmm", "HHmmssff" };
        DateTime parsedTime = DateTime.ParseExact(timeString, formats, CultureInfo.InvariantCulture);
        return parsedTime.TimeOfDay;
    }

    public static string[] SplitSegment(string segment)
    {
        return segment.Split('*');
    }

    public static string FindSegment(string[] lines, string segmentId)
    {
        return lines.FirstOrDefault(l => l.StartsWith(segmentId));
    }
}
