using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Application.Models.IndividualProtocols.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class IndividualProtocolModule()
    : CrudCarterModule<
            IIndividualProtocolService,
            IndividualProtocol,
            IndividualProtocolDto,
            IndividualProtocolDetailsDto,
            CreateIndividualProtocolDto,
            IUpdateDto>
       (string.Format(BASE_ROUTE_TEMPLATE, INDIVIDUAL_PROTOCOLS_ROUTE))
{
    private const string UPDATE_STATUS_ROUTE = $"{ID_ROUTE}/status/{{status}}";
    private const string GET_ALL_FOR_STUDENT_ROUTE = "/students";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_STATUS_ROUTE, UpdateStatus).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapDelete(ID_ROUTE, Delete).RequireAuthorization(ADMIN_POLICY_NAME);

        app.MapGet(GET_ALL_FOR_STUDENT_ROUTE, GetAllForStudent).RequireAuthorization(STUDENT_POLICY_NAME);
    }

    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateStatus
    (
        IIndividualProtocolService individualProtocolService,
        [FromRoute] Guid id,
        [FromRoute] IndividualProtocolStatus status,
        CancellationToken cancellationToken
    )
    {
        await individualProtocolService.UpdateStatusAsync(id, status, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<Ok<List<IndividualProtocolDto>>, BadRequest, NotFound>> GetAllForStudent
    (
        IIndividualProtocolService individualProtocolService,
        CancellationToken cancellationToken
    )
    {
        var studentIndividualProtocols = await individualProtocolService.GetAllForStudentAsync(cancellationToken);

        return TypedResults.Ok(studentIndividualProtocols);
    }
}