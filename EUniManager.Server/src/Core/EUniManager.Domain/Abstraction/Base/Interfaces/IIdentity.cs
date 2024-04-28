namespace EUniManager.Domain.Abstraction.Base.Interfaces;

public interface IIdentity<TId> where TId : IEquatable<TId>
{
    public TId Id { get; set; }
}