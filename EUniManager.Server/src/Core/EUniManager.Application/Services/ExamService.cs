using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Application.Models.Exams.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class ExamService : BaseService<Exam, Guid, ExamDto, ExamDetailsDto>, IExamService
{
    private readonly ExamMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public ExamService(EUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<ExamDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<ExamDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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