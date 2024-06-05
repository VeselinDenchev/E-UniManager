using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class TeacherMapper
{
    public partial List<TeacherDto> Map(List<Teacher> entities);
    
    [MapProperty(nameof(@Teacher.LecturingSubjects), nameof(TeacherDetailsDto.LecturingSubjects), 
                 Use = nameof(MapSubjectsToCurrentYearSubjectDtos))]
    [MapProperty(nameof(@Teacher.AssistingSubjects), nameof(TeacherDetailsDto.AssistingSubjects), 
                 Use = nameof(MapSubjectsToCurrentYearSubjectDtos))]
    public partial TeacherDetailsDto Map(Teacher entity);
    
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

    private List<CurrentYearSubjectDto> MapSubjectsToCurrentYearSubjectDtos(List<Subject> subjects)
    {
        return subjects.Select(s => new CurrentYearSubjectDto
        {
            Id = s.Id,
            SpecialtyName = s.Specialty.Name,
            SubjectCourseName = s.Course.Name,
            Semester = s.Semester
        }).ToList();
    }
}