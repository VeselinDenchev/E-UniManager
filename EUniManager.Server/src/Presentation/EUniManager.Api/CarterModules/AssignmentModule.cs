using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Assignments.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class AssignmentModule()
    : CrudCarterModule<
            IAssignmentService,
            Assignment,
            IEntityDto,
            AssignmentDetailsDto,
            CreateAssignmentDto,
            UpdateAssignmentDto>
        (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Assignments).ToLowerInvariant()))
{
    private const string GET_WITH_SOLUTION_BY_ID_ROUTE = "/{id}/with-solution";
    private const string GET_STUDENT_ASSIGNMENTS_ROUTE = "/students";
    private const string GET_TEACHER_ASSIGNMENTS_ROUTE = "/teachers";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization();
        app.MapPost(string.Empty, Create).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapDelete(ID_ROUTE, Delete).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapGet(GET_WITH_SOLUTION_BY_ID_ROUTE, GetWithSolutionById).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_STUDENT_ASSIGNMENTS_ROUTE, GetStudentAssignments).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_TEACHER_ASSIGNMENTS_ROUTE, GetTeacherAssignments).RequireAuthorization(TEACHER_POLICY_NAME);
    }
    
    private async Task<Results<Ok<AssignmentWithSolutionDto>, BadRequest, NotFound>> GetWithSolutionById
    (
        IAssignmentService assignmentService,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var studentAssignments = await assignmentService.GetByIdWithStudentSolutionAsync(id, cancellationToken);

        return TypedResults.Ok(studentAssignments);
    }
    
    private async Task<Results<Ok<List<AssignmentDto>>, BadRequest, NotFound>> GetStudentAssignments
    (
        IAssignmentService assignmentService,
        CancellationToken cancellationToken
    )
    {
        var studentAssignments = await assignmentService.GetStudentAssignmentsAsync(cancellationToken);

        return TypedResults.Ok(studentAssignments);
    }
    
    private async Task<Results<Ok<List<AssignmentDto>>, BadRequest, NotFound>> GetTeacherAssignments
    (
        IAssignmentService assignmentService,
        CancellationToken cancellationToken
    )
    {
        var teacherAssignments = await assignmentService.GetTeacherAssignmentsAsync(cancellationToken);

        return TypedResults.Ok(teacherAssignments);
    }
}