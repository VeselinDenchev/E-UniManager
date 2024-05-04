using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.CourseSchedules.Dtos;
using EUniManager.Application.Models.CourseSchedules.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class CourseScheduleModule()
    : CrudCarterModule<
        ICourseScheduleService,
        CourseSchedule, CourseScheduleDto,
        CourseScheduleDetailsDto,
        CreateCourseScheduleDto,
        UpdateCourseScheduleDto>
        (string.Format(BASE_PATH_TEMPLATE, "course-schedule"));