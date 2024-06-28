using EUniManager.Domain.Enums;

namespace EUniManager.Application.Extensions;

internal static class EnumsToBulgarianStringExtensions
{
    internal static string ToBulgarianString(this ActivityType activityType)
    {
        return activityType switch
        {
            ActivityType.Lecture => "Лекция",
            ActivityType.Exercise => "Упражнение",
            _ => throw new ArgumentException("Invalid specialty activity type!")
        };
    }
    
    internal static string ToBulgarianString(this SpecialtyEducationType educationType)
    {
        return educationType switch
        {
            SpecialtyEducationType.FullTime => "Редовна",
            SpecialtyEducationType.PartTime => "Задочна",
            _ => throw new ArgumentException("Such education type doesn't exist!")
        };
    }
}