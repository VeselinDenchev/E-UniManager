using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Students.Dtos;

namespace EUniManager.Application.Models.Specialties.Dtos;

public sealed record SpecialtyDetailsDto : IDetailsDto
{
    public string FacultyName { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public string EducationalAndQualificationDegree { get; set; } = null!;
    
    public int FirstAcademicYearStart { get; set; }

    public byte CurrentYear { get; set; } // Default value 1

    public List<FacultyStudentDto>? Students { get; set; }
}