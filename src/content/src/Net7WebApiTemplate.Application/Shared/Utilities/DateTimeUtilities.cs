namespace Net7WebApiTemplate.Application.Shared.Utilities
{
    public static class DateTimeUtilities
    {
        public static DateTime ConvertDatFromatUtc(DateTime dateTimeUtc, string localTimeZone)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, timeZone);
        }
    }

    public class TimeZones
    {
        public string Value { get; set; }

        private TimeZones(string values)
        {
            Value = values;
        }

        public static TimeZones CST { get { return new TimeZones("Central Standard Time"); } }
        public static TimeZones EST { get { return new TimeZones("Eastern Standard Time"); } }
    }
}