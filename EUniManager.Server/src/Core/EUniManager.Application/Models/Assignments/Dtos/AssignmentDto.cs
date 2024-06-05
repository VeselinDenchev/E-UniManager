using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Assignments.Dtos;

public record AssignmentDto : IEntityDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public string? Description { get; set; }
}