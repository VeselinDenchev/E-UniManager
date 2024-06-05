using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Resources.Dtos;

public sealed record ResourceDetailsDto : IDetailsDto
{
    public string Title { get; set; } = null!;

    public string ResourceType { get; set; } = null!;
    
    public string? Info { get; set; }

    public string? FileId { get; set; }
}