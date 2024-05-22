using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Domain.Entities;

public class CertifiedSemester : BaseEntity<Guid>
{
    public Student Student { get; set; } = null!;

    public byte Semester { get; set; }
}