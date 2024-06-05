namespace EUniManager.Application.Models.Students.Dtos;

public sealed record EnrollmentDto
{
    public DateOnly Date { get; set; }

    public string Reason { get; set; } = null!;

    public float Mark { get; set; }
}