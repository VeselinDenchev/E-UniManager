using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record SubjectDetailsDto : IDetailsDto
{
    public Guid Id { get; set; }
    
    public string CourseName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;
    
    public string LecturerFullNameWithRank { get; set; } = null!;
    
    public List<string> AssistantFullNamesWithRank { get; set; } = null!;

    public List<Guid> ActivityIds { get; set; } = null!;

    public string Protocol { get; set; } = null!;
}