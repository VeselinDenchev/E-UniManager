using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Subjects.Dtos;

namespace EUniManager.Application.Models.Teachers.Dtos;

public sealed class TeacherDetailsDto : IDetailsDto
{
    public Guid Id { get; init; }
    
    public string Email { get; set; } = null!;
    
    public string? Rank { get; init; }
    
    public string FirstName { get; init; } = null!;

    public string MiddleName { get; init; } = null!;
    
    public string LastName { get; init; } = null!;


    public List<CurrentYearSubjectDto> LecturingSubjects { get; set; } = new();

    public List<CurrentYearSubjectDto> AssistingSubjects { get; set; } = new();
}