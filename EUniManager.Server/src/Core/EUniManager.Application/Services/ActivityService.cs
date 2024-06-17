using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Application.Models.Activities.Interfaces;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public class ActivityService
    : BaseService<Activity, Guid, ActivityDto, ActivityDetailsDto>, 
      IActivityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ActivityMapper _activityMapper = new();

    public ActivityService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public override async Task<List<ActivityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Activity> activityEntities = await _dbSet.Include(a => a.Teacher)
                                                      .Include(a => a.Subject).ThenInclude(s => s.Course)
                                                      .ToListAsync(cancellationToken);
        
        List<ActivityDto> activityDtos = _activityMapper.Map(activityEntities);

        return activityDtos;
    }

    public override async ValueTask<ActivityDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Activity activityEntity = await _dbSet.AsNoTracking()
                                              .Include(a => a.Teacher)
                                              .Include(a => a.Subject).ThenInclude(s => s.Course)
                                              .FirstOrDefaultAsync(cancellationToken) ??
                                  throw new ArgumentException($"Such {nameof(Activity).ToLowerInvariant()} doesn't exist!");

        ActivityDetailsDto activityDto = _activityMapper.Map(activityEntity);

        return activityDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createActivityDto = (dto as CreateActivityDto)!;
        Activity activity = _activityMapper.Map(createActivityDto);
        
        activity.Teacher = await _dbContext.Teachers.FindAsync(createActivityDto.TeacherId, cancellationToken) ??
                           throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
        
        activity.Subject = await _dbContext.Subjects.Include(s => s.Lecturer)
                                                    .Include(s => s.Assistants)
                                                    .FirstOrDefaultAsync(s => s.Id == createActivityDto.SubjectId, 
                                                                         cancellationToken) ??
                           throw new ArgumentException($"Such {nameof(Subject).ToLowerInvariant()} doesn't exist!");

        List<Activity> otherActivitiesFromSameSubject = await _dbSet.AsNoTracking()
                                                                    .Include(a => a.Subject)
                                                                    .Where(a => a.Subject.Id == activity.Subject.Id)
                                                                    .ToListAsync(cancellationToken);

        bool isLecture = activity.Type == ActivityType.Lecture;
        bool hasLecture = otherActivitiesFromSameSubject.Any(a => a.Type == ActivityType.Lecture);
        bool isExercise = activity.Type == ActivityType.Exercise;
        
        if (isLecture && hasLecture)
        {
            throw new ArgumentException("Lecture for this subject already exists!");
        }
        else if (isExercise)
        {
            int otherExercisesFromSameSubjectCount = otherActivitiesFromSameSubject.Count(a => a.Type == ActivityType.Exercise);
            
            bool areExercisesAvailable = otherExercisesFromSameSubjectCount < activity.Subject.Assistants.Count;
            if (!areExercisesAvailable)
            {
                throw new ArgumentException("There are no exercises available for this subject");
            }
        }
        
        if (isLecture && activity.Teacher.Id != activity.Subject.Lecturer.Id)
        {
            throw new ArgumentException("This teacher is not the lecturer of the subject. " +
                                        $"Try with {activity.Subject.Lecturer.FullNameWithRank}");
        }
        else if (isExercise && !activity.Subject.Assistants.Contains(activity.Teacher))
        {
            string[] subjectAssistantFullNamesWithRank = activity.Subject.Assistants.Select(a => a.FullNameWithRank)
                                                                 .ToArray();
            string possibleAssistantsString = string.Join(", ", subjectAssistantFullNamesWithRank);
            
            throw new ArgumentException("This teacher can't be assistant of the subject. " +
                                        $"Try with {possibleAssistantsString}.");
        }

        bool hasExercise = isExercise || otherActivitiesFromSameSubject.Any(a => a.Type == ActivityType.Exercise);
        bool isLastTeacherForSubject = otherActivitiesFromSameSubject.Count + 1 == activity.Subject.Assistants.Count + 1;
        if (hasLecture && hasExercise && isLastTeacherForSubject)
        {
            // Thus teacher is already validated in the upper if-statement only assistants will be validated
            List<Activity> exercises = otherActivitiesFromSameSubject.Where(a => a.Type == ActivityType.Exercise).ToList();
            if (isExercise)
            {
                exercises.Add(activity);
            }

            List<Teacher> assistants = activity.Subject.Assistants;
            if (isExercise)
            {
                assistants.Add(activity.Teacher);
            }
            
            ValidateAssistants(exercises, assistants);
        }

        if (isLecture)
        {
            activity.Students = activity.Subject.Students;
        }
        else if (isExercise)
        {
            activity.Students = new List<Student>();
            foreach (Guid studentId in createActivityDto.StudentIds)
            {
                Student student = await _dbContext.Students.Include(s =>s.Subjects)
                                                           .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken) ??
                                  throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");

                bool hasTheSubject = student.Subjects.Contains(activity.Subject);

                if (!hasTheSubject)
                {
                    throw new ArgumentException($"{nameof(Student).ToLowerInvariant()} doesn't have this subject!");
                }
                
                bool isContainedInOtherExerciseActivitiesFromThisSubject =
                    otherActivitiesFromSameSubject.Where(a => a.Type == ActivityType.Exercise)
                                                  .Any(a => a.Students.Contains(student));

                if (isContainedInOtherExerciseActivitiesFromThisSubject)
                {
                    throw new ArgumentException($"{nameof(Student).ToLowerInvariant()} is assigned to another exercise of this subject!");
                }
                
                activity.Students.Add(student);
            }
        }
        
        // Add validation for lectures and exercised count compared to Course
        
        await CreateEntityAsync(activity, cancellationToken);
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

    public async Task<List<ActivityDto>> GetAllForStudentAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<Activity> activityEntities = await _dbSet.Include(a => a.Teacher)
                                                      .Include(a => a.Subject).ThenInclude(s => s.Course)
                                                      .Include(a => a.Students)
                                                      .Where(a => a.Students.Any(s => s.Id == studentId))
                                                      .OrderByDescending(a => a.Subject.Semester)
                                                          .ThenBy(a => a.Subject.Course.Name)
                                                          .ThenByDescending(a => a.Type)
                                                      .ToListAsync(cancellationToken);
        
        List<ActivityDto> activityDtos = _activityMapper.Map(activityEntities);

        return activityDtos;
    }
    
    public async Task ToggleActivity(Guid id, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(a => a.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsStopped,
                                                           p => !p.IsStopped),
                                                           cancellationToken);

        if (rowsAffected > 0) return;
        
        throw new ArgumentException($"Such {nameof(Exam).ToLowerInvariant()} doesn't exist");
    }

    protected override void SetNotModifiedPropertiesOnUpdate(Activity entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(a => a.Type).IsModified = false;
        _dbContext.Entry(entity).Property(a => a.Subject).IsModified = false;
    }

    private void ValidateAssistants(List<Activity> exercises, List<Teacher> assistants)
    {
        foreach (Activity exercise in exercises)
        {
            if (assistants.Contains(exercise.Teacher)) continue;
                
            throw new ArgumentException("Subject exercises assistants are not matching subject assistants");
        }
    }
}