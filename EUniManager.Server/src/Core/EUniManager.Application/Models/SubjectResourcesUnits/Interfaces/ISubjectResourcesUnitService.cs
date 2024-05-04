using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.SubjectResourcesUnits.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.SubjectResourcesUnits.Interfaces;

public interface ISubjectResourcesUnitService 
    : IBaseService<SubjectResourcesUnit, Guid, SubjectResourcesUnitDto, SubjectResourcesUnitDetailsDto>;