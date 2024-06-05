using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Application.Models.CourseScheduleUnits.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class CourseScheduleUnitModule()
    : CrudCarterModule<
            ICourseScheduleUnitService,
            CourseScheduleUnit,
            IEntityDto,
            CourseScheduleUnitDetailsDto,
            CreateCourseScheduleUnitDto,
            UpdateCourseScheduleUnitDto>
      (string.Format(BASE_ROUTE_TEMPLATE, COURSE_SCHEDULE_UNITS_ROUTE))
{
    private const string GET_SPECIALTY_SCHEDULE_ROUTE = "schedule/specialties/semester-type/{semesterType}";
    private const string GET_STUDENT_SCHEDULE_ROUTE = "schedule/students/semester-type/{semesterType}";
    private const string GET_TEACHER_SCHEDULE_ROUTE = "schedule/teachers/semester-type/{semesterType}";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(GET_SPECIALTY_SCHEDULE_ROUTE, GetSpecialtyScheduleByStudent)
           .RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_STUDENT_SCHEDULE_ROUTE, GetStudentSchedule).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_TEACHER_SCHEDULE_ROUTE, GetTeacherSchedule).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapDelete(ID_ROUTE, Delete).RequireAuthorization(ADMIN_POLICY_NAME);
    }

    private async Task<Results<Ok<List<StudentCourseScheduleUnitDto>>, BadRequest, NotFound>> GetSpecialtyScheduleByStudent
    (
        ICourseScheduleUnitService courseScheduleUnitService, 
        [FromRoute] SemesterType semesterType,
        CancellationToken cancellationToken
    )
    {
        var specialtySchedule = await courseScheduleUnitService.GetSpecialtyScheduleByStudentAsync(semesterType, cancellationToken);

        return TypedResults.Ok(specialtySchedule);
    }
    
    private async Task<Results<Ok<List<StudentCourseScheduleUnitDto>>, BadRequest, NotFound>> GetStudentSchedule
    (
        ICourseScheduleUnitService courseScheduleUnitService,
        [FromRoute] SemesterType semesterType,
        CancellationToken cancellationToken
    )
    {
        var studentSchedule = await courseScheduleUnitService.GetStudentScheduleAsync(semesterType, cancellationToken);

        return TypedResults.Ok(studentSchedule);
    }
    
    private async Task<Results<Ok<List<TeacherCourseScheduleUnitDto>>, BadRequest, NotFound>> GetTeacherSchedule
    (
        ICourseScheduleUnitService courseScheduleUnitService,
        [FromRoute] SemesterType semesterType,
        CancellationToken cancellationToken
    )
    {
        var teacherSchedule = await courseScheduleUnitService.GetTeacherScheduleAsync(semesterType, cancellationToken);

        return TypedResults.Ok(teacherSchedule);
    }
}