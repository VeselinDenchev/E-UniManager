using EUniManager.Application.Extensions;
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

    [UserMapping]
    public StudentDetailsDto MapStudentToStudentDetailsDto(Student entity)
    {
        return new StudentDetailsDto
        {
            ServiceData = new()
            {
                Pin = entity.ServiceData.Pin,
                StudentStatus = GetStudentStatusString(entity.ServiceData.Status),
                PlanNumber = entity.ServiceData.PlanNumber,
                FacultyNumber = entity.ServiceData.FacultyNumber,
                GroupNumber = entity.ServiceData.GroupNumber,
                EnrolledInSemester = entity.ServiceData.EnrolledInSemester
            },
            PersonalData = new()
            {
                CityArea = new()
                {
                    City = entity.PersonalData.CityArea.City,
                    Area = entity.PersonalData.CityArea.Area
                },
                FirstName = entity.PersonalData.FirstName,
                MiddleName = entity.PersonalData.MiddleName,
                LastName = entity.PersonalData.LastName,
                UniqueIdentifier = new()
                {
                    Identifier = entity.PersonalData.UniqueIdentifier.Identifier,
                    UniqueIdentifierType = 
                        GetPersonalUniqueIdentifierTypeString(entity.PersonalData.UniqueIdentifier.UniqueIdentifierType)
                },
                InsuranceNumber = entity.PersonalData.InsuranceNumber,
                BirthDate = entity.PersonalData.BirthDate.ToBulgarianDateFormatString(),
                Gender = GetGenderString(entity.PersonalData.Gender),
                Citizenship = entity.PersonalData.Citizenship,
                IdCard = new()
                {
                    IdNumber = entity.PersonalData.IdCard?.IdNumber ?? string.Empty,
                    DateIssued = entity.PersonalData.IdCard?.DateIssued.ToBulgarianDateFormatString() ?? string.Empty
                },
                Email = entity.PersonalData.Email,
            },
            PermanentResidence = new()
            {
                CityArea = new()
                {
                    City = entity.PermanentResidence.CityArea.City,
                    Area = entity.PermanentResidence.CityArea.Area
                },
                Street = entity.PermanentResidence.Street,
                PhoneNumber = entity.PermanentResidence.PhoneNumber
            },
            TemporaryResidence = new()
            {
                CityArea = new()
                {
                    City = entity.TemporaryResidence.CityArea.City,
                    Area = entity.TemporaryResidence.CityArea.Area
                },
                Street = entity.TemporaryResidence.Street,
                PhoneNumber = entity.TemporaryResidence.PhoneNumber
            },
            UsualResidenceCountry = entity.UsualResidenceCountry,
            Enrollment = new()
            {
                Date = entity.Enrollment.Date.ToBulgarianDateFormatString(),
                Reason = entity.Enrollment.Reason,
                Mark = entity.Enrollment.Mark
            },
            DiplomaOwned = new()
            {
                EducationalAndQualificationDegree = 
                    GetEducationalAndQualificationalDegreeString(entity.DiplomaOwned.EducationalAndQualificationDegree),
                Series = entity.DiplomaOwned.Series,
                Number = entity.DiplomaOwned.Number,
                RegistrationNumber = entity.DiplomaOwned.RegistrationNumber,
                Date = entity.DiplomaOwned.Date.ToBulgarianDateFormatString(),
                Year = entity.DiplomaOwned.Year,
                IssuedByInstitutionType = GetInstitutionIssuerTypeString(entity.DiplomaOwned.IssuedByInstitutionType),
                InstitutionName = entity.DiplomaOwned.InstitutionName,
                CityArea = new()
                {
                    City = entity.DiplomaOwned.CityArea.City,
                    Area = entity.DiplomaOwned.CityArea.Area
                },
                Specialty = entity.DiplomaOwned.Specialty,
                ProfessionalQualification = entity.DiplomaOwned.ProfessionalQualification,
            }
        };
    }
    
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
    [MapProperty(nameof(@Student.ServiceData.Status), nameof(StudentHeaderDto.Status))]
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
    
    private string GetPersonalUniqueIdentifierTypeString(PersonalUniqueIdentifierType identifierType)
    {
        return identifierType switch
        {
            PersonalUniqueIdentifierType.Egn => "ЕГН",
            PersonalUniqueIdentifierType.Ssn => "SSN",
            _ => throw new ArgumentException("Unknown unique identifier type!")
        };
    }

    private string GetGenderString(Gender gender)
    {
        return gender switch
        {
            Gender.Male => "Мъж",
            Gender.Female => "Жена",
            _ => throw new ArgumentException("Invalid gender!")
        };
    }
    
    private string GetEducationalAndQualificationalDegreeString(EducationalAndQualificationDegree degree)
    {
        return degree switch
        {
            EducationalAndQualificationDegree.HighSchool => "Средно образование",
            EducationalAndQualificationDegree.Bachelor => "Бакалавър",
            EducationalAndQualificationDegree.Master => "Магистър",
            _ => throw new ArgumentException("Invalid degree!")
        };
    }
    
    private string GetInstitutionIssuerTypeString(InstitutionIssuerType issuer)
    {
        return issuer switch
        {
            InstitutionIssuerType.HighSchool => "Гимназия",
            InstitutionIssuerType.University => "Университет",
            _ => throw new ArgumentException("Invalid institution issuer type!")
        };
    }

    private string GetSpecialtyEducationTypeString(SpecialtyEducationType educationType)
    {
        return educationType switch
        {
            SpecialtyEducationType.FullTime => "Редовна",
            SpecialtyEducationType.PartTime => "Задочна",
            SpecialtyEducationType.Remote => "Дистанционна",
            _ => throw new ArgumentException("Invalid specialty education type!")
        };
    }
    
    private int GetCertifiedSemestersCount(List<CertifiedSemester> certifiedSemesters) => certifiedSemesters.Count;
}