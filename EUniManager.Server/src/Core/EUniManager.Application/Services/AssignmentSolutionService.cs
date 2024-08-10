using EUniManager.Application.Extensions;
using EUniManager.Application.Helpers;
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.AssigmentSolutions.Dtos;
using EUniManager.Application.Models.AssigmentSolutions.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class AssignmentSolutionService 
    : BaseService<AssignmentSolution, Guid, IEntityDto, AssignmentSolutionDetailsDto>, 
      IAssignmentSolutionService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AssignmentSolutionMapper _assignmentSolutionMapper = new();

    public AssignmentSolutionService(IEUniManagerDbContext dbContext,
                                     ICloudinaryService cloudinaryService,
                                     IHttpContextAccessor httpContextAccessor) 
        : base(dbContext)
    {
        _cloudinaryService = cloudinaryService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<List<IEntityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<AssignmentSolutionDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        UserRole userRole = AuthorizationHelper.GetCurrentUserRole(_httpContextAccessor);

        AssignmentSolution solutionEntity;
        if (userRole == UserRole.Student)
        {
            solutionEntity = await _dbSet.AsNoTracking()
                                         .Include(asol => asol.Assignment).ThenInclude(a => a.Students)
                                         .FirstOrDefaultAsync(asol => asol.Id == id, cancellationToken) ?? 
                             throw new ArgumentException("Such assignment solution doesn't exist!");
            
            if (solutionEntity.SeenOn is null)
            {
                _dbContext.Attach(solutionEntity);
                
                solutionEntity.SeenOn = DateTime.Now;
                _dbSet.Update(solutionEntity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
            
            if (!solutionEntity.Assignment.Students.TrueForAll(s => s.Id != studentId))
            {
                throw new ArgumentException("Unauthorized access!");
            }
        }
        else if (userRole == UserRole.Teacher)
        {
            solutionEntity = await _dbSet.AsNoTracking()
                                         .Include(asol => asol.Assignment).ThenInclude(a => a.Teacher)
                                         .FirstOrDefaultAsync(asol => asol.Id == id, cancellationToken) ??
                             throw new ArgumentException("Such assignment solution doesn't exist!");

            Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

            if (solutionEntity.Assignment.Teacher.Id != teacherId)
            {
                throw new ArgumentException("Unauthorized access!");
            }
        }
        else throw new ArgumentException("Unauthorized access!");
        
        AssignmentSolutionDetailsDto solutionDto = _assignmentSolutionMapper.Map(solutionEntity);

        return solutionDto;
    }

    // Assignment solution is created for each student when the assignment is created
    public override Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        ManageAssignmentSolutionDto solutionDto = (dto as ManageAssignmentSolutionDto)!;


        AssignmentSolution solutionEntity = await _dbSet.Include(asol => asol.Assignment)
                                                        .Include(asol => asol.Student)
                                                        .FirstOrDefaultAsync(asol => asol.Id == id, cancellationToken) ??
                                            throw new ArgumentException("Such assignment solution doesn't exist!");
        
        Guid studentIdFromHttpContext = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        if (solutionEntity.Student.Id != studentIdFromHttpContext)
        {
            throw new ArgumentException("Unauthorized access!");
        }

        bool hasFileDataInRequest = solutionDto.File is { Bytes: { Length: > 0 } } &&
                                    !string.IsNullOrWhiteSpace(solutionDto.File.MimeType);
        if (solutionEntity.Assignment.Type is AssignmentType.File && hasFileDataInRequest)
        {
            solutionEntity.File = await _cloudinaryService.UploadAsync(solutionDto.File!.Bytes,
                                                                       solutionDto.File.MimeType, 
                                                                       cancellationToken);
        }
        else if (solutionEntity.Assignment.Type is AssignmentType.Text)
        {
            solutionEntity.Text = solutionDto.Text;
        }
        
        bool hasSolution = solutionEntity.File is not null || solutionEntity.Text is not null;
        solutionEntity.SubmittedOn = hasSolution ? DateTime.Now : null;
        
        SetNotModifiedPropertiesOnUpdate(solutionEntity);
        
        await UpdateEntityAsync(solutionEntity, cancellationToken);
    }

    // Can't delete single solution. All solutions related to assignment are deleted with it.
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task CreateAsync(Assignment assignment, Student student, CancellationToken cancellationToken)
    {
        AssignmentSolution assignmentSolution = new()
        {
            Assignment = assignment,
            Student = student
        };
        
        await _dbSet.AddAsync(assignmentSolution, cancellationToken);
    }

    public async Task<List<AssignmentSolutionDto>> GetAllSolutionsToAssignmentAsync(Guid assignmentId, 
                                                                                    CancellationToken cancellationToken)
    {
        Guid assignmentTeacherId = await _dbContext.Teachers.AsNoTracking()
                                                            .Include(t => t.Assignments)
                                                            .Where(t => t.Assignments.Any(a => a.Id == assignmentId))
                                                            .Select(t => t.Id)
                                                            .FirstOrDefaultAsync(cancellationToken);

        await AuthorizeAssignmentTeacherAsync(assignmentTeacherId, cancellationToken);
        
        List<AssignmentSolution> solutionEntities = await _dbSet.AsNoTracking()
                                                                .Include(asol => asol.Assignment).ThenInclude(a => a.Teacher)
                                                                .Include(asol => asol.Student)
                                                                .Include(asol => asol.File)
                                                                .Where(asol => asol.Assignment.Id == assignmentId)
                                                                .ToListAsync(cancellationToken);

        List<AssignmentSolutionDto> solutionDtos = _assignmentSolutionMapper.Map(solutionEntities);

        return solutionDtos;
    }

    public async Task UpdateMarkAsync(Guid id, Mark mark, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(mark))
        {
            throw new ArgumentException("Such mark doesn't exist!");
        }
        
        bool exists = await _dbSet.AsNoTracking()
                                  .AnyAsync(asol => asol.Id == id, cancellationToken);
        if (!exists) throw new ArgumentException($"Such assignment solution doesn't exist!");
        
        Guid assignmentTeacherId = await GetAssignmentTeacherBySolutionIdAsync(id, cancellationToken);
        await AuthorizeAssignmentTeacherAsync(assignmentTeacherId, cancellationToken);
        
        int rowsAffected = await _dbSet.Where(asol => asol.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(a => a.Mark, a => mark)
                                                                 .SetProperty(a => a.MarkedOn, a => DateTime.Now)
                                                                 .SetProperty(a => a.Comment, a => null)
                                                                 .SetProperty(a => a.ModifiedAt, a => DateTime.Now),
                                                           cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such assignment solution doesn't exist");
    }
    
    
    public async Task UpdateCommentAsync(Guid id, string comment, CancellationToken cancellationToken)
    {
        bool exists = await _dbSet.AsNoTracking()
                                  .AnyAsync(asol => asol.Id == id && asol.Mark == null, cancellationToken);
        if (!exists) throw new ArgumentException($"Such assignment solution doesn't exist or it doesn't have mark!");
        
        Guid assignmentTeacherId = await GetAssignmentTeacherBySolutionIdAsync(id, cancellationToken);
        await AuthorizeAssignmentTeacherAsync(assignmentTeacherId, cancellationToken);
        
        int rowsAffected = await _dbSet.Where(asol => asol.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(a => a.Comment, a => comment)
                                                                 .SetProperty(a => a.ModifiedAt, a => DateTime.Now),
                                                           cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such assignment solution doesn't exist");
    }

    public async Task DeleteAllSolutionsToAssignmentAsync(List<AssignmentSolution> assignmentSolutions, 
                                                          AssignmentType assignmentType)
    {
        if (assignmentType == AssignmentType.File)
        {
            List<Task> deleteFileTasks = new();
            foreach (AssignmentSolution solution in assignmentSolutions.Where(asol => asol.File is not null))
            {
                deleteFileTasks.Add(_cloudinaryService.DeleteAsync(solution.File!));
            }
            await Task.WhenAll(deleteFileTasks);
        }

        _dbSet.RemoveRange(assignmentSolutions);
    }

    protected override void SetNotModifiedPropertiesOnUpdate(AssignmentSolution entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(asol => asol.SeenOn).IsModified = false;
        _dbContext.Entry(entity).Property(asol => asol.Mark).IsModified = false;
        _dbContext.Entry(entity).Property(asol => asol.MarkedOn).IsModified = false;
        _dbContext.Entry(entity).Property(asol => asol.Comment).IsModified = false;
    }

    private async Task AuthorizeAssignmentTeacherAsync(Guid teacherIdToCheck, CancellationToken cancellationToken)
    {
        Guid teacherIdFromHttpContext = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);

        if (teacherIdToCheck != teacherIdFromHttpContext)
        {
            throw new ArgumentException("Unauthorized access!");
        }
    }

    private async Task<Guid> GetAssignmentTeacherBySolutionIdAsync(Guid assignmentSolutionId, CancellationToken cancellationToken)
    {
        Guid teacherId = await _dbSet.Include(asol => asol.Assignment).ThenInclude(a => a.Teacher)
                                     .Where(asol => asol.Id == assignmentSolutionId)
                                     .Select(asol => asol.Assignment.Teacher.Id)
                                     .FirstOrDefaultAsync(cancellationToken);

        return teacherId;
    }
}