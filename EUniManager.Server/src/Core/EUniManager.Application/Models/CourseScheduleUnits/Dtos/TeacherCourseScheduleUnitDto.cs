namespace EUniManager.Application.Models.CourseScheduleUnits.Dtos;

public sealed record TeacherCourseScheduleUnitDto
{
    public required  Guid Id { get; set; }
    
    public required string Day { get; set; } = null!;
    
    public required string Timespan { get; set; } = null!;

    public required string Week { get; set; } = null!;

    public required string? SchedulePlace { get; set; }
    
    public required short RoomNumber { get; set; }
    
    public required string SpecialtyName { get; set; }

    public required int SpecialtyCurrentYear { get; set; }

    public string? Group { get; set; }
    
    public required string ActivitySubjectCourseName { get; set; } = null!;
    
    public required string ActivityType { get; set; } = null!;

    public required string SpecialtyEducationType { get; set; } = null!;

    public required string ActivitySubjectControlType { get; set; } = null!;
}