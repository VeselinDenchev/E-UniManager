using System.Diagnostics;

using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class StudentMapper
{
    [UserMapping]
    public List<StudentDto> Map(List<Student> entities)
    {
        return entities.Select(s => new StudentDto
        {
            Id = s.Id,
            Pin = s.ServiceData.Pin,
            FacultyNumber = s.ServiceData.FacultyNumber,
            FullName = s.FullName,
            FacultyName = s.Specialty.Faculty.Name,
            SpecialtyName = s.Specialty.Name,
            StudentStatus = GetStudentStatusString(s.ServiceData.Status),
            EnrolledInSemester = s.ServiceData.EnrolledInSemester
        }).ToList();
    }
    
    [MapProperty(nameof(@Student.ServiceData.Pin), nameof(@StudentDetailsDto.ServiceData.Pin))]
    [MapProperty(nameof(@Student.ServiceData.Status),
                 nameof(@StudentDetailsDto.ServiceData.StudentStatus),
                 Use = nameof(GetSpecialtyEducationTypeString))]
    [MapProperty(nameof(@Student.ServiceData.PlanNumber), nameof(@StudentDetailsDto.ServiceData.PlanNumber))]
    [MapProperty(nameof(@Student.ServiceData.FacultyNumber), nameof(@StudentDetailsDto.ServiceData.FacultyNumber))]
    [MapProperty(nameof(@Student.ServiceData.GroupNumber), nameof(@StudentDetailsDto.ServiceData.GroupNumber))]
    [MapProperty(nameof(@Student.ServiceData.EnrolledInSemester), nameof(@StudentDetailsDto.ServiceData.EnrolledInSemester))]
    public partial StudentDetailsDto MapStudentToStudentDetailsDto(Student entity);
    
    [MapProperty(nameof(@CreateStudentDto.ServiceData.Pin), nameof(@Student.ServiceData.Pin))]
    [MapProperty(nameof(@CreateStudentDto.ServiceData.StudentStatus), nameof(@Student.ServiceData.Status))]
    [MapProperty(nameof(@CreateStudentDto.ServiceData.PlanNumber), nameof(@Student.ServiceData.PlanNumber))]
    [MapProperty(nameof(@CreateStudentDto.ServiceData.FacultyNumber), nameof(@Student.ServiceData.FacultyNumber))]
    [MapProperty(nameof(@CreateStudentDto.ServiceData.GroupNumber), nameof(@Student.ServiceData.GroupNumber))]
    [MapProperty(nameof(@CreateStudentDto.ServiceData.EnrolledInSemester), nameof(@Student.ServiceData.EnrolledInSemester))]
    public partial Student Map(CreateStudentDto dto);

    [MapProperty(nameof(@Student.ServiceData.Pin), nameof(StudentHeaderDto.Pin))]
    [MapProperty(nameof(@Student.ServiceData.PlanNumber), nameof(StudentHeaderDto.PlanNumber))]
    [MapProperty(nameof(@Student.ServiceData.FacultyNumber), nameof(StudentHeaderDto.FacultyNumber))]
    [MapProperty(nameof(@Student.ServiceData.GroupNumber), nameof(StudentHeaderDto.GroupNumber))]
    [MapProperty(nameof(@Student.Specialty.Name), nameof(StudentHeaderDto.SpecialtyName))]
    [MapProperty(nameof(@Student.Specialty.EducationType), nameof(StudentHeaderDto.SpecialtyEducationType))]
    [MapProperty(nameof(Student.CertifiedSemesters), 
                 nameof(StudentHeaderDto.CertifiedSemesterCount), 
                 Use = nameof(GetCertifiedSemestersCount))]
    public partial StudentHeaderDto MapStudentToStudentHeaderDto(Student entity);

    private string GetStudentStatusString(StudentStatus status)
    {
        return status switch
        {
            StudentStatus.Studying => "Учи",
            StudentStatus.Interrupted => "Прекъснал",
            StudentStatus.Graduated => "Завършил",
            _ => throw new ArgumentException("Unknown student status!")
        };
    }

    private string GetSpecialtyEducationTypeString(SpecialtyEducationType educationType)
    {
        return educationType switch
        {
            SpecialtyEducationType.FullTime => "Редовна",
            SpecialtyEducationType.PartTime => "Задочна",
            _ => throw new ArgumentException("Invalid specialty education type!")
        };
    }
    
    private int GetCertifiedSemestersCount(List<CertifiedSemester> certifiedSemesters) => certifiedSemesters.Count;
}