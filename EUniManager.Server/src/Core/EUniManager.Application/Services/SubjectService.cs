using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Application.Models.Subjects.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class SubjectService 
    : BaseService<Subject, Guid, SubjectDto, SubjectDetailsDto>,
      ISubjectService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SubjectMapper _subjectMapper = new();
    
    public SubjectService(IEUniManagerDbContext dbContext, 
                          IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<List<SubjectDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Subject> subjectEntities = await _dbSet.Include(s => s.Course)
                                                    .Include(s => s.Lecturer)
                                                    .Include(s => s.Specialty).ThenInclude(s => s.Faculty)
                                                    .ToListAsync(cancellationToken);
        
        List<SubjectDto> subjectDtos = _subjectMapper.MapSubjectsToSubjectDtos(subjectEntities);

        return subjectDtos;
    }

    public override async ValueTask<SubjectDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Subject subjectEntity = await _dbSet.AsNoTracking()
                                            .Include(s => s.Course)
                                            .Include(s => s.Students)
                                            .Include(s => s.Lecturer)
                                            .Include(s => s.Assistants)
                                            .Include(s => s.Specialty).ThenInclude(s => s.Faculty)
                                            .Include(s => s.Activities)
                                            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");

        SubjectDetailsDto subjectDto = _subjectMapper.Map(subjectEntity);

        return subjectDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createSubjectDto = (dto as CreateSubjectDto)!;

        Subject subject = _subjectMapper.Map(createSubjectDto);
        
        subject.Course = await _dbContext.Courses.FindAsync(createSubjectDto.CourseId, cancellationToken) ??
                         throw new ArgumentException($"Such {nameof(Course).ToLowerInvariant()} doesn't exist!");
        
        subject.Lecturer = await _dbContext.Teachers.FindAsync(createSubjectDto.LecturerId, cancellationToken) ??
                           throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
        
        subject.Assistants = [];
        foreach (Guid assistantId in createSubjectDto.AssistantIds)
        {
            Teacher assistant = await _dbContext.Teachers.FindAsync(assistantId, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
            subject.Assistants.Add(assistant); 
        }
        
        subject.Specialty = await _dbContext.Specialties.Include(s => s.Students)
                                                        .FirstOrDefaultAsync(s => s.Id == createSubjectDto.SpecialtyId, 
                                                                             cancellationToken) ??
                            throw new ArgumentException($"Such {nameof(Specialty).ToLowerInvariant()} doesn't exist!");

        subject.Students = subject.Specialty.Students ?? new List<Student>();

        subject.Marks = new List<SubjectMark>();
        foreach (Student student in subject.Students)
        {
            SubjectMark mark = new()
            {
                Student = student,
                Subject = subject,
                Mark = null
            };
            subject.Marks.Add(mark);
        }
        _dbContext.SubjectMarks.AddRange(subject.Marks);

        await CreateEntityAsync(subject, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        var updateSubjectDto = (dto as UpdateSubjectDto)!;
        
        Subject subject = await _dbSet.Include(s => s.Assistants)
                                      .FirstOrDefaultAsync(s => s.Id == id, cancellationToken) ??
                          throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");
        
        subject.Lecturer = await _dbContext.Teachers.FindAsync(updateSubjectDto.LecturerId, cancellationToken) ??
                           throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
        
        subject.Assistants = new List<Teacher>();
        foreach (Guid assistantId in updateSubjectDto.AssistantIds)
        {
            Teacher assistant = await _dbContext.Teachers.FindAsync(assistantId, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
            subject.Assistants.Add(assistant);
        }

        await UpdateEntityAsync(subject, cancellationToken);
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StudentSubjectInfoDto>> GetStudentSubjectsInfoAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<Subject> studentSubjects = await _dbSet.AsNoTracking()
                                                    .Include(s => s.Course)
                                                    .Include(s => s.Marks).ThenInclude(m => m.Student)
                                                    .Include(sub => sub.Students)
                                                    .Include(s => s.Lecturer)
                                                    .Where(sub => sub.Students.Any(st => st.Id == studentId) && 
                                                                  sub.Marks.Any(m => m.Student.Id == studentId))
                                                    .OrderBy(s => s.Semester)
                                                    .ToListAsync(cancellationToken);

        var subjectsInfo = _subjectMapper.MapSubjectsToStudentSubjectResultDtos(studentSubjects);

        return subjectsInfo;
    }

    public async Task UpdateMarkAsync(Guid subjectId, Guid studentId, Mark mark, CancellationToken cancellationToken)
    {
        Guid lecturerUserId = await _dbSet.Include(s => s.Lecturer).ThenInclude(l => l.User)
                                          .Where(s => s.Id == subjectId)
                                          .Select(s => s.Lecturer.User.Id)
                                          .FirstOrDefaultAsync(cancellationToken);

        Guid teacherIdFromHttpContext = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        if (lecturerUserId != teacherIdFromHttpContext) throw new ArgumentException("Unauthorized access!");
        
        int rowsAffected = await _dbContext.SubjectMarks.Include(sm => sm.Subject)
                                                        .Include(sm => sm.Student)
                                                        .Where(sm => sm.Subject.Id == subjectId && sm.Student.Id == studentId)
                                                        .ExecuteUpdateAsync(s => s.SetProperty(sm => sm.Mark, mark),
                                                                            cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");
    }

    protected override void SetNotModifiedPropertiesOnUpdate(Subject entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(s => s.Course).IsModified = false;
        _dbContext.Entry(entity).Property(s => s.Specialty).IsModified = false;
        _dbContext.Entry(entity).Property(s => s.Students).IsModified = false;
        _dbContext.Entry(entity).Property(s => s.Protocol).IsModified = false;
        _dbContext.Entry(entity).Property(s => s.ControlType).IsModified = false;
    }
}