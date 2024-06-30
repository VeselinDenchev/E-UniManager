using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities.Students;

public class Diploma : BaseEntity<Guid>
{
    public EducationalAndQualificationDegree EducationalAndQualificationDegree { get; set; }

    public string Series { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string RegistrationNumber { get; set; } = null!;

    public DateOnly Date { get; set; }

    public short Year { get; set; }

    public InstitutionIssuerType IssuedByInstitutionType { get; set; }

    public string InstitutionName { get; set; } = null!;

    public CityArea CityArea { get; set; } = null;

    public string Specialty { get; set; } = null;

    public string? ProfessionalQualification { get; set; }
}