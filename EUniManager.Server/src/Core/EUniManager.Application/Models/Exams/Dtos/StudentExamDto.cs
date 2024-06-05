namespace EUniManager.Application.Models.Exams.Dtos;

public record StudentExamDto
{
    public Guid Id { get; set; }
    
    public required string SubjectCourseName { get; set; } = null!;

    public required string? ExamType { get; set; } = null!;

    public required string ExaminerFullName { get; set; } = null!;

    public required string Date { get; set; } = null!;

    public required string Time { get; set; } = null!;

    public required string SchedulePlace { get; set; } = null!;

    public required short RoomNumber { get; set; }

    public required byte? GroupNumber { get; set; }
}