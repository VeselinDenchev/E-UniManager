﻿using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class CourseScheduleUnit : BaseEntity<Guid>
{
    public DayOfWeek? DayOfWeek { get; set; }

    public DateOnly? ExactDate { get; set; }
    
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime => StartTime.AddHours(2);

    public ScheduleMonthlyFrequency? MonthlyFrequency { get; set; }

    public SubjectGroupType? GroupType { get; set; }

    public byte? GroupNumber { get; set; }

    public short RoomNumber { get; set; }

    public SchedulePlace Place { get; set; }

    public Activity Activity { get; set; } = null!;

    public SemesterType SemesterType => Activity.Subject.Semester % 2 == 0 ? SemesterType.Summer : SemesterType.Winter;
}