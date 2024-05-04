using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.SubjectResourcesUnits.Dtos;
using EUniManager.Application.Models.SubjectResourcesUnits.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class SubjectResourcesUnitService 
    : BaseService<SubjectResourcesUnit, Guid, SubjectResourcesUnitDto, SubjectResourcesUnitDetailsDto>, 
      ISubjectResourcesUnitService
{
    private readonly SubjectResourcesUnitMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public SubjectResourcesUnitService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<SubjectResourcesUnitDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<SubjectResourcesUnitDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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