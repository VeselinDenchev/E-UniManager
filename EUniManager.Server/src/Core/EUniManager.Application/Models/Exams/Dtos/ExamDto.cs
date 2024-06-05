using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Exams.Dtos;

public sealed record ExamDto : IEntityDto
{
    public required Guid Id { get; set; }
    
    public required string SubjectCourseName { get; set; } = null!;

    public required string SpecialtyName { get; set; } = null!;

    public required string ExamType { get; set; }

    public required string Date { get; set; } = null!;

    public required string Time { get; set; } = null!;

    public required string SchedulePlace { get; set; } = null!;

    public required short RoomNumber { get; set; }

    public required byte? GroupNumber { get; set; }
}