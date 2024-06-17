using EUniManager.Application.Models.AssigmentSolutions.Dtos;

namespace EUniManager.Application.Models.Assignments.Dtos;

public sealed record AssignmentWithSolutionDto
{
    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string StartDate { get; set; } = null!;

    public string DueDate { get; set; } = null!;
    
    public string? Description { get; set; }

    public AssignmentSolutionDetailsDto Solution { get; set; } = null!;
}