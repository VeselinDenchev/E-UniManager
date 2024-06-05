using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Application.Models.Courses.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class CourseService(IEUniManagerDbContext dbContext)
    : BaseService<Course, Guid, CourseDto, CourseDetailsDto>(dbContext), ICourseService
{
    private readonly CourseMapper _courseMapper = new();

    public override async Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Course> courseEntities = await GetAllEntitiesAsync(cancellationToken);
        List<CourseDto> courseDtos = _courseMapper.Map(courseEntities);

        return courseDtos;
    }

    public override async ValueTask<CourseDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Course courseEntity = await _dbSet.AsNoTracking()
                                          .Include(c => c.Subjects)
                                          .FirstOrDefaultAsync(c => c.Id == id, cancellationToken) ??
                              throw new ArgumentException($"Such {nameof(Course).ToLowerInvariant()} doesn't exist!");
        
        CourseDetailsDto courseDto = _courseMapper.Map(courseEntity);

        return courseDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        Course course = _courseMapper.Map((dto as CreateCourseDto)!);
        
        await CreateEntityAsync(course, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        bool exists = await ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            throw new ArgumentException($"Such {nameof(Course).ToLowerInvariant()} doesn't exist");
        }
        
        Course course = _courseMapper.Map((dto as UpdateCourseDto)!);
        
        course.Id = id;
        
        _dbSet.Update(course);
        SetNotModifiedPropertiesOnUpdate(course);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override void SetNotModifiedPropertiesOnUpdate(Course entity)
    {
        base.SetNotModifiedPropertiesOnUpdate(entity);
        
        _dbContext.Entry(entity).Property(e => e.Name).IsModified = false;
        _dbContext.Entry(entity).Collection(e => e.Subjects).IsModified = false;
    }
}