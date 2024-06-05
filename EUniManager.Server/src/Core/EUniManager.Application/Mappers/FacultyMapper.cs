using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class FacultyMapper
{
    public partial List<FacultyDto> Map(List<Faculty> entities);
    
    public partial FacultyDetailsDto Map(Faculty entity);
    
    [MapperIgnoreTarget(nameof(Faculty.Id))]
    [MapperIgnoreTarget(nameof(Faculty.CreatedAt))]
    [MapperIgnoreTarget(nameof(Faculty.ModifiedAt))]
    // [MapperIgnoreTarget(nameof(Faculty.Students))]
    public partial Faculty Map(ManageFacultyDto dto);
}