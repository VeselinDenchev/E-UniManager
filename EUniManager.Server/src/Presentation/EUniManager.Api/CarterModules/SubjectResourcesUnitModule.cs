using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.SubjectResourcesUnits.Dtos;
using EUniManager.Application.Models.SubjectResourcesUnits.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public class SubjectResourcesUnitModule()
    : CrudCarterModule<
            ISubjectResourcesUnitService,
            SubjectResourcesUnit,
            SubjectResourcesUnitDto,
            SubjectResourcesUnitDetailsDto,
            CreateSubjectResourcesUnitDto,
            UpdateSubjectResourcesUnitDto>
        (string.Format(BASE_PATH_TEMPLATE, "subject-resources-unit"));