namespace EUniManager.Domain.Abstraction.Base.Interfaces;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}