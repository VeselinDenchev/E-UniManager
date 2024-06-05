using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Application.Models.Exams.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class ExamService : BaseService<Exam, Guid, ExamDto, ExamDetailsDto>, IExamService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ExamMapper _examMapper = new();
    
    public ExamService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<List<ExamDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Exam> examEntities = await _dbSet.Include(e => e.Subject).ThenInclude(s => s.Course)
                                              .Include(e => e.Subject).ThenInclude(s => s.Specialty)
                                              .OrderByDescending(e => e.Date)
                                                  .ThenByDescending(e => e.Time)
                                                  .ThenBy(e => e.Subject.Specialty.Name)
                                                  .ThenBy(e => e.GroupNumber)
                                                  .ThenBy(e => e.Subject.Course.Name)
                                                  .ThenBy(e => e.Type)
                                                  .ThenBy(e => e.Place)
                                                  .ThenBy(e => e.RoomNumber)
                                              .ToListAsync(cancellationToken);
        
        List<ExamDto> examDtos = _examMapper.MapExamList(examEntities);

        return examDtos;
    }

    public override async ValueTask<ExamDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Exam examEntity = await _dbSet.AsNoTracking()
                                      .Include(e => e.Subject).ThenInclude(s => s.Course)
                                      .FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
                          throw new ArgumentException($"Such {nameof(Exam).ToLowerInvariant()} doesn't exist!");

        ExamDetailsDto examDto = _examMapper.Map(examEntity);

        return examDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createExamDto = (dto as CreateExamDto)!;

        List<Exam> subjectExams = await _dbContext.Subjects.AsNoTracking()
                                                           .Include(s => s.Exams)
                                                           .Where(s => s.Id == createExamDto.SubjectId)
                                                           .Select(s => s.Exams)
                                                           .FirstOrDefaultAsync(cancellationToken) ??
                                  throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");
        
        Exam exam = _examMapper.Map(createExamDto);

        if (exam.Type is ExamType.Regular or ExamType.Remedial)
        {
            CheckExamTypeExistenceForSubject(subjectExams, exam.Type.Value);
        }
        
        exam.Subject = await _dbContext.Subjects.FindAsync(createExamDto.SubjectId, cancellationToken) ??
                       throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");
        
        await CreateEntityAsync(exam, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            throw new ArgumentException($"Such {nameof(Exam).ToLowerInvariant()} doesn't exist");
        }
        
        Exam exam = _examMapper.Map((dto as UpdateExamDto)!);
        exam.Id = id;
        exam.Subject = await _dbSet.AsNoTracking()
                                   .Include(e => e.Subject)
                                   .Where(e => e.Id == id)
                                   .Select(e => e.Subject)
                                   .FirstAsync(cancellationToken);

        _dbSet.Update(exam);
        SetNotModifiedPropertiesOnUpdate(exam);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<StudentExamDto>> GetAllForStudentBySemesterAsync(byte currentYear, 
                                                                            CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<Exam> semesterExamEntities = 
            await _dbSet.AsNoTracking()
                        .Include(e => e.Subject).ThenInclude(s => s.Course)
                        .Include(e => e.Subject).ThenInclude(s => s.Students)
                                                .ThenInclude(s => s.Specialty)
                        .Include(e => e.Subject).ThenInclude(s => s.Lecturer)
                        .Where(e => e.Subject.Students.Any(s => s.Id == studentId &&
                                                                s.ServiceData.EnrolledInSemester >= (currentYear * 2 - 1) &&
                                                                s.Specialty.CurrentYear == currentYear))
                        .OrderBy(e => e.Date).ThenBy(e => e.Time)
                        .ToListAsync(cancellationToken);

        List<StudentExamDto> semesterExamDtos = _examMapper.MapStudentExamList(semesterExamEntities);

        return semesterExamDtos;
    }
    
    // GetAllForTeacher

    private void CheckExamTypeExistenceForSubject(List<Exam> exams, ExamType examType)
    {
        bool alreadyHasThisExamType = exams.Exists(e => e.Type == examType);
        if (!alreadyHasThisExamType) return;
        
        throw new ArgumentException($"{nameof(Subject)} already has {examType.ToString().ToLowerInvariant()} {nameof(Exam).ToLowerInvariant()}");
    }

    protected override void SetNotModifiedPropertiesOnUpdate(Exam entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(e => e.Type).IsModified = false;
    }
}