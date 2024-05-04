using EUniManager.Application.Mappers;
using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.AssigmentSolutions.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class AssignmentSolutionService 
    : BaseService<AssignmentSolution, Guid, AssignmentSolutionDto, AssignmentSolutionDetailsDto>, 
      IAssignmentSolutionService
{
    private readonly AssignmentSolutionMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public AssignmentSolutionService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager) 
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<AssignmentSolutionDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<AssignmentSolutionDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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