using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Application.Models.Subjects.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class SubjectModule()
    : CrudCarterModule<
            ISubjectService,
            Subject,
            SubjectDto,
            SubjectDetailsDto,
            CreateSubjectDto,
            UpdateSubjectDto>
      (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Subjects).ToLowerInvariant()))
{
    private const string GET_STUDENT_SUBJECTS_INFO_ROUTE = "/students";
    private const string UPDATE_MARK_ROUTE = "/{subjectId}/students/{studentId}/marks/{mark}";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(GET_STUDENT_SUBJECTS_INFO_ROUTE, GetStudentSubjectsInfo)
           .RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapPatch(UPDATE_MARK_ROUTE, UpdateMark).RequireAuthorization(TEACHER_POLICY_NAME);
    }

    private async Task<Results<Ok<List<StudentSubjectInfoDto>>, BadRequest, NotFound>> GetStudentSubjectsInfo
    (
        ISubjectService subjectService,
        CancellationToken cancellationToken
    )
    {
        var subjectsInfo = await subjectService.GetStudentSubjectsInfoAsync(cancellationToken);

        return TypedResults.Ok(subjectsInfo);
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateMark
    (
        ISubjectService subjectService,
        [FromRoute] Guid subjectId,
        [FromRoute] Guid studentId,
        [FromRoute] Mark mark,
        CancellationToken cancellationToken
    )
    {
        await subjectService.UpdateMarkAsync(subjectId, studentId, mark, cancellationToken);

        return TypedResults.NoContent();
    }
}