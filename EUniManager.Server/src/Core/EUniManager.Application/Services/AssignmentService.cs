using EUniManager.Application.Mappers;
using EUniManager.Application.Models.AssigmentSolutions.Interfaces;
using EUniManager.Application.Models.Assignments.Dtos;
using EUniManager.Application.Models.Assignments.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class AssignmentService 
    : BaseService<Assignment, Guid, IEntityDto, AssignmentDetailsDto>, IAssignmentService
{
    private readonly IAssignmentSolutionService _assignmentSolutionService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AssignmentMapper _assignmentMapper = new();
    
    public AssignmentService(IEUniManagerDbContext dbContext,
                             IAssignmentSolutionService assignmentSolutionService,
                             IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _assignmentSolutionService = assignmentSolutionService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<List<IEntityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<AssignmentDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Assignment assignmentEntity = await _dbSet.AsNoTracking()
                                                  .FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ??
                                      throw new ArgumentException(
                                          $"Such {nameof(Assignment).ToLowerInvariant()} doesn't exist!"); 

        AssignmentDetailsDto assignmentDto = _assignmentMapper.MapAssignmentToAssignmentDetailsDto(assignmentEntity);

        return assignmentDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createAssignmentDto = (dto as CreateAssignmentDto)!;

        Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        bool hasActivity = await _dbContext.Activities.AsNoTracking()
                                                      .Include(a => a.Teacher)
                                                      .AnyAsync(a => a.Teacher.Id == teacherId, cancellationToken);

        if (!hasActivity) throw new ArgumentException($"{nameof(Teacher)} doesn't have this activity!"); // Unauthorized
        
        Assignment assignment = _assignmentMapper.Map(createAssignmentDto);
        
        assignment.Resource = await _dbContext.Resources.Include(r => r.Activity)
                                                        .FirstOrDefaultAsync(r => r.Type == ResourceType.Assignment && 
                                                                             r.Id == createAssignmentDto.ResourceId,
                                                                             cancellationToken) ??
                              throw new ArgumentException($"Such {nameof(Resource).ToLowerInvariant()} of type " +
                                                          $"{nameof(ResourceType.Assignment).ToLowerInvariant()} with such " +
                                                          $"{nameof(Activity).ToLowerInvariant()} doesn't exist!");
        
        assignment.Students = await _dbContext.Activities.Include(a => a.Students)
                                                         .Where(a => a.Id == assignment.Resource.Activity.Id)
                                                         .Select(a => a.Students)
                                                         .FirstOrDefaultAsync(cancellationToken) ??
                              throw new ArgumentException($"Such {nameof(Assignment).ToLowerInvariant()} doesn't exist!");

        foreach (Student student in assignment.Students)
        {
            await _assignmentSolutionService.CreateAsync(assignment, student, cancellationToken);
        }
        
        assignment.Teacher = await _dbContext.Activities.Include(a => a.Teacher)
                                                        .Where(a => a.Id == assignment.Resource.Activity.Id)
                                                        .Select(a => a.Teacher)
                                                        .FirstOrDefaultAsync(cancellationToken) ??
                             throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
        
        await CreateEntityAsync(assignment, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await ExistsAsync(id, cancellationToken);
        if (!exists) throw new ArgumentException($"Such {nameof(Assignment).ToLowerInvariant()} doesn't exist!");
        
        Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        bool hasActivity = await _dbContext.Activities.AsNoTracking()
                                                      .Include(a => a.Teacher)
                                                      .AnyAsync(a => a.Teacher.Id == teacherId, cancellationToken);

        if (!hasActivity) throw new ArgumentException($"{nameof(Teacher)} doesn't have this activity!"); // Unauthorized
        
        Assignment assignment = _assignmentMapper.Map((dto as UpdateAssignmentDto)!);
        assignment.Id = id;

        assignment.Resource = await _dbSet.Include(a => a.Resource)
                                          .Where(a => a.Id == id)
                                          .Select(a => a.Resource)
                                          .FirstOrDefaultAsync(cancellationToken) ??
                              throw new ArgumentException($"Can't find assignment resource!");
        
        SetNotModifiedPropertiesOnUpdate(assignment);

        await UpdateEntityAsync(assignment, cancellationToken);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        bool hasActivity = await _dbContext.Activities.AsNoTracking()
                                                      .Include(a => a.Teacher)
                                                      .AnyAsync(a => a.Teacher.Id == teacherId, cancellationToken);

        if (!hasActivity) throw new ArgumentException($"{nameof(Teacher)} doesn't have this activity");
        
        Assignment assignment = await _dbSet.Include(a => a.Solutions).ThenInclude(asol => asol.File)
                                            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ?? 
                                throw new ArgumentException($"Such {nameof(Assignment).ToLowerInvariant()} doesn't exist!");

        await DeleteByAssignmentAsync(assignment);
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async ValueTask<AssignmentWithSolutionDto> GetByIdWithStudentSolutionAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        Assignment assignmentEntity = await _dbSet.AsNoTracking()
                                          .Include(a => a.Solutions).ThenInclude(asol => asol.Student)
                                          .Include(a => a.Solutions).ThenInclude(asol => asol.File)
                                          .FirstOrDefaultAsync(a => a.Id == id && 
                                                               a.Solutions.Any(asol => asol.Student.Id == studentId), 
                                                               cancellationToken) ??
                                      throw new ArgumentException(
                                          $"Such {nameof(Assignment).ToLowerInvariant()} doesn't exist!");

        AssignmentSolution solution = assignmentEntity.Solutions.First();
        if (solution.SeenOn is null)
        {
            solution.SeenOn = DateTime.Now;
            _dbContext.AssignmentsSolutions.Update(solution);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        
        AssignmentWithSolutionDto assignmentDto = _assignmentMapper.MapAssignmentToAssignmentWithSolutionDto(assignmentEntity);
        assignmentDto.Solution = new AssignmentSolutionMapper().Map(solution);

        return assignmentDto;
    }
    
    public async Task DeleteByAssignmentAsync(Assignment assignment)
    {
        if (assignment.Solutions.Any())
        {
            await _assignmentSolutionService.DeleteAllSolutionsToAssignmentAsync(assignment.Solutions, assignment.Type);
            assignment.Solutions = [];
        }
        
        _dbSet.Remove(assignment);
    }

    public async Task<List<AssignmentDto>> GetStudentAssignmentsAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<Assignment> assignmentEntities = await _dbSet.AsNoTracking()
                                                          .Include(a => a.Students)
                                                          .Include(a => a.Resource).ThenInclude(r => r.Activity)
                                                          .Where(a => a.Students.Any(s => s.Id == studentId) && 
                                                                      !a.Resource.Activity.IsStopped)
                                                          .OrderByDescending(a => a.CreatedAt)
                                                          .ToListAsync(cancellationToken);
        
        List<AssignmentDto> assignmentDtos = _assignmentMapper.Map(assignmentEntities);

        return assignmentDtos;
    }
    
    public async Task<List<AssignmentDto>> GetTeacherAssignmentsAsync(CancellationToken cancellationToken)
    {
        Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<Assignment> assignmentEntities = await _dbSet.AsNoTracking()
                                                          .Include(a => a.Teacher)
                                                          .Include(a => a.Resource).ThenInclude(r => r.Activity)
                                                          .Where(a => a.Teacher.Id == teacherId &&
                                                                      !a.Resource.Activity.IsStopped)
                                                          .OrderByDescending(a => a.CreatedAt)
                                                          .ToListAsync(cancellationToken);
        
        List<AssignmentDto> assignmentDtos = _assignmentMapper.Map(assignmentEntities);

        return assignmentDtos;
    }

    protected override void SetNotModifiedPropertiesOnUpdate(Assignment entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Collection(a => a.Students).IsModified = false;
        _dbContext.Entry(entity).Collection(a => a.Solutions).IsModified = false;
    }
}