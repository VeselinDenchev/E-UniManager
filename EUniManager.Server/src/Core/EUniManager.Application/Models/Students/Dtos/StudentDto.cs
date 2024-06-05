using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Students.Dtos;

public sealed record StudentDto : IEntityDto
{
    public required Guid Id { get; set; }
    
    public required long Pin { get; set; }
    
    public required int FacultyNumber { get; set; }

    public required string FullName { get; set; } = null!;
    
    public required string FacultyName { get; set; } = null!;
    
    public required string SpecialtyName { get; set; } = null!;

    public required string StudentStatus { get; set; } = null!;

    public required byte? EnrolledInSemester { get; set; }
}