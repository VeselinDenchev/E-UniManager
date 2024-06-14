using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Application.Models.Faculties.Interfaces;
using EUniManager.Domain.Entities;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class FacultyModule
    : CrudCarterModule<
            IFacultyService,
            Faculty,
            FacultyDto,
            FacultyDetailsDto,
            ManageFacultyDto,
            ManageFacultyDto>
{
    public FacultyModule()
        : base(string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Faculties).ToLowerInvariant()))
    {
        RequireAuthorization(ADMIN_POLICY_NAME);
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll);
        app.MapGet(ID_ROUTE, GetById);
        app.MapPost(string.Empty, Create);
        app.MapPut(ID_ROUTE, Update);
    }
}