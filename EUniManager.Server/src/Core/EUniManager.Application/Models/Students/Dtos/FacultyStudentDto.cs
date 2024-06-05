namespace EUniManager.Application.Models.Students.Dtos;

public sealed record FacultyStudentDto
{
    public required Guid Id { get; set; }
    
    public required long Pin { get; set; }

    public required string FullName { get; set; } = null!;
}