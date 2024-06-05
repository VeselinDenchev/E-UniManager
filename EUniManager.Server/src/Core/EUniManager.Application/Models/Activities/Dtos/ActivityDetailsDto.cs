using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Activities.Dtos;

public sealed record ActivityDetailsDto : IDetailsDto
{
    public string ActivityType { get; set; } = null!;

    public string TeacherFullNameWithRank { get; set; } = null!;

    public string SubjectCourseName { get; set; } = null!;

    public bool IsStopped { get; set; }
}