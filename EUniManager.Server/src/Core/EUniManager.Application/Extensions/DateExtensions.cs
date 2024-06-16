using System.Globalization;

namespace EUniManager.Application.Extensions;

public static class DateExtensions
{
    public const string BULGARIAN_DATE_FORMAT = "dd.MM.yyyy г.";
    
    public static string ToBulgarianDateFormatString(this DateOnly date)
        => date.ToString(BULGARIAN_DATE_FORMAT, CultureInfo.GetCultureInfo("bg-BG"));
}