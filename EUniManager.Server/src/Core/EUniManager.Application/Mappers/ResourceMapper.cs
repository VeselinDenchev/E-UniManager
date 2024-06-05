using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class ResourceMapper
{
    [UserMapping]
    public List<ResourceDto> Map(List<Resource> entities)
    {
        return entities.Select(r => new ResourceDto
        {
            Id = r.Id,
            Title = r.Title,
            ResourceType = GetResourceTypeString(r.Type),
            Info = r.Info
        }).ToList();
    }
    
    [MapProperty(nameof(Resource.Type),nameof(ResourceDetailsDto.ResourceType))]
    public partial ResourceDetailsDto Map(Resource entity);
    
    [MapperIgnoreTarget(nameof(Resource.Id))]
    [MapperIgnoreTarget(nameof(Resource.CreatedAt))]
    [MapperIgnoreTarget(nameof(Resource.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Resource.File))]
    [MapProperty(nameof(CreateResourceDto.ResourceType),nameof(Resource.Type))]
    public partial Resource Map(CreateResourceDto dto);
    
    [MapperIgnoreTarget(nameof(Resource.Id))]
    [MapperIgnoreTarget(nameof(Resource.CreatedAt))]
    [MapperIgnoreTarget(nameof(Resource.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Resource.File))]
    [MapProperty(nameof(UpdateResourceDto.ResourceType),nameof(Resource.Type))]
    public partial Resource Map(UpdateResourceDto dto);

    private string GetResourceTypeString(ResourceType resourceType)
    {
        return resourceType switch
        {
            ResourceType.Info => "Информация",
            ResourceType.Assignment => "Задание",
            _ => throw new ArgumentException("Invalid resource type!")
        };
    }
}