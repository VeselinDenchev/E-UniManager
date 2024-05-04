using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Application.Models.Specialties.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class SpecialtyModule()
    : CrudCarterModule<
        ISpecialtyService,
        Specialty,
        SpecialtyDto,
        SpecialtyDetailsDto,
        CreateSpecialtyDto,
        UpdateSpecialtyDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Specialty).ToLowerInvariant()));