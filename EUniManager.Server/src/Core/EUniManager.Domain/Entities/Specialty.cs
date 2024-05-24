using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Specialty : BaseEntity<Guid>
{
    public Faculty Faculty { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<Student>? Students { get; set; }

    public int FirstAcademicYearStart { get; set; }

    public byte CurrentYear { get; set; } // Default value 1

    public int CurrentAcademicYearStart => FirstAcademicYearStart + CurrentYear - 1;
    
    public List<Subject> Subjects { get; set; } = null!;

    public bool HasGraduated { get; set; }

    public SpecialtyEducationType EducationType { get; set; }
}