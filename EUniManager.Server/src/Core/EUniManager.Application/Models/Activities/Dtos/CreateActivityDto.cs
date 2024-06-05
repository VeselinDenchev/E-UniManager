using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Activities.Dtos;

public sealed record CreateActivityDto : ICreateDto
{
    public string ActivityType { get; set; } = null!;

    public Guid TeacherId { get; set; }

    public Guid SubjectId { get; set; }

    public List<Guid> StudentIds { get; set; } = new();

    public bool IsStopped { get; set; }
}