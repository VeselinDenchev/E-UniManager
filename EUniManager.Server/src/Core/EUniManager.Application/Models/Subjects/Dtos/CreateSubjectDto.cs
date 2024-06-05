using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record CreateSubjectDto : ICreateDto
{
    public byte Semester { get; set; }
    
    public Guid CourseId { get; set; }

    public Guid LecturerId { get; set; }

    public List<Guid> AssistantIds { get; set; } = null!;

    public Guid SpecialtyId { get; set; }

    public string Protocol { get; set; } = null!;

    public string ControlType { get; set; } = null!;
}