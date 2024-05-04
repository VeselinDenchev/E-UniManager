using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Application.Models.Faculties.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class FacultyModule()
    : CrudCarterModule<
        IFacultyService,
        Faculty,
        FacultyDto,
        FacultyDetailsDto,
        CreateFacultyDto,
        UpdateFacultyDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Faculty).ToLowerInvariant()));