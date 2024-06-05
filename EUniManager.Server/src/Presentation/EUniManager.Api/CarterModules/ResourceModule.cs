using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Application.Models.Resources.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.RoutesConstant;
using static EUniManager.Api.Constants.PoliciesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class ResourceModule()
    : CrudCarterModule<
            IResourceService,
            Resource, 
            ResourceDto,
            ResourceDetailsDto,
            CreateResourceDto,
            UpdateResourceDto>
        (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Resources).ToLowerInvariant()))
{
    private const string GET_BY_ACTIVITY_ID_ROUTE = "/activities/{activityId}";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapDelete(ID_ROUTE, Delete).RequireAuthorization(TEACHER_POLICY_NAME);
        app.MapGet(GET_BY_ACTIVITY_ID_ROUTE, GetByActivityId).RequireAuthorization(ACADEMIC_POLICY_NAME);
    }

    private async Task<Results<Ok<List<ResourceDto>>, BadRequest, NotFound>> GetByActivityId
    (
        IResourceService resourceService, 
        [FromRoute] Guid activityId, 
        CancellationToken cancellationToken
    )
    {
        var activityResources = await resourceService.GetByActivityIdAsync(activityId, cancellationToken);

        return TypedResults.Ok(activityResources);
    }
}