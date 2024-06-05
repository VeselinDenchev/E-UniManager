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

public sealed class ActivityModule
    : CrudCarterModule<
            IActivityService,
            Activity,
            ActivityDto,
            ActivityDetailsDto,
            CreateActivityDto,
            IUpdateDto>

{
    public ActivityModule()
        : base(string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Activities).ToLowerInvariant()))
    {
        RequireAuthorization(ADMIN_POLICY_NAME);
    }
    
    private const string TOGGLE_ACTIVITY_ROUTE = "/{id}/toggle";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll);
        app.MapGet(ID_ROUTE, GetById);
        app.MapPost(string.Empty, Create);
        app.MapPatch(TOGGLE_ACTIVITY_ROUTE, ToggleActivity);
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