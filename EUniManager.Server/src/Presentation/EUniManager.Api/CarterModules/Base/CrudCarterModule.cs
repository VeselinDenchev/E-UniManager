using Carter;

using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Abstraction.Base;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;

namespace EUniManager.Api.CarterModules.Base;

public abstract class CrudCarterModule<TService, TEntity, TEntityDto, TEntityDetailsDto, TCreateDto, TUpdateDto> 
    : CarterModule
    where TService : IBaseService<TEntity, Guid, TEntityDto, TEntityDetailsDto>
    where TEntity : BaseEntity<Guid>
    where TEntityDto : class, IEntityDto
    where TEntityDetailsDto : class, IDetailsDto
    where TCreateDto : class, ICreateDto
    where TUpdateDto : class, IUpdateDto
{
    protected const string ID_ROUTE = "/{id}";
    
    protected CrudCarterModule(string basePath) : base(basePath)
    {
        RequireAuthorization();
        WithTags(typeof(TEntity).Name);
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPut(ID_ROUTE, Update).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapDelete(ID_ROUTE, Delete).RequireAuthorization(ADMIN_POLICY_NAME);
    }
    
    protected async Task<Ok<List<TEntityDto>>> GetAll(TService service, CancellationToken cancellationToken)
    {
        List<TEntityDto> dtos = await service.GetAllAsync(cancellationToken);
        
        return TypedResults.Ok(dtos);
    }
        
    protected async Task<Results<Ok<TEntityDetailsDto>, NotFound>> GetById
    (
        Guid id, 
        TService service, 
        CancellationToken cancellationToken
    )
    {
        TEntityDetailsDto dto = await service.GetByIdAsync(id, cancellationToken);

        return TypedResults.Ok(dto);
    }
    
    protected async Task<Results<Created, BadRequest, UnprocessableEntity>> Create
    (
        TCreateDto createDto,
        TService service,
        CancellationToken cancellationToken
    )
    {
        await service.CreateAsync(createDto, cancellationToken);

        return TypedResults.Created();
    }
    
    protected async Task<Results<NoContent, BadRequest, NotFound, UnprocessableEntity>> Update
    (
        TService service,
        [FromRoute] Guid id,
        [FromBody] TUpdateDto dto,
        CancellationToken cancellationToken
    )
    {
        await service.UpdateAsync(id, dto, cancellationToken);

        return TypedResults.NoContent();
    }
    
    protected async Task<Results<NoContent, BadRequest, NotFound>> Delete
    (
        TService service,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await service.DeleteAsync(id, cancellationToken);

        return TypedResults.NoContent();
    }
}