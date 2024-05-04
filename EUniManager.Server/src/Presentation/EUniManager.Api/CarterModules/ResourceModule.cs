using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Application.Models.Resources.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class ResourceModule()
    : CrudCarterModule<
        IResourceService, 
        Resource, ResourceDto,
        ResourceDetailsDto,
        CreateResourceDto,
        UpdateResourceDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Resource).ToLowerInvariant()));