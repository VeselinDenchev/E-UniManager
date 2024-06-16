using System.Globalization;

namespace EUniManager.Application.Extensions;

public static class DateTimeExtensions
{
    public static string ToBulgarianDateTimeFormatString(this DateTime dateTime)
        => dateTime.ToString("dd.M.yyyy г. HH:mm:ss", CultureInfo.GetCultureInfo("bg-BG"));
}