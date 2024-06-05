using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Application.Models.IndividualProtocols.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class IndividualProtocolService 
    : BaseService<IndividualProtocol, Guid, IndividualProtocolDto, IndividualProtocolDetailsDto>,
      IIndividualProtocolService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IndividualProtocolMapper _individualProtocolMapper = new();
    
    public IndividualProtocolService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<List<IndividualProtocolDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<IndividualProtocol> individualProtocolEntities = await _dbSet.Include(ip => ip.Subject)
                                                                              .ThenInclude(s => s.Course)
                                                                          .ToListAsync(cancellationToken);
        
        List<IndividualProtocolDto> examIndividualProtocolDtos = _individualProtocolMapper.Map(individualProtocolEntities);

        return examIndividualProtocolDtos;
    }

    public override async ValueTask<IndividualProtocolDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        IndividualProtocol individualProtocolEntity = await _dbSet.AsNoTracking()
                                                                  .Include(ip => ip.Subject).ThenInclude(s => s.Course)
                                                                  .FirstOrDefaultAsync(ip => ip.Id == id, cancellationToken) ??
                                                      throw new ArgumentException("Such individual protocol doesn't exist!");;

        IndividualProtocolDetailsDto individualProtocolDto = _individualProtocolMapper.Map(individualProtocolEntity);

        return individualProtocolDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createIndividualProtocolDto = (dto as CreateIndividualProtocolDto)!;
        IndividualProtocol individualProtocol = _individualProtocolMapper.Map(createIndividualProtocolDto);
        individualProtocol.Student = await _dbContext.Students.FindAsync(createIndividualProtocolDto.StudentId) ??
                                     throw new ArgumentException(
                                         $"Such {nameof(individualProtocol.Subject).ToLowerInvariant()} doesn't exist!");
        individualProtocol.Subject = await _dbContext.Subjects.FindAsync(createIndividualProtocolDto.SubjectId) ??
                                     throw new ArgumentException(
                                         $"Such {nameof(individualProtocol.Subject).ToLowerInvariant()} doesn't exist!");
        
        await CreateEntityAsync(individualProtocol, cancellationToken);
    }

    // Non-updatable
    public override Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IndividualProtocolDto>> GetAllForStudentAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        var studentIndividualProtocolEntities = await _dbSet.Include(ip => ip.Student)
                                                            .Include(ip => ip.Subject).ThenInclude(s => s.Course)
                                                            .Where(ip => ip.Student.Id == studentId)
                                                            .OrderBy(ip => ip.CreatedAt)
                                                            .ToListAsync(cancellationToken);
        
        var studentIndividualProtocolDtos = _individualProtocolMapper.Map(studentIndividualProtocolEntities);

        return studentIndividualProtocolDtos;
    }
    
    
    public async Task UpdateStatusAsync(Guid id, IndividualProtocolStatus status, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(s => s.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(sp => sp.Status, status),
                                                           cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such individual protocol doesn't exist!");
    }
}