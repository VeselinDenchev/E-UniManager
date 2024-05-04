using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Application.Models.Students.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities.Students;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Services;

public sealed class StudentService 
    : BaseService<Student, Guid, StudentDto, StudentDetailsDto>, IStudentService
{
    private readonly StudentMapper _mapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public StudentService(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _userManager = userManager;
    }

    public override async Task<List<StudentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<StudentDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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