using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CertifiedSemesters.Dtos;
using EUniManager.Application.Models.CertifiedSemesters.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class CertifiedSemesterService 
    : BaseService<CertifiedSemester, Guid, IEntityDto, IDetailsDto>, 
      ICertifiedSemesterService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CertifiedSemesterMapper _certifiedSemesterMapper = new();
    
    public CertifiedSemesterService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    // Will be implemented later
    
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task<List<IEntityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public override ValueTask<IDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public override Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public override Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StudentCertifiedSemesterDto>> GetAllForStudentAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        List<CertifiedSemester> certifiedSemesterEntities = await _dbSet.AsNoTracking()
                                                                        .Include(cs => cs.Student)
                                                                            .ThenInclude(s => s.Specialty)
                                                                        .Where(cs => cs.Student.Id == studentId)
                                                                        .OrderBy(cs => cs.Semester)
                                                                            .ThenBy(cs => cs.CreatedAt)
                                                                        .ToListAsync(cancellationToken);

        var certifiedSemesterDtos = _certifiedSemesterMapper.Map(certifiedSemesterEntities);

        return certifiedSemesterDtos;
    }
}