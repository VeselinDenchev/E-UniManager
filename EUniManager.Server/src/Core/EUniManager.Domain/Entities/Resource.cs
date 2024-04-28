using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Assignments;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Resource : BaseEntity<Guid>
{
    public string Title { get; set; } = null!;

    public ResourceType Type { get; set; }

    public CloudinaryFile File { get; set; } = null!;

    public CurriculumSubjectResourcesUnit CurriculumSubjectResourcesUnit { get; set; }

    public List<Assignment> Assignments { get; set; } = null!;
}