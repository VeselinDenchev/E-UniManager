using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Subjects.Dtos;

public sealed record SubjectDto : IEntityDto
{
    public Guid Id { get; set; }
    
    public string CourseName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;
    
    public string LecturerFullNameWithRank { get; set; } = null!;
}