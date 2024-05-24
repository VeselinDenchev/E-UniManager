using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Domain.Entities;

public class Faculty : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public List<Specialty> Specialties { get; set; } = null!;
}