using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Application.Models.RequestApplications.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class RequestApplicationModule()
    : CrudCarterModule<
        IRequestApplicationService,
        RequestApplication,
        RequestApplicationDto,
        IDetailsDto,
        CreateRequestApplicationDto,
        IUpdateDto>
        (string.Format(BASE_ROUTE_TEMPLATE, REQUEST_APPLICATIONS_ROUTE))
{
    private const string GET_ALL_FOR_STUDENT_ROUTE = "/students";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(GET_ALL_FOR_STUDENT_ROUTE, GetAllForStudent).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
    }

    private async Task<Results<Ok<List<RequestApplicationDto>>, BadRequest, NotFound>> GetAllForStudent
    (
        IRequestApplicationService requestApplicationService,
        CancellationToken cancellationToken
    )
    {
        var studentRequestApplications =
            await requestApplicationService.GetAllForStudentAsync(cancellationToken);

        return TypedResults.Ok(studentRequestApplications);
    }
}