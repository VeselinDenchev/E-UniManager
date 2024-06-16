using EUniManager.Application.Extensions;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class RequestApplicationMapper
{
    public List<RequestApplicationDto> Map(List<RequestApplication> entities)
    {
        return entities.Select(ra => new RequestApplicationDto
        {
            Id = ra.Id,
            Number = ra.Number,
            StudentPin = ra.Student.ServiceData.Pin,
            RequestApplicationType = GetRequestApplicationTypeString(ra.Type),
            RegistryDate = ra.RegistryDate.ToBulgarianDateTimeFormatString(),
            ResolutionDate = ra.ResolutionDate?.ToBulgarianDateTimeFormatString(),
            FileId = ra.File.Id
        }).ToList();
    }
    
    [MapperIgnoreTarget(nameof(RequestApplication.Id))]
    [MapperIgnoreTarget(nameof(RequestApplication.CreatedAt))]
    [MapperIgnoreTarget(nameof(RequestApplication.ModifiedAt))]
    [MapperIgnoreTarget(nameof(RequestApplication.Student))]
    [MapProperty(nameof(CreateRequestApplicationDto.RequestApplicationType), nameof(RequestApplication.Type))]
    public partial RequestApplication Map(CreateRequestApplicationDto dto);

    private string GetRequestApplicationTypeString(RequestApplicationType requestApplicationType)
    {
        return requestApplicationType switch
        {
            RequestApplicationType.Universal => "Универсална",
            RequestApplicationType.ModularEducation => "Модулно обучение",
            _ => throw new ArgumentException("Invalid request application type!")
        };
    }
}