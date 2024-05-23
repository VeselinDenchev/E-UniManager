using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class IndividualProtocol : BaseEntity<Guid>
{
    public Student Student { get; set; } = null!;
    
    public Subject Subject { get; set; } = null!;

    public IndividualProtocolStatus Status { get; set; }
}