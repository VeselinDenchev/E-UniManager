using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Domain.Entities;

public class CloudinaryFile : BaseEntity<string>
{
    public string Extension { get; set; } = null!;

    public int Version { get; set; }
}