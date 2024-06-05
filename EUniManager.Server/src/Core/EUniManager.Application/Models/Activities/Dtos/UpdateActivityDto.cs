using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Activities.Dtos;

public sealed record UpdateActivityDto : IUpdateDto
{
    public Guid TeacherId { get; set; }

    public bool IsStopped { get; set; }
}