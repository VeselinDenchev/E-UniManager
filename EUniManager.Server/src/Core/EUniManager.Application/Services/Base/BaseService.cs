using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Abstraction.Base;
using EUniManager.Persistence;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services.Base;

public abstract class BaseService<TEntity, TId, TListElementDto, TDetailsDto> 
    : IBaseService<TEntity, TId, TListElementDto, TDetailsDto>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TListElementDto: class, IEntityDto
    where TDetailsDto: class, IDetailsDto
{
    protected readonly EUniManagerDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseService(EUniManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public abstract Task<List<TListElementDto>> GetAllAsync(CancellationToken cancellationToken);

    public abstract ValueTask<TDetailsDto> GetByIdAsync(TId id, CancellationToken cancellationToken);

    public abstract Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken);

    public abstract Task UpdateAsync(TId id, IUpdateDto dto, CancellationToken cancellationToken);

    public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            throw new ArgumentNullException($"Such {typeof(TEntity).Name.ToLower()} doesn't exist!");
        }
            
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected async Task<List<TEntity>> GetAllEntitiesAsync(CancellationToken cancellationToken) 
        => await _dbSet.AsNoTracking()
                       .ToListAsync(cancellationToken);

    protected async ValueTask<TEntity?> GetEntityByIdAsync(TId id, CancellationToken cancellationToken) 
        => await _dbSet.FindAsync([id], cancellationToken);

    protected async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}