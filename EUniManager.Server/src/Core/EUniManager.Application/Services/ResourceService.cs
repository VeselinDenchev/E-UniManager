using EUniManager.Application.Helpers;
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Assignments.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Resources.Dtos;
using EUniManager.Application.Models.Resources.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class ResourceService : 
    BaseService<Resource, Guid, ResourceDto, ResourceDetailsDto>, IResourceService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IAssignmentService _assignmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ResourceMapper _resourceMapper = new();
    
    public ResourceService(IEUniManagerDbContext dbContext,
                           ICloudinaryService cloudinaryService,
                           IAssignmentService assignmentService,
                           IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _cloudinaryService = cloudinaryService;
        _assignmentService = assignmentService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<List<ResourceDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<ResourceDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Resource resourceEntity = await _dbSet.FindAsync(id, cancellationToken) ??
                                  throw new ArgumentException(
                                      $"Such {nameof(Resource).ToLowerInvariant()} doesn't exist!");

        ResourceDetailsDto resourceDto = _resourceMapper.Map(resourceEntity);

        return resourceDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        CreateResourceDto createResourceDto = (dto as CreateResourceDto)!;
        
        Activity resourceActivity = await _dbContext.Activities.Include(a => a.Teacher)
                                                               .FirstOrDefaultAsync(a => a.Id == createResourceDto.ActivityId, 
                                                                                    cancellationToken) ?? 
                                    throw new ArgumentException($"Such {nameof(Activity).ToLowerInvariant()} doesn't exist!");

        Guid teacherIdFromHttpContext = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        if (resourceActivity.Teacher.Id != teacherIdFromHttpContext)
        {
            throw new ArgumentException("Unauthorized access!");
        }
        
        Resource resource = _resourceMapper.Map(createResourceDto);
        resource.Activity = resourceActivity;
        
        if (resource.Type == ResourceType.Info && createResourceDto.File is not null)
        {
            resource.File = await _cloudinaryService.UploadAsync(createResourceDto.File.Bytes,
                                                                 createResourceDto.File.MimeType,
                                                                 cancellationToken);
        }
        
        await CreateEntityAsync(resource, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        Resource existingResource = await _dbSet.AsNoTracking()
                                                .Include(r => r.File)
                                                .Include(r => r.Assignment)
                                                .Include(r => r.Activity).ThenInclude(a => a.Teacher)
                                                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken) ??
                                    throw new ArgumentException("Such resource doesn't exist!");

        Guid teacherIdFromHttpContext = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        if (existingResource.Activity.Teacher.Id != teacherIdFromHttpContext)
        {
            throw new ArgumentException("Unauthorized access!");
        }
        
        UpdateResourceDto updateResourceDto = (dto as UpdateResourceDto)!;
        Resource updatedResource = _resourceMapper.Map(updateResourceDto);
        updatedResource.Id = id;

        bool isUpdatedWithInfoType = updatedResource.Type == ResourceType.Info;
        bool hadFile = existingResource is { Type: ResourceType.Info, File: not null };
        bool hasFileDataInRequest = updateResourceDto.File is { Bytes: { Length: > 0 } } && 
                                    !string.IsNullOrWhiteSpace(updateResourceDto.File.MimeType);
        
        bool willUploadFile = isUpdatedWithInfoType && !hadFile && hasFileDataInRequest;
        bool willUpdateFile = isUpdatedWithInfoType && hadFile && hasFileDataInRequest;
        bool willDeleteFile = hadFile && !hasFileDataInRequest;
        if (willUploadFile)
        {
            updatedResource.File = await _cloudinaryService.UploadAsync(updateResourceDto.File!.Bytes, 
                                                                        updateResourceDto.File.MimeType, 
                                                                        cancellationToken);
        }
        else if (willUpdateFile)
        {
            updatedResource.File = await _cloudinaryService.UpdateAsync(existingResource.File!.Id,
                                                                        updateResourceDto.File!.Bytes,
                                                                        updateResourceDto.File.MimeType,
                                                                        cancellationToken);
        }
        else if (willDeleteFile)
        {
            await _cloudinaryService.DeleteByIdAsync(existingResource.File!.Id);
            updatedResource.File = null;
        }
        else throw new ArgumentException("Invalid file update!");

        // Resource assignment
        bool isAssignmentToInfoUpdate = existingResource.Type == ResourceType.Assignment &&
                                        updatedResource.Type == ResourceType.Info;
        if (isAssignmentToInfoUpdate && existingResource.Assignment is not null)
        {
            await _assignmentService.DeleteByAssignmentAsync(existingResource.Assignment);
            updatedResource.Assignment = null;
        }
        
        SetNotModifiedPropertiesOnUpdate(updatedResource);
        await UpdateEntityAsync(updatedResource, cancellationToken);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Resource resource = await _dbSet.Include(r => r.File)
                                        .Include(r => r.Assignment).ThenInclude(a => a.Solutions)
                                        .Include(r => r.Activity).ThenInclude(a => a.Teacher)
                                        .FirstOrDefaultAsync(r => r.Id == id, cancellationToken) ??
                            throw new ArgumentException($"Such {nameof(Resource).ToLowerInvariant()} doesn't exist!");
        
        Guid teacherIdFromHttpContext = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        if (resource.Activity.Teacher.Id != teacherIdFromHttpContext)
        {
            throw new ArgumentException("Unauthorized access!");
        }

        List<Task> deleteTasks = new();
        
        if (resource.File is not null)
        {
            deleteTasks.Add(_cloudinaryService.DeleteByIdAndExtensionAsync(resource.File.Id, resource.File.Extension));
        }

        if (resource.Assignment is not null)
        {
            deleteTasks.Add(_assignmentService.DeleteByAssignmentAsync(resource.Assignment));
        }

        await Task.WhenAll(deleteTasks);
            
        _dbSet.Remove(resource);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ResourceDto>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken)
    {
        UserRole userRole = AuthorizationHelper.GetCurrentUserRole(_httpContextAccessor);

        List<Resource> activityResourceEntities;
        if (userRole == UserRole.Student)
        {
            Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

            bool hasActivity = await _dbContext.Activities.AsNoTracking()
                                                          .Include(a => a.Students)
                                                          .AnyAsync(a => a.Id == activityId &&
                                                                         a.Students.Any(s => s.Id == studentId), 
                                                                    cancellationToken);

            if (!hasActivity) throw new ArgumentException($"{nameof(Student)} doesn't have this activity"); // Unauthorized
            
            
            activityResourceEntities = await _dbSet.AsNoTracking()
                                                   .Include(r => r.Activity).ThenInclude(a => a.Students)
                                                   .Include(r => r.File)
                                                   .Include(r => r.Assignment)
                                                   .Where(r => r.Activity.Id == activityId && 
                                                               r.Activity.Students.Any(s => s.Id == studentId))
                                                   .ToListAsync(cancellationToken);
        }
        else if (userRole == UserRole.Teacher)
        {
            Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
            
            bool hasActivity = await _dbContext.Activities.AsNoTracking()
                                                          .Include(a => a.Teacher)
                                                          .AnyAsync(a => a.Id == activityId &&
                                                                         a.Teacher.Id == teacherId, cancellationToken);

            if (!hasActivity) throw new ArgumentException($"{nameof(Teacher)} doesn't have this activity"); // Unauthorized
            
            activityResourceEntities = await _dbSet.AsNoTracking()
                                                   .Include(r => r.Activity).ThenInclude(a => a.Teacher)
                                                   .Include(r => r.File)
                                                   .Include(r => r.Assignment)
                                                   .Where(r => r.Activity.Id == activityId &&
                                                               r.Activity.Teacher.Id == teacherId)
                                                   .ToListAsync(cancellationToken);
        }
        else throw new ArgumentException("Unauthorized access!");

        List<ResourceDto> activityResourceDtos = _resourceMapper.Map(activityResourceEntities);
        foreach (ResourceDto resourceDto in activityResourceDtos)
        {
            Resource resourceEntity  = activityResourceEntities.First(a => a.Id == resourceDto.Id);
            
            if (resourceEntity.File is not null)
            {
                FileMapper fileMapper = new();
                resourceDto.File = fileMapper.Map(resourceEntity.File);
            }
            
            if (resourceEntity.Assignment is not null)
            {
                AssignmentMapper assignmentMapper = new();
                resourceDto.Assignment = assignmentMapper.MapAssignmentToAssignmentDto(resourceEntity.Assignment);
            }
        }

        return activityResourceDtos;
    }
}