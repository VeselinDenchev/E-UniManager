using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.IndividualProtocols.Dtos;

public sealed record CreateIndividualProtocolDto : ICreateDto
{
    public Guid StudentId { get; set; }
    
    public Guid SubjectId { get; set; }

    public string Status { get; set; } = null!;
}