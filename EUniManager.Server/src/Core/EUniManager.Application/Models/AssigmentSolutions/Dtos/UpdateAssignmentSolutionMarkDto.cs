using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.AssigmentSolutions.Dtos;

public sealed record UpdateAssignmentSolutionMarkDto
{
    public Mark Mark { get; set; }

    public string? Comment { get; set; }
}