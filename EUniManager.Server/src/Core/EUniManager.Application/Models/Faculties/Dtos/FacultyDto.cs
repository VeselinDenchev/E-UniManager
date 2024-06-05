using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Faculties.Dtos;

public sealed record FacultyDto : IEntityDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
}