using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class CourseMapper
{
    public partial List<CourseDto> Map(List<Course> entities);
    
    [MapProperty(nameof(@Course.Subjects),nameof(CourseDetailsDto.SubjectsCount), Use = nameof(GetSubjectsCount))]
    public partial CourseDetailsDto Map(Course entity);
    
    [MapperIgnoreTarget(nameof(Course.Id))]
    [MapperIgnoreTarget(nameof(Course.CreatedAt))]
    [MapperIgnoreTarget(nameof(Course.ModifiedAt))]
    // [MapperIgnoreTarget(nameof(Course.Student))]
    public partial Course Map(CreateCourseDto dto);
    
    [MapperIgnoreTarget(nameof(Course.Id))]
    [MapperIgnoreTarget(nameof(Course.CreatedAt))]
    [MapperIgnoreTarget(nameof(Course.ModifiedAt))]
    public partial Course Map(UpdateCourseDto dto);

    private int GetSubjectsCount(List<Subject> subjects) => subjects.Count;
}