using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Application.Models.Subjects.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class SubjectModule()
    : CrudCarterModule<
        ISubjectService,
        Subject,
        SubjectDto,
        SubjectDetailsDto,
        CreateSubjectDto,
        UpdateSubjectDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Subject).ToLowerInvariant()));