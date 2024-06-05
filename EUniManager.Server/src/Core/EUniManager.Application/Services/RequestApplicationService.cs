using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Application.Models.RequestApplications.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class RequestApplicationService 
    : BaseService<RequestApplication, Guid, RequestApplicationDto, IDetailsDto>,
      IRequestApplicationService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RequestApplicationMapper _requestApplicationMapper = new();
    
    public RequestApplicationService(IEUniManagerDbContext dbContext, 
                                     ICloudinaryService cloudinaryService, 
                                     IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _cloudinaryService = cloudinaryService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<List<RequestApplicationDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<RequestApplication> requestApplicationEntities = await _dbSet.AsNoTracking()
                                                                          .Include(ra => ra.Student)
                                                                          .Include(ra => ra.File)
                                                                          .ToListAsync(cancellationToken);
        
        List<RequestApplicationDto> requestApplicationDtos = _requestApplicationMapper.Map(requestApplicationEntities);

        return requestApplicationDtos;
    }

    // Doesn't need such method
    public override ValueTask<IDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createRequestApplicationDto = (dto as CreateRequestApplicationDto)!;
        RequestApplication requestApplication = _requestApplicationMapper.Map(createRequestApplicationDto);
        
        requestApplication.Student = await _dbContext.Students.FindAsync(createRequestApplicationDto.StudentId) ?? 
                                     throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
        
        requestApplication.File = await _cloudinaryService.UploadAsync(createRequestApplicationDto.FileBytes,
                                                                       createRequestApplicationDto.FileMimeType,
                                                                       cancellationToken);
        
        await CreateEntityAsync(requestApplication, cancellationToken);
    }

    // Non-updatable
    public override Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<List<RequestApplicationDto>> GetAllForStudentAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<RequestApplication> requestApplicationEntities = await _dbSet.AsNoTracking()
                                                                          .Include(ra => ra.Student)
                                                                          .Include(ra => ra.File)
                                                                          .Where(ra => ra.Student.Id == studentId)
                                                                          .ToListAsync(cancellationToken);
        
        List<RequestApplicationDto> requestApplicationDtos = _requestApplicationMapper.Map(requestApplicationEntities);

        return requestApplicationDtos;
    }

    protected override void SetNotModifiedPropertiesOnUpdate(RequestApplication entity)
    {
        throw new NotImplementedException();
    }
}