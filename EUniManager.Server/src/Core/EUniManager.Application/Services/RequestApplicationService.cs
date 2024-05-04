using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Application.Models.RequestApplications.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class RequestApplicationService 
    : BaseService<RequestApplication, Guid, RequestApplicationDto, RequestApplicationDetailsDto>,
      IRequestApplicationService
{
    private readonly RequestApplicationMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public RequestApplicationService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<RequestApplicationDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<RequestApplicationDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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