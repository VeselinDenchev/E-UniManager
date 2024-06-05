using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Application.Models.Base.Interfaces;

public interface IBaseService<TEntity, in TId, TEntityDto, TDetailsDto>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TEntityDto: class, IEntityDto
    where TDetailsDto: class, IDetailsDto
{
     Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);
    
     Task<List<TEntityDto>> GetAllAsync(CancellationToken cancellationToken);

     ValueTask<TDetailsDto> GetByIdAsync(TId id, CancellationToken cancellationToken);

     Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken);
    
     Task UpdateAsync(TId id, IUpdateDto dto, CancellationToken cancellationToken);

     Task DeleteAsync(TId id, CancellationToken cancellationToken);
}