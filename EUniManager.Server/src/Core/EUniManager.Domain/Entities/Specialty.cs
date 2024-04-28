using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Domain.Entities;

public class Specialty : BaseEntity<Guid>
{
    public Faculty Faculty { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<Student>? Students { get; set; }

    public List<Subject> Subjects { get; set; } = null!;
}