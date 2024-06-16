namespace EUniManager.Application.Models.CertifiedSemesters.Dtos;

public sealed record StudentCertifiedSemesterDto
{
    public required Guid Id { get; set; }

    public byte Semester { get; set; }

    public required long StudentPin { get; set; }

    public required string EducationType { get; set; } = null!;

    public required string Date { get; set; } = null!;
}