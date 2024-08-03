using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Abstraction.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using static EUniManager.Application.Helpers.AuthorizationHelper;

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
    
    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken) 
        => await _dbSet.AsNoTracking()
                       .AnyAsync(e => e.Id.Equals(id), cancellationToken);
    
    public abstract Task<List<TEntityDto>> GetAllAsync(CancellationToken cancellationToken);

    public abstract ValueTask<TDetailsDto> GetByIdAsync(TId id, CancellationToken cancellationToken);

    public abstract Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken);

    public abstract Task UpdateAsync(TId id, IUpdateDto dto, CancellationToken cancellationToken);

    public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        TEntity entity = await _dbSet.FindAsync(id) ?? 
                          throw new ArgumentException(
                              $"Such {typeof(TEntity).Name.ToLowerInvariant()} doesn't exist!");
            
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected async Task<List<TEntity>> GetAllEntitiesAsync(CancellationToken cancellationToken) 
        => await _dbSet.AsNoTracking()
                       .ToListAsync(cancellationToken);

    protected async Task CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void SetNotModifiedPropertiesOnUpdate(TEntity entity)
    {
        _dbContext.Entry(entity).Property(e => e.Id).IsModified = false;
        _dbContext.Entry(entity).Property(e => e.CreatedAt).IsModified = false;
        _dbContext.Entry(entity).Property(e => e.ModifiedAt).IsModified = false;
    }

    protected async Task<Guid> GetStudentIdFromHttpContextAsync(IHttpContextAccessor httpContextAccessor, 
                                                                CancellationToken cancellationToken)
    {
        Guid studentId = await _dbContext.Students.AsNoTracking()
                                                  .Include(s => s.User)
                                                  .Where(s => s.User.Id == GetCurrentUserId(httpContextAccessor))
                                                  .Select(s => s.Id)
                                                  .FirstOrDefaultAsync(cancellationToken);

        if (studentId != Guid.Empty) return studentId;

        throw new ArgumentException("Unauthorized access!");
    }
    
    protected async Task<Guid> GetTeacherIdFromHttpContextAsync(IHttpContextAccessor httpContextAccessor, 
                                                                CancellationToken cancellationToken)
    {
        Guid teacherId = await _dbContext.Teachers.AsNoTracking()
                                                  .Include(s => s.User)
                                                  .Where(s => s.User.Id == GetCurrentUserId(httpContextAccessor))
                                                  .Select(s => s.Id)
                                                  .FirstOrDefaultAsync(cancellationToken);

        if (teacherId != Guid.Empty) return teacherId;

        throw new ArgumentException("Unauthorized access!");
    }
}