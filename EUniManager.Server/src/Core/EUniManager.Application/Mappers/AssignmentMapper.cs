using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class AssignmentMapper
{
    [MapperIgnoreTarget(nameof(Teacher.Id))]
    [MapperIgnoreTarget(nameof(Teacher.CreatedAt))]
    [MapperIgnoreTarget(nameof(Teacher.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Teacher.User))]
    [MapperIgnoreTarget(nameof(Teacher.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.Assignments))]
    public partial Assignment Map(CreateTeacherDto dto);
    
    [MapperIgnoreTarget(nameof(Teacher.Id))]
    [MapperIgnoreTarget(nameof(Teacher.CreatedAt))]
    [MapperIgnoreTarget(nameof(Teacher.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Teacher.User))]
    [MapperIgnoreTarget(nameof(Teacher.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.Assignments))]
    public partial Assignment Map(UpdateTeacherDto dto);
    
    public partial List<TeacherDto> Map(List<Assignment> entities);
    
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.Assignments))]
    public partial TeacherDetailsDto Map(Assignment entity);
}