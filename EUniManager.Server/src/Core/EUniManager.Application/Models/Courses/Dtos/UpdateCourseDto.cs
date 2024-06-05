using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Courses.Dtos;

public sealed record UpdateCourseDto : IUpdateDto
{
    public byte LecturesCount { get; set; }
    
    public byte ExercisesCount { get; set; }

    public byte CreditsCount { get; set; }
}