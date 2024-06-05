using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record UpdateSubjectDto : IUpdateDto
{
    public Guid LecturerId { get; set; }

    public List<Guid> AssistantIds { get; set; } = null!;
}