namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record CurrentYearSubjectDto
{
    public Guid Id { get; set; }
    
    public required string SpecialtyName { get; set; } = null!;

    public required string SubjectCourseName { get; set; } = null!;

    public required byte Semester { get; set; }
}