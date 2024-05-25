using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Domain.Entities.Students;

public class IdCard : BaseEntity<Guid>
{
    public string IdNumber { get; set; } = null!;
    
    public DateOnly DateIssued { get; set; }
}