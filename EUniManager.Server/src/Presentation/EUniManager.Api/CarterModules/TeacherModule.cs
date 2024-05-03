using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Application.Models.Teachers.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class TeacherModule() 
    : CrudCarterModule<
        ITeacherService, 
        Teacher, 
        TeacherDto, 
        TeacherDetailsDto, 
        CreateTeacherDto, 
        UpdateTeacherDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Teacher).ToLowerInvariant()));