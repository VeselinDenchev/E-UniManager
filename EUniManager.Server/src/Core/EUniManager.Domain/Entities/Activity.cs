using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Activity : BaseEntity<Guid>
{
    public ActivityType Type { get; set; }

    public Teacher Teacher { get; set; } = null!;

    public List<Student> Students { get; set; } = null!;

    public Subject Subject { get; set; } = null!;

    public bool IsStopped { get; set; }
}