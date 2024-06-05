using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Faculties.Dtos;

public sealed record ManageFacultyDto : ICreateDto, IUpdateDto
{
    public string Name { get; set; }
}