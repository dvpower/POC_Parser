namespace POC837Parser.DataModels
{
    public class DateOrTimeElement
    {
        public string DateTimeQualifier { get; set; }
        public string DateTimeQualifierDescription { get; set; }
        
        public string DateTimePeriodFormatQualifier { get; set; }
        public string DateTimePeriod { get; set; }

        // Helper property to parse the date(s) based on the format qualifier
        public DateTimeRange ParsedDateTimeRange => ParseDateTimePeriod();

        private DateTimeRange ParseDateTimePeriod()
        {
            switch (DateTimePeriodFormatQualifier)
            {
                case "D8":  // CCYYMMDD
                    return new DateTimeRange { Start = DateTime.ParseExact(DateTimePeriod, "yyyyMMdd", null) };
                case "D6":  // CCYYMM
                    return new DateTimeRange { Start = DateTime.ParseExact(DateTimePeriod + "01", "yyyyMMdd", null) };
                case "RD8": // CCYYMMDD-CCYYMMDD
                    var dates = DateTimePeriod.Split('-');
                    return new DateTimeRange
                    {
                        Start = DateTime.ParseExact(dates[0], "yyyyMMdd", null),
                        End = DateTime.ParseExact(dates[1], "yyyyMMdd", null)
                    };
                case "DT":  // CCYYMMDDHHMM
                    return new DateTimeRange { Start = DateTime.ParseExact(DateTimePeriod, "yyyyMMddHHmm", null) };
                case "TM":  // HHMM
                    var now = DateTime.Now;
                    var time = DateTime.ParseExact(DateTimePeriod, "HHmm", null);
                    return new DateTimeRange { Start = new DateTime(now.Year, now.Month, now.Day, time.Hour, time.Minute, 0) };
                case "TQ":  // HHMM-HHMM
                    var times = DateTimePeriod.Split('-');
                    var startTime = DateTime.ParseExact(times[0], "HHmm", null);
                    var endTime = DateTime.ParseExact(times[1], "HHmm", null);
                    var today = DateTime.Now.Date;
                    return new DateTimeRange
                    {
                        Start = today.Add(startTime.TimeOfDay),
                        End = today.Add(endTime.TimeOfDay)
                    };
                case "RD6": // CCYYMM-CCYYMM
                    var months = DateTimePeriod.Split('-');
                    return new DateTimeRange
                    {
                        Start = DateTime.ParseExact(months[0] + "01", "yyyyMMdd", null),
                        End = DateTime.ParseExact(months[1] + "01", "yyyyMMdd", null).AddMonths(1).AddDays(-1)
                    };
                // Add more cases as needed
                default:
                    throw new NotImplementedException($"Format {DateTimePeriodFormatQualifier} not implemented");
            }
        }
    }

    public class DateTimeRange
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
