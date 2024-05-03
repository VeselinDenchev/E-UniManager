using Carter;

using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Abstraction.Base;

using Microsoft.AspNetCore.Http.HttpResults;

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
    public CrudCarterModule(string basePath) : base(basePath)
    {
        WithTags(typeof(TEntity).Name);
    }
    
    protected const string BASE_PATH_TEMPLATE = "/{0}";
    protected const string GET_ALL_ROUTE = "/all";
    protected const string GET_BY_ID_ROUTE = "/{id}";
    protected const string CREATE_ROUTE = "/create";
    protected const string UPDATE_ROUTE = "/update/{id}";
    protected const string DELETE_ROUTE = "/delete/{id}";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(GET_ALL_ROUTE, GetAll);
        app.MapGet(GET_BY_ID_ROUTE, GetById);
        app.MapPost(CREATE_ROUTE, Create);
        app.MapPut(UPDATE_ROUTE, Update);
        app.MapDelete(DELETE_ROUTE, Delete);
    }
    
    protected virtual async Task<IResult> GetAll(TService service, CancellationToken cancellationToken)
    {
        List<TEntityDto> dtos = await service.GetAllAsync(cancellationToken);

        return TypedResults.Ok(dtos);
    }
        
    protected virtual async Task<Results<Ok<TEntityDetailsDto>, NotFound<TEntityDetailsDto>>> GetById
    (
        Guid id, 
        TService service, 
        CancellationToken cancellationToken
    )
    {
        TEntityDetailsDto dto = await service.GetByIdAsync(id, cancellationToken);

        return TypedResults.Ok(dto);
    }
    
    protected virtual async Task<Results<Ok, BadRequest, UnprocessableEntity>> Create
    (
        TCreateDto createDto,
        TService service,
        CancellationToken cancellationToken
    )
    {
        await service.CreateAsync(createDto, cancellationToken);

        return TypedResults.Ok();
    }
    
    protected virtual async Task<Results<Ok, BadRequest, NotFound, UnprocessableEntity>> Update
    (
        Guid id,
        TUpdateDto dto,
        TService service, 
        CancellationToken cancellationToken
    )
    {
        await service.UpdateAsync(id, dto, cancellationToken);

        return TypedResults.Ok();
    }
    
    protected virtual async Task<Results<Ok, BadRequest, NotFound>> Delete
    (
        Guid id,
        TService service, 
        CancellationToken cancellationToken
    )
    {
        await service.DeleteAsync(id, cancellationToken);

        return TypedResults.Ok();
    }
}