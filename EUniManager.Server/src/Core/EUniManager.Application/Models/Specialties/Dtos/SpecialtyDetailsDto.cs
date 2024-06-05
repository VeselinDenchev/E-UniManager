using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Students.Dtos;

namespace EUniManager.Application.Models.Specialties.Dtos;

public sealed record SpecialtyDetailsDto : IDetailsDto
{
    public string FacultyName { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;
    
    public int FirstAcademicYearStart { get; set; }

    public byte CurrentYear { get; set; } // Default value 1

    public List<FacultyStudentDto>? Students { get; set; }
}