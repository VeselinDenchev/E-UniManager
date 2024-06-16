namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record StudentSubjectInfoDto
{
    public required Guid Id { get; set; }
    
    public required byte Semester { get; set; }

    public required string CourseName { get; set; } = null!;
    
    public required byte LecturesCount { get; set; }
    
    public required byte ExercisesCount { get; set; }

    public required string MarkWithWords { get; set; } = null!;
    
    public required byte? MarkNumeric { get; set; }

    public required byte CreditsCount { get; set; }

    public required string LecturerFullNameWithRank { get; set; } = null!;

    public required string Protocol { get; set; } = null!;
}