using EUniManager.Domain.Abstraction.Base;

namespace EUniManager.Application.Models.Base.Interfaces;

public interface IBaseService<TEntity, in TId, TListElementDto, TDetailsDto>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TListElementDto: class, IEntityDto
    where TDetailsDto: class, IDetailsDto
{
     Task<List<TListElementDto>> GetAllAsync(CancellationToken cancellationToken);

     ValueTask<TDetailsDto> GetByIdAsync(TId id, CancellationToken cancellationToken);

     Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken);
    
     Task UpdateAsync(TId id, IUpdateDto dto, CancellationToken cancellationToken);

     Task DeleteAsync(TId id, CancellationToken cancellationToken);
}