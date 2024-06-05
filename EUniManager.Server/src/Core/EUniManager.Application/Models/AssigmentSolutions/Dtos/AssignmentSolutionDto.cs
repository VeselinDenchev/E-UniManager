using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.AssigmentSolutions.Dtos;

public sealed record AssignmentSolutionDto : IEntityDto
{
    public required Guid Id { get; set; }
    
    public required int StudentFacultyNumber { get; set; }
    
    public required string StudentFullName { get; set; } = null!;

    public required string? FileId { get; set; }
    
    public required string? Text { get; set; }
    
    public required Mark? Mark { get; set; }
}