namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record StudentSubjectInfoDto
{
    public byte Semester { get; set; }

    public string CourseName { get; set; } = null!;
    
    public byte LecturesCount { get; set; }
    
    public byte ExercisesCount { get; set; }

    public string MarkWithWords { get; set; } = null!;
    
    public byte? MarkNumeric { get; set; }

    public byte CreditsCount { get; set; }

    public string LecturerFullNameWithRank { get; set; } = null!;

    public string Protocol { get; set; } = null!;
}