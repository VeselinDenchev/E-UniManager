namespace EUniManager.Application.Models.Students.Dtos;

public sealed record ServiceDataDto
{
    public long Pin { get; set; }

    public string StudentStatus { get; set; } = null!;

    public int PlanNumber { get; set; }

    public int FacultyNumber { get; set; }

    public byte GroupNumber { get; set; }

    public byte? EnrolledInSemester { get; set; }
}