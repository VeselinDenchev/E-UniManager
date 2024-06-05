using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Application.Models.Specialties.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class SpecialtyModule
    : CrudCarterModule<
            ISpecialtyService,
            Specialty,
            SpecialtyDto,
            SpecialtyDetailsDto,
            CreateSpecialtyDto,
            IUpdateDto>
{
    private const string GET_ALL_BY_FACULTY_ROUTE = "/faculties/{facultyId}";
    private const string GRADUATE_ROUTE = "/{id}/graduate";
    private const string INCREMENT_ACADEMIC_YEAR_ROUTE = "/increment-academic-year";

    public SpecialtyModule()
        : base(string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Specialties).ToLowerInvariant()))
    {
        RequireAuthorization(ADMIN_POLICY_NAME);
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll);
        app.MapGet(ID_ROUTE, GetById);
        app.MapGet(GET_ALL_BY_FACULTY_ROUTE, GetAllByFaculty);
        app.MapPost(string.Empty, Create);
        app.MapPatch(GRADUATE_ROUTE, Graduate);
        app.MapPatch(INCREMENT_ACADEMIC_YEAR_ROUTE, IncrementAcademicYear);
    }

    private async Task<Results<Ok<List<SpecialtyDto>>, BadRequest, NotFound>> GetAllByFaculty
    (
        ISpecialtyService specialtyService,
        [FromRoute] Guid facultyId,
        CancellationToken cancellationToken
    )
    {
        List<SpecialtyDto> facultySpecialties = await specialtyService.GetAllByFaculty(facultyId, cancellationToken);

        return TypedResults.Ok(facultySpecialties);
    }

    private async Task<IResult> Graduate(ISpecialtyService specialtyService,
                                         [FromRoute] Guid id,
                                         CancellationToken cancellationToken)
    {
        await specialtyService.GraduateAsync(id, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<IResult> IncrementAcademicYear(ISpecialtyService specialtyService, 
                                                      CancellationToken cancellationToken)
    {
        await specialtyService.IncrementAcademicYearAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}