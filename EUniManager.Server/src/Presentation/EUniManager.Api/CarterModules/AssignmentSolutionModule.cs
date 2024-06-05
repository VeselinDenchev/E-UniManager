using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.AssigmentSolutions.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class AssignmentSolutionModule()
    : CrudCarterModule<
        IAssignmentSolutionService,
        AssignmentSolution,
        IEntityDto,
        AssignmentSolutionDetailsDto,
        ManageAssignmentSolutionDto,
        ManageAssignmentSolutionDto>
        (string.Format(BASE_ROUTE_TEMPLATE, ASSIGNMENT_SOLUTIONS_ROUTE))
{
    private const string GET_ALL_SOLUTIONS_TO_ASSIGNMENT_ROUTE = "/assignments/{assignmentId}";
    private const string UPDATE_MARK_ROUTE = "/{id}/update-mark";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ACADEMIC_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_ALL_SOLUTIONS_TO_ASSIGNMENT_ROUTE, GetAllSolutionsToAssignment)
           .RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapPatch(UPDATE_MARK_ROUTE, UpdateMark).RequireAuthorization(TEACHER_POLICY_NAME);
    }

    private async Task<Results<Ok<List<AssignmentSolutionDto>>, BadRequest, NotFound>> GetAllSolutionsToAssignment
    (
        IAssignmentSolutionService assignmentSolutionService,
        [FromRoute] Guid assignmentId,
        CancellationToken cancellationToken
    )
    {
        var solutions = await assignmentSolutionService.GetAllSolutionsToAssignmentAsync(assignmentId, cancellationToken);

        return TypedResults.Ok(solutions);
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateMark
    (
        IAssignmentSolutionService assignmentSolutionService,
        [FromRoute] Guid id,
        UpdateAssignmentSolutionMarkDto updateAssignmentSolutionMarkDto,
        CancellationToken cancellationToken
    )
    {
        await assignmentSolutionService.UpdateMarkAsync(id, updateAssignmentSolutionMarkDto, cancellationToken);

        return TypedResults.NoContent();
    }
}