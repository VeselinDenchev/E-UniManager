namespace EUniManager.Application.Models.Activities.Dtos;

public sealed record TeacherActivityDto
{
    public required Guid Id { get; set; }

    public string SpecialtyName { get; set; } = null!;

    public string EducationalAndQualificationDegree { get; set; } = null!;

    public string EducationType { get; set; } = null!;
    
    public string FacultyName { get; set; } = null!;

    public string CourseName { get; set; } = null!;
    
    public byte Semester { get; set; }

    public string ActivityType { get; set; } = null!;

    public bool IsStopped { get; set; }
}