using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Activities.Dtos;

public sealed record ActivityDto : IEntityDto
{
    public required Guid Id { get; set; }

    public required string CreatedAt { get; set; } = null!;

    public required byte Semester { get; set; }
    
    public required string ActivityType { get; set; } = null!;

    public required string TeacherFullNameWithRank { get; set; } = null!;

    public required string SubjectCourseName { get; set; } = null!;

    public required bool IsStopped { get; set; }
}