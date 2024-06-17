using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Activities.Interfaces;

public interface IActivityService : IBaseService<Activity, Guid, ActivityDto, ActivityDetailsDto>
{
    Task<List<ActivityDto>> GetAllForStudentAsync(CancellationToken cancellationToken);
    Task ToggleActivity(Guid id, CancellationToken cancellationToken);
}