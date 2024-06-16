using System.Globalization;

namespace EUniManager.Application.Extensions;

public static class DateExtensions
{
    public static string ToBulgarianDateFormatString(this DateOnly date)
        => date.ToString("dd.MM.yyyy г.", CultureInfo.GetCultureInfo("bg-BG"));
}