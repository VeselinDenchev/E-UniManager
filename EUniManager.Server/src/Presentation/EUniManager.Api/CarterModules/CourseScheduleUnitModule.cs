using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Application.Models.CourseScheduleUnits.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class CourseScheduleUnitModule()
    : CrudCarterModule<
        ICourseScheduleUnitService,
        CourseScheduleUnit,
        CourseScheduleUnitDto,
        CourseScheduleUnitDetailsDto,
        CreateCourseScheduleUnitDto,
        UpdateCourseScheduleUnitDto>
        (string.Format(BASE_PATH_TEMPLATE, "course-schedule-unit"));