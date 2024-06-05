using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.RequestApplications.Dtos;

public record CreateRequestApplicationDto : ICreateDto
{
    public int Number { get; set; }
    
    public Guid StudentId { get; set; }

    public string RequestApplicationType { get; set; } = null!;
    
    public DateTime RegistryDate { get; set; }

    public DateTime? ResolutionDate { get; set; }

    public byte[] FileBytes { get; set; } = null!;

    public string FileMimeType { get; set; } = null!;
}