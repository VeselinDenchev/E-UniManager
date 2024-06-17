using EUniManager.Application.Extensions;
using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class ActivityMapper
{
    [UserMapping]
    public List<ActivityDto> Map(List<Activity> entities)
    {
        return entities.Select(a => new ActivityDto
        {
            Id = a.Id,
            CreatedAt = DateOnly.FromDateTime(a.CreatedAt).ToBulgarianDateFormatString(),
            Semester = a.Subject.Semester,
            ActivityType = MapActivityTypeToString(a.Type),
            TeacherFullNameWithRank = a.Teacher.FullNameWithRank,
            SubjectCourseName = a.Subject.Course.Name,
            IsStopped = a.IsStopped
        }).ToList();
    }
    
    [MapProperty(nameof(Activity.Type), nameof(ActivityDetailsDto.ActivityType), Use = nameof(MapActivityTypeToString))]
    [MapProperty(nameof(@Activity.Teacher.FullNameWithRank), nameof(ActivityDetailsDto.TeacherFullNameWithRank))]
    [MapProperty(nameof(@Activity.Subject.Course.Name), nameof(ActivityDetailsDto.SubjectCourseName))]
    public partial ActivityDetailsDto Map(Activity entity);
    
    [MapperIgnoreTarget(nameof(Activity.Id))]
    [MapperIgnoreTarget(nameof(Activity.CreatedAt))]
    [MapperIgnoreTarget(nameof(Activity.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Activity.Teacher))]
    [MapperIgnoreTarget(nameof(Activity.Subject))]
    [MapProperty(nameof(CreateActivityDto.ActivityType), nameof(Activity.Type))]
    public partial Activity Map(CreateActivityDto dto);
    
    [MapperIgnoreTarget(nameof(Activity.Id))]
    [MapperIgnoreTarget(nameof(Activity.CreatedAt))]
    [MapperIgnoreTarget(nameof(Activity.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Activity.Teacher))]
    [MapperIgnoreTarget(nameof(Activity.Subject))]
    [MapperIgnoreTarget(nameof(Activity.Teacher))]
    public partial Activity Map(UpdateActivityDto dto);

    private string MapActivityTypeToString(ActivityType activityType)
    {
        return activityType switch
        {
            ActivityType.Lecture => "Лекция",
            ActivityType.Exercise => "Упражнение",
            _ => throw new ArgumentException("Invalid specialty activity type!")
        };
    }
}