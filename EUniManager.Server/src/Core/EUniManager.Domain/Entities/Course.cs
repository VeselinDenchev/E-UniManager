using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Course : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public byte LecturesCount { get; set; }
    
    public byte ExercisesCount { get; set; }
    
    public Mark? Mark { get; set; }

    public byte CreditsCount { get; set; }

    public string Protocol { get; set; } = null!;

    public List<Subject> Subjects { get; set; } = null!;
}