namespace ExpressCargo.Core.Extensions
{
    public static class DateTimeUtility
    {
        static readonly CultureInfo format = CultureInfo.InvariantCulture;
        public static DateTimeOffset ToIst(this DateTimeOffset _)
        {
            return _.ToUniversalTime().AddHours(5).AddMinutes(30);
        }
        public static string ToFullDateString(this DateTimeOffset _)
        {
            return _.ToIst().ToString("dddd, dd MMM yyyy, hh:mm:ss tt", format);
        }
        public static string ToTimeString(this DateTimeOffset _)
        {
            return _.ToIst().ToString("hh:mm:ss tt", format);
        }
        public static string ToDateTimeString(this DateTimeOffset _)
        {
            return _.ToIst().ToString("dd MMM yyyy, hh:mm:ss tt", format);
        }
        public static string ToDateString(this DateTimeOffset _)
        {
            return _.ToIst().ToString("dd MMM yyyy", format);
        }
        public static string ToString_MMM_YYYY(this DateTime _)
        {
            return _.ToString("MMM yy", format);
        }
    }
}
