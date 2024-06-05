using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Courses.Dtos;

public sealed record CourseDetailsDto : IDetailsDto
{
    public string Name { get; set; } = null!;

    public byte LecturesCount { get; set; }
    
    public byte ExercisesCount { get; set; }

    public byte CreditsCount { get; set; }

    public int SubjectsCount { get; set; }
}