using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Application.Models.Faculties.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class FacultyService : BaseService<Faculty, Guid, FacultyDto, FacultyDetailsDto>, IFacultyService
{
    private readonly FacultyMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public FacultyService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<FacultyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<FacultyDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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