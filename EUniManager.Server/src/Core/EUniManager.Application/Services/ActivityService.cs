using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Application.Models.Activities.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Services;

public class ActivityService : BaseService<Activity, Guid, ActivityDto, ActivityDetailsDto>, IActivityService
{
    public ActivityService(IEUniManagerDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<List<ActivityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<ActivityDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}