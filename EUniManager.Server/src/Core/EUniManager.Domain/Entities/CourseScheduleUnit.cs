using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class CourseScheduleUnit : BaseEntity<Guid>
{
    public DayOfWeek? DayOfWeek { get; set; }

    public DateOnly? ExactDate { get; set; }
    
    public string Day => (DayOfWeek.HasValue ? DayOfWeek.Value.ToString() : ExactDate.ToString())!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime => StartTime.AddHours(2);

    public string Timespan => $"{StartTime} - {EndTime}";

    public ScheduleMonthlyFrequency? MonthlyFrequency { get; set; }

    public SubjectGroupType? GroupType { get; set; }

    public byte? GroupNumber { get; set; }

    public string? Group => GroupType is not null && GroupNumber is not null
                                ? $"{GroupNumber} {GroupType}"
                                : null;

    public short RoomNumber { get; set; }

    public SchedulePlace Place { get; set; }

    public Activity Activity { get; set; } = null!;

    public byte Semester { get; set; }

    public SemesterType SemesterType => Semester % 2 == 0 ? SemesterType.Summer : SemesterType.Winter;
}