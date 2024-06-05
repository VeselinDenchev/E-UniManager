namespace EUniManager.Application.Models.Students.Dtos;

public sealed record DiplomaDto
{
    public string EducationalAndQualificationDegree { get; set; } = null!;

    public string Series { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string RegistrationNumber { get; set; } = null!;

    public DateOnly Date { get; set; }

    public short Year { get; set; }

    public string IssuedByInstitutionType { get; set; } = null!;

    public string InstitutionName { get; set; } = null!;

    public CityAreaDto CityArea { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public string? ProfessionalQualification { get; set; }
}