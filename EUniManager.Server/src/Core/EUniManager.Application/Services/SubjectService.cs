using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Application.Models.Subjects.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class SubjectService 
    : BaseService<Subject, Guid, SubjectDto, SubjectDetailsDto>,
      ISubjectService
{
    private readonly SubjectMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public SubjectService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<SubjectDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<SubjectDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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