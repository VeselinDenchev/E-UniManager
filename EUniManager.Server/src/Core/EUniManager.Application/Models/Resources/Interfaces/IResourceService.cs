using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Resources.Interfaces;

public interface IResourceService : IBaseService<Resource, Guid, ResourceDto, ResourceDetailsDto>;