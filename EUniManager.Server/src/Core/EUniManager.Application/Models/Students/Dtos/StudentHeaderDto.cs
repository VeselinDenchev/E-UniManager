namespace EUniManager.Application.Models.Students.Dtos;

public sealed record StudentHeaderDto
{
    public long Pin { get; set; }

    public int PlanNumber { get; set; }

    public long FacultyNumber { get; set; }

    public string FullName { get; set; } = null!;

    public byte GroupNumber { get; set; }

    public string Status { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public string SpecialtyEducationType { get; set; } = null!;

    public int CertifiedSemesterCount { get; set; }

    public string UniversityEmail => $"s{Pin}@sd.e-uni.bg";
}