using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Assignment : BaseEntity<Guid>
{
    public Resource Resource { get; set; } = null!;

    public Guid ResourceId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }
    
    public DateTime DueDate { get; set; }

    public AssignmentType Type { get; set; }
    
    public List<Student> Students { get; set; } = null!;
    
    public string? Description { get; set; }

    public List<AssignmentSolution> Solutions { get; set; } = null!;

    public Teacher Teacher { get; set; } = null!;
}