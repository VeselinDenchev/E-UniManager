using EUniManager.Domain.Abstraction.Base.Interfaces;

namespace EUniManager.Domain.Abstraction.Base;

public abstract class BaseEntity<TId> : IIdentity<TId>, IAuditable where TId : IEquatable<TId>
{
    public required TId Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
}