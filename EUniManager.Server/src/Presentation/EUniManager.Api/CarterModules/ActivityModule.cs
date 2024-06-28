using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Application.Models.Activities.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class ActivityModule()
    : CrudCarterModule<
            IActivityService,
            Activity,
            ActivityDto,
            ActivityDetailsDto,
            CreateActivityDto,
            IUpdateDto>
      (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Activities).ToLowerInvariant()))
{
    private const string GET_ALL_FOR_STUDENT_ROUTE = "/students";
    private const string GET_ALL_FOR_TEACHER_ROUTE = "/teachers";
    private const string TOGGLE_ACTIVITY_ROUTE = "/{id}/toggle";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(GET_ALL_FOR_STUDENT_ROUTE, GetAllForStudent).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_ALL_FOR_TEACHER_ROUTE, GetAllForTeacher).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(TOGGLE_ACTIVITY_ROUTE, ToggleActivity).RequireAuthorization(ADMIN_POLICY_NAME);
    }

    private async Task<Results<Ok<List<ActivityDto>>, UnauthorizedHttpResult>> GetAllForStudent(
        IActivityService activityService,
        CancellationToken cancellationToken
    )
    {
        List<ActivityDto> activities = await activityService.GetAllForStudentAsync(cancellationToken);

        return TypedResults.Ok(activities);
    }
    
    private async Task<Results<Ok<List<TeacherActivityDto>>, UnauthorizedHttpResult>> GetAllForTeacher(
        IActivityService activityService,
        CancellationToken cancellationToken)
    {
        List<TeacherActivityDto> activities = await activityService.GetAllForTeacherAsync(cancellationToken);

        return TypedResults.Ok(activities);
    }
    
    private async Task<Results<NoContent, NotFound>> ToggleActivity
    (
        IActivityService assignmentService, 
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await assignmentService.ToggleActivity(id, cancellationToken);

        return TypedResults.NoContent();
    }
}