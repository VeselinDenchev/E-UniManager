using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Teachers.Dtos;

public sealed record TeacherDetailsDto : IDetailsDto
{
    public Guid Id { get; set; }
    
    public string? Rank { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public List<string> LecturingSubjects { get; set; } = new();

    public List<string> AssistingSubjects { get; set; } = new();

    public List<string> Assignments { get; set; } = new();
}