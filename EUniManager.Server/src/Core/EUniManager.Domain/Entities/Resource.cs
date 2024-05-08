using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Resource : BaseEntity<Guid>
{
    public string Title { get; set; } = null!;

    public ResourceType Type { get; set; }

    public CloudinaryFile? File { get; set; }

    public SubjectResourcesUnit SubjectResourcesUnit { get; set; } = null!;

    public List<Assignment> Assignments { get; set; } = null!;
}