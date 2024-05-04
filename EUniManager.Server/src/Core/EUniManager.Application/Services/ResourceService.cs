using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Application.Models.Resources.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class ResourceService : 
    BaseService<Resource, Guid, ResourceDto, ResourceDetailsDto>, IResourceService
{
    private readonly ResourceMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public ResourceService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<ResourceDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<ResourceDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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