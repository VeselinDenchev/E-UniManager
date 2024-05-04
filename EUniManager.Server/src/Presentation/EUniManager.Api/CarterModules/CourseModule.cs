using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Application.Models.Courses.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class CourseModule() : 
    CrudCarterModule<
        ICourseService,
        Course,
        CourseDto,
        CourseDetailsDto,
        CreateCourseDto,
        UpdateCourseDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Course).ToLowerInvariant()));