using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Specialties.Dtos;

namespace EUniManager.Application.Models.Faculties.Dtos;

public sealed record FacultyDetailsDto : IDetailsDto
{
    public string Name { get; set; } = null!;

    public List<FacultySpecialtyDto> Specialties { get; set; } = null!;
}