using System.Text;

using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Specialties.Dtos;

public sealed record SpecialtyDto : IEntityDto
{
    public Guid Id { get; set; }
    
    public string FacultyName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EducationalAndQualificationDegree { get; set; } = null!;

    public int FirstAcademicYearStart { get; set; }

    public byte CurrentYear { get; set; } // Default value 1
}