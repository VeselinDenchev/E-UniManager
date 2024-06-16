using EUniManager.Application.Extensions;
using EUniManager.Application.Models.CertifiedSemesters.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class CertifiedSemesterMapper
{
    [UserMapping]
    public List<StudentCertifiedSemesterDto> Map(List<CertifiedSemester> certifiedSemester)
    {
        return certifiedSemester.Select(cs => new StudentCertifiedSemesterDto
        {
            Id = cs.Id,
            Semester = cs.Semester,
            StudentPin = cs.Student.ServiceData.Pin,
            EducationType = GetEducationTypeString(cs.Student.Specialty.EducationType),
            Date = cs.CreatedAt.ToBulgarianDateTimeFormatString()
        }).ToList();
    }

    private string GetEducationTypeString(SpecialtyEducationType educationType)
    {
        return educationType switch
        {
            SpecialtyEducationType.FullTime => "Редовна",
            SpecialtyEducationType.PartTime => "Задочна",
            _ => throw new ArgumentException("Such education type doesn't exist!")
        };
    }
}