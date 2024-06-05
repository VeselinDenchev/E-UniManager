namespace EUniManager.Application.Models.CourseScheduleUnits.Dtos;

public sealed record StudentCourseScheduleUnitDto
{
    public required Guid Id { get; set; }

    public required string Day { get; set; } = null!;
    
    public required string Timespan { get; set; } = null!;

    public required string Week { get; set; } = null!;

    public required string? Group { get; set; }

    public required short RoomNumber { get; set; }

    public required string? SchedulePlace { get; set; }

    public required Guid ActivityId { get; set; }
    
    public required string ActivityType { get; set; } = null!;
    
    public required string ActivitySubjectCourseName { get; set; } = null!;

    public required string ActivityTeacherFullNameWithRank { get; set; } = null!;
}