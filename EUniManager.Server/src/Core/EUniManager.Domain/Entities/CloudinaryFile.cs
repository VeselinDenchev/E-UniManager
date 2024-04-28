using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Domain.Entities;

public class CloudinaryFile : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    
    public string Extension { get; set; } = null!;

    public int Version { get; set; }
}