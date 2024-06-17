using System.Globalization;

namespace EUniManager.Application.Extensions;

public static class DateTimeExtensions
{
    public const string BULGARIAN_DATE_TIME_FORMAT = "dd.M.yyyy г. HH:mm:ss ч.";

    public static string ToBulgarianDateTimeFormatString(this DateTime dateTime)
    {
        TimeZoneInfo bulgarianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

        // Convert UTC time to Bulgarian time
        DateTime bulgarianTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, bulgarianTimeZone);
        
        return bulgarianTime.ToString(BULGARIAN_DATE_TIME_FORMAT, CultureInfo.GetCultureInfo("bg-BG"));
    }
}