using EUniManager.Domain.Entities;

namespace EUniManager.Application.Extensions;

public static class CourseScheduleUnitExtensions
{
    // public static IQueryable<CourseScheduleUnit> OrderSchedule(this IQueryable<CourseScheduleUnit> courseScheduleUnits)
    // {
    //     return courseScheduleUnits.OrderBy(csu => csu.DayOfWeek).ThenBy(csu => csu.StartTime)
    //                                                             .ThenBy(csu => csu.GroupType)
    //                                                             .ThenBy(csu => csu.GroupNumber);
    // }
    
    public static List<CourseScheduleUnit> ToScheduleOrderedList(this IEnumerable<CourseScheduleUnit> courseScheduleUnits)
    {
        return courseScheduleUnits.OrderBy(csu => csu.DayOfWeek).ThenBy(csu => csu.StartTime)
                                                                .ThenBy(csu => csu.GroupType)
                                                                .ThenBy(csu => csu.GroupNumber)
                                  .ToList();
    }
}