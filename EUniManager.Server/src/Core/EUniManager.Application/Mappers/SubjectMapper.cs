using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class SubjectMapper
{
    [UserMapping]
    public List<SubjectDto> MapSubjectsToSubjectDtos(List<Subject> entities)
    {
        return entities.Select(s => new SubjectDto
        {
            Id = s.Id,
            CourseName = s.Course.Name,
            FacultyName = s.Specialty.Faculty.Name,
            SpecialtyName = s.Specialty.Name,
            LecturerFullNameWithRank = s.Lecturer.FullNameWithRank
        }).ToList();
    }
    
    [MapProperty(nameof(@Subject.Course.Name), nameof(SubjectDetailsDto.CourseName))]
    [MapProperty(nameof(@Subject.Specialty.Faculty.Name), nameof(SubjectDetailsDto.FacultyName))]
    [MapProperty(nameof(@Subject.Specialty.Name), nameof(SubjectDetailsDto.SpecialtyName))]
    [MapProperty(nameof(@Subject.Lecturer.FullNameWithRank), nameof(SubjectDetailsDto.LecturerFullNameWithRank))]
    [MapProperty(nameof(Subject.Assistants), 
                 nameof(SubjectDetailsDto.AssistantFullNamesWithRank),
                 Use = nameof(MapAssistantsToAssistantFullNamesWithRank))]
    public partial SubjectDetailsDto Map(Subject entity);
    
    [MapProperty(nameof(@Subject.Course.Name), nameof(StudentSubjectInfoDto.CourseName))]
    [MapProperty(nameof(Subject.Marks), nameof(StudentSubjectInfoDto.MarkWithWords), Use = nameof(MapMarkToMarkWithWords))]
    [MapProperty(nameof(Subject.Marks), nameof(StudentSubjectInfoDto.MarkNumeric), Use = nameof(MapMarkToMarkNumeric))]
    [MapProperty(nameof(@Subject.Lecturer.FullNameWithRank), nameof(StudentSubjectInfoDto.LecturerFullNameWithRank))]
    public partial List<StudentSubjectInfoDto> MapSubjectsToStudentSubjectResultDtos(List<Subject> entities);

    [MapperIgnoreTarget(nameof(Subject.Id))]
    [MapperIgnoreTarget(nameof(Subject.CreatedAt))]
    [MapperIgnoreTarget(nameof(Subject.ModifiedAt))]
    [MapperIgnoreTarget(nameof(Subject.Course))]
    [MapperIgnoreTarget(nameof(Subject.Students))]
    [MapperIgnoreTarget(nameof(Subject.Lecturer))]
    [MapperIgnoreTarget(nameof(Subject.Assistants))]
    [MapperIgnoreTarget(nameof(Subject.Specialty))]
    [MapperIgnoreTarget(nameof(Subject.Activities))]
    public partial Subject Map(CreateSubjectDto dto);

    private string GetFacultyName(Specialty specialty) => specialty.Faculty.Name;
    
    private List<string> MapAssistantsToAssistantFullNamesWithRank(List<Teacher> assistants)
        => assistants.Select(a => a.FullNameWithRank).ToList();

    private string MapMarkToMarkWithWords(List<SubjectMark> subjectMarks)
    {
        Mark? mark = subjectMarks.FirstOrDefault()?.Mark;
        
        return mark switch
        {
            Mark.Poor => "Слаб",
            Mark.Sufficient => "Среден",
            Mark.Good => "Добър",
            Mark.VeryGood => "Много добър",
            Mark.Excellent => "Отличен",
            _ => "Неположен"
        };
    }

    private byte? MapMarkToMarkNumeric(List<SubjectMark> subjectMarks)
    {
        Mark? mark = subjectMarks.FirstOrDefault()?.Mark;
        
        return mark.HasValue ? (byte)mark : null;
    }
}