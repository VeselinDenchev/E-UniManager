using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Specialties.Dtos;

public sealed record CreateSpecialtyDto : ICreateDto
{
    public Guid FacultyId { get; set; }

    public string Name { get; set; } = null!;

    public int FirstAcademicYearStart { get; set; }

    public byte CurrentYear { get; set; } // Default value 1

    public string EducationType { get; set; } = null!;

    public string EducationalAndQualificationDegree { get; set; } = null!;
}