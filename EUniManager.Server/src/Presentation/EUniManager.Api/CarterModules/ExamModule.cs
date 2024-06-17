using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Application.Models.Exams.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class ExamModule()
    : CrudCarterModule<IExamService, Exam, ExamDto, ExamDetailsDto, CreateExamDto, UpdateExamDto>
        (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Exams).ToLowerInvariant()))
{
    private const string GET_EXAMS_FOR_STUDENT_BY_SEMESTER_ROUTE = "/students";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        base.AddRoutes(app);
        
        app.MapGet(GET_EXAMS_FOR_STUDENT_BY_SEMESTER_ROUTE, GetAllForStudentBySemester)
           .RequireAuthorization(STUDENT_POLICY_NAME);
    }

    private async Task<Results<Ok<List<StudentExamDto>>, BadRequest, NotFound>> GetAllForStudentBySemester
    (
        IExamService examService,
        CancellationToken cancellationToken
    )
    {
        var semesterExams = await examService.GetAllForStudentAsync(cancellationToken);

        return TypedResults.Ok(semesterExams);
    }
}