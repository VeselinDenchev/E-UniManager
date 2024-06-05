using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Exams.Dtos;

public sealed record ExamDetailsDto : IDetailsDto
{
    public string SubjectCourseName { get; set; } = null!;

    public string? ExamType { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string Time { get; set; } = null!;

    public string SchedulePlace { get; set; } = null!;

    public short RoomNumber { get; set; }

    public byte? GroupNumber { get; set; }
}