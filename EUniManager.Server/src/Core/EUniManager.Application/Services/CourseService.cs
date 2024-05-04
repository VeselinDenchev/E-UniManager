using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Application.Models.Courses.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class CourseService : BaseService<Course, Guid, CourseDto, CourseDetailsDto>, ICourseService
{
    private readonly CourseMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public CourseService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<CourseDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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