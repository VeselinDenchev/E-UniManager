using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.CourseScheduleUnits.Dtos;

public sealed record CourseScheduleUnitDetailsDto : IDetailsDto
{
    public required string? DayOfWeek { get; set; }

    public required string? ExactDate { get; set; }
    

    public required string StartTime { get; set; } = null!;

    public required string EndTime { get; set; } = null!;

    public required string? ScheduleMonthlyFrequency { get; set; }


    public required byte? GroupNumber { get; set; }
    
    public required string? SubjectGroupType { get; set; }

    public required short RoomNumber { get; set; }

    public required string? SchedulePlace { get; set; }

    public required string ActivityType { get; set; } = null!;
    
    public required string ActivitySubjectCourseName { get; set; } = null!;
    
    public required string ActivityTeacherFullNameWithRank { get; set; } = null!;

    public required byte Semester { get; set; }

    public required string SemesterType { get; set; } = null!;
}