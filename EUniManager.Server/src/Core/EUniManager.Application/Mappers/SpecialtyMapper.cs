using EUniManager.Application.Extensions;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class SpecialtyMapper
{
    [MapProperty(nameof(@Specialty.Faculty.Name), nameof(SpecialtyDto.FacultyName))]
    [MapProperty(nameof(Specialty.EducationalAndQualificationDegree), nameof(SpecialtyDto.EducationalAndQualificationDegree),
        Use = nameof(EducationAndQualificationDegreeToBulgarianString))]
    public partial List<SpecialtyDto> Map(List<Specialty> entities);
    
    [MapProperty(nameof(@Specialty.Faculty.Name), nameof(SpecialtyDetailsDto.FacultyName))]
    [MapProperty(nameof(Specialty.Name), nameof(SpecialtyDetailsDto.Name))]
    [MapProperty(nameof(Specialty.EducationalAndQualificationDegree), nameof(SpecialtyDetailsDto.EducationalAndQualificationDegree), 
        Use = nameof(EducationAndQualificationDegreeToBulgarianString))]
    public partial SpecialtyDetailsDto Map(Specialty entity);
    
    [MapperIgnoreTarget(nameof(Specialty.Id))]
    [MapperIgnoreTarget(nameof(Specialty.CreatedAt))]
    [MapperIgnoreTarget(nameof(Specialty.ModifiedAt))]
    public partial Specialty Map(CreateSpecialtyDto dto);

    private List<FacultyStudentDto> MapStudentsToStudentIds(List<Student> students) 
        => students.Select(s => new FacultyStudentDto
           {
               Id = s.Id,
               Pin = s.ServiceData.Pin,
               FullName = s.FullName
           }).ToList();

    private string EducationAndQualificationDegreeToBulgarianString(EducationalAndQualificationDegree degree)
        => degree.ToBulgarianString();
}