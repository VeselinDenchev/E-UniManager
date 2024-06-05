using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class IndividualProtocolMapper
{
    [UserMapping]
    public List<IndividualProtocolDto> Map(List<IndividualProtocol> entities)
    {
        return entities.Select(ip => new IndividualProtocolDto
        {
            Id = ip.Id,
            SubjectCourseName = ip.Subject.Course.Name,
            Status = GetIndividualProtocolStatusString(ip.Status),
            CreatedAt = ip.CreatedAt
        }).ToList();
    }
    
    [MapProperty(nameof(@IndividualProtocol.Subject.Course.Name), nameof(IndividualProtocolDetailsDto.SubjectCourseName))]
    [MapProperty(nameof(IndividualProtocol.Status), nameof(IndividualProtocolDetailsDto.Status), Use = nameof(GetIndividualProtocolStatusString))]
    public partial IndividualProtocolDetailsDto Map(IndividualProtocol entity);
    
    [MapperIgnoreTarget(nameof(IndividualProtocol.Id))]
    [MapperIgnoreTarget(nameof(IndividualProtocol.CreatedAt))]
    [MapperIgnoreTarget(nameof(IndividualProtocol.ModifiedAt))]
    public partial IndividualProtocol Map(CreateIndividualProtocolDto dto);

    private string GetIndividualProtocolStatusString(IndividualProtocolStatus status)
    {
        return status switch
        {
            IndividualProtocolStatus.Pending => "Нерешен",
            IndividualProtocolStatus.Active => "Активен",
            IndividualProtocolStatus.Inactive => "Неактивен",
            IndividualProtocolStatus.Used => "Използван",
            _ => throw new ArgumentException("Ïnvalid individual protocol status!")
        };
    }
}