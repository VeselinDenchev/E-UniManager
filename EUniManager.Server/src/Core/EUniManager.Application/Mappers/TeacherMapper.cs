using System.Collections;

using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class TeacherMapper
{
    [MapperIgnoreTarget(nameof(Teacher.Id))]
    [MapperIgnoreTarget(nameof(Teacher.CreatedAt))]
    [MapperIgnoreTarget(nameof(Teacher.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Teacher.User))]
    [MapperIgnoreTarget(nameof(Teacher.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.Assignments))]
    public partial Teacher Map(CreateTeacherDto dto);
    
    [MapperIgnoreTarget(nameof(Teacher.Id))]
    [MapperIgnoreTarget(nameof(Teacher.CreatedAt))]
    [MapperIgnoreTarget(nameof(Teacher.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Teacher.User))]
    [MapperIgnoreTarget(nameof(Teacher.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(Teacher.Assignments))]
    public partial Teacher Map(UpdateTeacherDto dto);
    
    public partial List<TeacherDto> Map(List<Teacher> entities);
    
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.LecturingSubjects))]
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.AssistingSubjects))]
    [MapperIgnoreTarget(nameof(TeacherDetailsDto.Assignments))]
    public partial TeacherDetailsDto Map(Teacher entity);
}