using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Exams.Dtos;

public sealed record CreateExamDto : ICreateDto
{
    public Guid SubjectId { get; set; }

    public string? ExamType { get; set; }

    public DateTime DateTime { get; set; }

    public string SchedulePlace { get; set; } = null!;

    public short RoomNumber { get; set; }

    public byte? GroupNumber { get; set; }
}