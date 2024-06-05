using EUniManager.Application.Extensions;
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Application.Models.CourseScheduleUnits.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class CourseScheduleUnitService 
    : BaseService<CourseScheduleUnit, Guid, IEntityDto, CourseScheduleUnitDetailsDto>, 
      ICourseScheduleUnitService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CourseScheduleUnitMapper _courseSheduleUnitMapper = new();
    
    public CourseScheduleUnitService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public override Task<List<IEntityDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<CourseScheduleUnitDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        CourseScheduleUnit scheduleUnitEntity = await _dbSet.Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                                                                         .ThenInclude(s => s.Course)
                                                            .Include(csu => csu.Activity).ThenInclude(a => a.Teacher)
                                                            .FirstOrDefaultAsync(csu => csu.Id == id, cancellationToken) ?? 
                                                throw new ArgumentException("Such schedule unit doesn't exist!");

        CourseScheduleUnitDetailsDto scheduleUnitDto = _courseSheduleUnitMapper.Map(scheduleUnitEntity);

        return scheduleUnitDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createCourseScheduleUnitDto = (dto as CreateCourseScheduleUnitDto)!;
        CourseScheduleUnit scheduleUnit = _courseSheduleUnitMapper.Map(createCourseScheduleUnitDto);
        
        scheduleUnit.Activity = await _dbContext.Activities.FindAsync(createCourseScheduleUnitDto.ActivityId, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Activity).ToLowerInvariant()} doesn't exist!");

        await CreateEntityAsync(scheduleUnit, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            throw new ArgumentException("Such schedule unit doesn't exist");
        }

        var updateCourseScheduleUnitDto = (dto as UpdateCourseScheduleUnitDto)!;
        CourseScheduleUnit scheduleUnit = _courseSheduleUnitMapper.Map(updateCourseScheduleUnitDto);
        
        scheduleUnit.Id = id;
        scheduleUnit.Activity = await _dbContext.Activities.FindAsync(updateCourseScheduleUnitDto.ActivityId, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Teacher).ToLowerInvariant()} doesn't exist!");
        
        SetNotModifiedPropertiesOnUpdate(scheduleUnit);

        await UpdateEntityAsync(scheduleUnit, cancellationToken);
    }

    public async Task<List<StudentCourseScheduleUnitDto>> GetSpecialtyScheduleByStudentAsync
    (
        SemesterType requestedSemesterType,
        CancellationToken cancellationToken
    )
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        int currentCalendarYear = DateTime.Now.Year;
        bool isRequestedPreviousYearSummerSemester =
            await IsRequestedPreviousYearSummerSemesterAsync(requestedSemesterType, currentCalendarYear, cancellationToken);
        
        List<CourseScheduleUnit> courseScheduleUnits = (await _dbSet
                .AsNoTracking()
                .Include(csu => csu.Activity)
                .Include(csu => csu.Activity).ThenInclude(a => a.Teacher)
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Specialty)
                                             .ThenInclude(s => s.Students)!
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Course)
                .ToListAsync(cancellationToken))
                .Where(csu => csu.Activity.Subject.Specialty.Students is not null && 
                              csu.Activity.Subject.Specialty.Students.Any(s => s.Id == studentId) && 
                              (isRequestedPreviousYearSummerSemester
                                  ? csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear
                                  : csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear - 1) 
                              &&
                              csu.SemesterType == requestedSemesterType)
                .ToScheduleOrderedList();
        
        List<StudentCourseScheduleUnitDto> specialtySchedule = 
            _courseSheduleUnitMapper.MapCourseScheduleUnitsToStudentSchedule(courseScheduleUnits);

        return specialtySchedule;
    }
    
    public async Task<List<StudentCourseScheduleUnitDto>> GetStudentScheduleAsync(SemesterType requestedSemesterType,
                                                                                  CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        int currentCalendarYear = DateTime.Now.Year;
        bool isRequestedPreviousYearSummerSemester =
            await IsRequestedPreviousYearSummerSemesterAsync(requestedSemesterType, currentCalendarYear, cancellationToken);
        
        List<CourseScheduleUnit> courseScheduleUnits = (await _dbSet
                .AsNoTracking()
                .Include(csu => csu.Activity).ThenInclude(a => a.Teacher)
                .Include(csu => csu.Activity).ThenInclude(a => a.Students)
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Specialty)
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Course)
                .ToListAsync(cancellationToken))
                .Where(csu => csu.Activity.Students.Any(s => s.Id == studentId) &&
                              (isRequestedPreviousYearSummerSemester
                                  ? csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear
                                  : csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear - 1)
                              &&
                              csu.SemesterType == requestedSemesterType)
                .ToScheduleOrderedList();
        
        List<StudentCourseScheduleUnitDto> specialtySchedule = 
            _courseSheduleUnitMapper.MapCourseScheduleUnitsToStudentSchedule(courseScheduleUnits);

        return specialtySchedule;
    }
    
    public async Task<List<TeacherCourseScheduleUnitDto>> GetTeacherScheduleAsync(SemesterType requestedSemesterType,
                                                                                  CancellationToken cancellationToken)
    {
        Guid teacherId = await GetTeacherIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        int currentCalendarYear = DateTime.Now.Year;
        bool isRequestedPreviousYearSummerSemester =
            await IsRequestedPreviousYearSummerSemesterAsync(requestedSemesterType, currentCalendarYear, cancellationToken);
        
        List<CourseScheduleUnit> courseScheduleUnits = (await _dbSet
                .AsNoTracking()
                .Include(csu => csu.Activity).ThenInclude(a => a.Teacher).ThenInclude(t => t.User)
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Course)
                .Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                             .ThenInclude(s => s.Specialty)
                .ToListAsync(cancellationToken))
                .Where(csu => csu.Activity.Teacher.Id == teacherId &&
                              isRequestedPreviousYearSummerSemester
                                  ? csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear
                                  : csu.Activity.Subject.Specialty.CurrentAcademicYearStart == currentCalendarYear - 1
                              &&
                              csu.SemesterType == requestedSemesterType)
                .ToScheduleOrderedList();

        List<TeacherCourseScheduleUnitDto> specialtySchedule = 
            _courseSheduleUnitMapper.MapCourseScheduleUnitsToTeacherSchedule(courseScheduleUnits);

        return specialtySchedule;
    }
    
    public async Task DeleteOldSchedulesAsync(CancellationToken cancellationToken)
    {
        await _dbSet.Include(csu => csu.Activity).ThenInclude(a => a.Subject)
                                                 .ThenInclude(s => s.Specialty)
                    .Where(csu => csu.Activity.Subject.Specialty.CurrentAcademicYearStart < DateTime.Now.Year - 2)
                    .ExecuteDeleteAsync(cancellationToken);
    }
    
    protected override void SetNotModifiedPropertiesOnUpdate(CourseScheduleUnit entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(csu => csu.MonthlyFrequency).IsModified = false;
    }

    private async Task<bool> IsRequestedPreviousYearSummerSemesterAsync(SemesterType requestedSemesterType,
                                                                        int currentCalendarYear,
                                                                        CancellationToken cancellationToken)
    {
        SemesterType currentSemesterType = (await _dbContext.Specialties.AsNoTracking()
                                                                        .ToListAsync(cancellationToken))
                                                                        .Any(s => s.CurrentAcademicYearStart == currentCalendarYear)
                                                                             ? SemesterType.Winter
                                                                             : SemesterType.Summer;

        bool isRequestedPreviousYearSummerSemester = currentSemesterType == SemesterType.Winter && 
                                                     requestedSemesterType == SemesterType.Summer;

        return isRequestedPreviousYearSummerSemester;
    }
}