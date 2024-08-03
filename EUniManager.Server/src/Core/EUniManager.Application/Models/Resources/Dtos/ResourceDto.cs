using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Dtos;

namespace EUniManager.Application.Models.Resources.Dtos;

public sealed record ResourceDto : IEntityDto
{
    public required Guid Id { get; set; }
    
    public required string? Title { get; set; }

    public required string ResourceType { get; set; } = null!;

    public required string? Info { get; set; }
    
    public FileDto? File { get; set; }

    public AssignmentDto? Assignment { get; set; }
}