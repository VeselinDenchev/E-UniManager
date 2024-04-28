using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Domain.Entities.Students;

public class IdCard : BaseEntity<Guid>
{
    public Student Student { get; set; }
    
    public string IdNumber { get; set; }
    
    public DateOnly DateIssued { get; set; }
}