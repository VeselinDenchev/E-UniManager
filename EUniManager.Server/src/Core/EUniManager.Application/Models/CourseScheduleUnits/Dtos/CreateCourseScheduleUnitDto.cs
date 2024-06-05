using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.CourseScheduleUnits.Dtos;

public sealed record CreateCourseScheduleUnitDto : ICreateDto
{
    public string? DayOfWeek { get; set; }

    public DateOnly? ExactDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public string? ScheduleMonthlyFrequency { get; set; }

    public string? GroupType { get; set; }

    public byte? GroupNumber { get; set; }

    public short RoomNumber { get; set; }

    public string SchedulePlace { get; set; } = null!;

    public Guid ActivityId { get; set; }
}