using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Resource : BaseEntity<Guid>
{
    public string Title { get; set; } = null!;

    public ResourceType Type { get; set; }

    public CloudinaryFile? File { get; set; }

    public Activity Activity { get; set; } = null!;

    public Assignment? Assignment { get; set; }
}