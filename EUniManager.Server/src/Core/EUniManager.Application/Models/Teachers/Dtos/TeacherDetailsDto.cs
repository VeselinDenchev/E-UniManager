using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Teachers.Dtos;

public sealed class TeacherDetailsDto : IDetailsDto
{
    public Guid Id { get; init; }
    
    public string? Rank { get; init; }
    
    public string FirstName { get; init; } = null!;

    public string MiddleName { get; init; } = null!;
    
    public string LastName { get; init; } = null!;

    public List<string> LecturingSubjects { get; set; } = new();

    public List<string> AssistingSubjects { get; set; } = new();

    public List<string> Assignments { get; set; } = new();
}