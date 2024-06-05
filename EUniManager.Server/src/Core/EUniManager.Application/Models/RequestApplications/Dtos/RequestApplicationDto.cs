using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.RequestApplications.Dtos;

public sealed record RequestApplicationDto : IEntityDto
{
    public required int Number { get; set; }
    
    public required long StudentPin { get; set; }

    public required string RequestApplicationType { get; set; } = null!;

    public required string RegistryDate { get; set; } = null!;

    public required string? ResolutionDate { get; set; }

    public required string FileId { get; set; } = null!;
}