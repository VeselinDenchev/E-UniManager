using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseSchedules.Dtos;
using EUniManager.Application.Models.CourseSchedules.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class CourseScheduleService 
    : BaseService<CourseSchedule, Guid, CourseScheduleDto, CourseScheduleDetailsDto>, ICourseScheduleService
{
    private readonly CourseScheduleMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public CourseScheduleService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<CourseScheduleDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<CourseScheduleDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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