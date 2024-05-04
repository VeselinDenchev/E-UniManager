﻿using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Abstraction.Base;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services.Base;

public abstract class BaseService<TEntity, TId, TEntityDto, TDetailsDto> 
    : IBaseService<TEntity, TId, TEntityDto, TDetailsDto>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TEntityDto: class, IEntityDto
    where TDetailsDto: class, IDetailsDto
{
    protected readonly IEUniManagerDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseService(IEUniManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public abstract Task<List<TEntityDto>> GetAllAsync(CancellationToken cancellationToken);

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