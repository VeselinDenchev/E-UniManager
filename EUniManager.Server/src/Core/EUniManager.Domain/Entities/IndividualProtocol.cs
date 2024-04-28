using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class IndividualProtocol : BaseEntity<Guid>
{
    public Subject Subject { get; set; } = null!;

    public IndividualProtocolStatus Status { get; set; }
}