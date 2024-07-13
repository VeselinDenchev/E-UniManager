using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class AssignmentSolution : BaseEntity<Guid>
{
    public Assignment Assignment { get; set; } = null!;
    
    public Student Student { get; set; } = null!;
    
    public CloudinaryFile? File { get; set; }

    public string? Text { get; set; }

    public DateTime? SeenOn { get; set; }
    
    public DateTime? SubmittedOn { get; set; }
    
    public Mark? Mark { get; set; }

    public DateTime? MarkedOn { get; set; }

    public string? Comment { get; set; }
}