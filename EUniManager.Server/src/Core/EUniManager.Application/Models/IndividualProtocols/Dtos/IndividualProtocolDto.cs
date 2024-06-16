using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.IndividualProtocols.Dtos;

public sealed record IndividualProtocolDto : IEntityDto
{
    public required Guid Id { get; set; }

    public required string SubjectCourseName { get; set; } = null!;

    public required string Status { get; set; } = null!;

    public required string CreatedAt { get; set; }
}