using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CertifiedSemesters.Dtos;
using EUniManager.Application.Models.CertifiedSemesters.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public class CertifiedSemesterModule()
    : CrudCarterModule<
            ICertifiedSemesterService,
            CertifiedSemester,
            IEntityDto,
            IDetailsDto,
            ICreateDto,
            IUpdateDto>
        (string.Format(BASE_ROUTE_TEMPLATE, CERTIFIED_SEMESTERS_ROUTE))
{
    private const string GET_STUDENT_CERTIFIED_SEMESTERS_ROUTE = "/students";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(GET_STUDENT_CERTIFIED_SEMESTERS_ROUTE, GetStudentCertifiedSemesters)
           .RequireAuthorization(STUDENT_POLICY_NAME);
    }

    private async Task<Results<Ok<List<StudentCertifiedSemesterDto>>, BadRequest, NotFound>>
        GetStudentCertifiedSemesters(
            ICertifiedSemesterService certifiedSemesterService,
            CancellationToken cancellationToken
        )
    {
        var certifiedSemesters = await certifiedSemesterService.GetAllForStudentAsync(cancellationToken);
        
        return TypedResults.Ok(certifiedSemesters);
    }
}