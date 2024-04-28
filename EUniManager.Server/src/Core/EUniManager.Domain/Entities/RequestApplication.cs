using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class RequestApplication : BaseEntity<Guid>
{
    public int Number { get; set; }
    
    public Student Student { get; set; } = null!;

    public RequestApplicationType Type { get; set; }
    
    public DateTime RegistryDate { get; set; }

    public DateTime? ResolutionDate { get; set; }

    public CloudinaryFile File { get; set; } = null!;
}