using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Application.Models.Courses.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class CourseModule :
    CrudCarterModule<
        ICourseService,
        Course,
        CourseDto,
        CourseDetailsDto,
        CreateCourseDto,
        UpdateCourseDto>
{
    public CourseModule() :     
        base(string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Courses).ToLowerInvariant()))
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