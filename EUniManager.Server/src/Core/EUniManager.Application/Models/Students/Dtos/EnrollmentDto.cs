namespace EUniManager.Application.Models.Students.Dtos;

public sealed record EnrollmentDto
{
    public string Date { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public float Mark { get; set; }
}