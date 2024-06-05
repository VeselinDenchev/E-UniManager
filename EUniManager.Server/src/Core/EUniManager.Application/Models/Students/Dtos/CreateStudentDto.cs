using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Students.Dtos;

namespace EUniManager.Application.Models.Students.Dtos;

public sealed record CreateStudentDto : ICreateDto
{
    public ServiceDataDto ServiceData { get; set; } = null!;

    public PersonalDataDto PersonalData { get; set; } = null!;

    public ResidenceDto PermanentResidence { get; set; } = null!;

    public ResidenceDto TemporaryResidence { get; set; } = null!;

    public string UsualResidenceCountry { get; set; } = null!;

    public EnrollmentDto Enrollment { get; set; } = null!;

    public DiplomaDto DiplomaOwned { get; set; } = null!;

    public Guid SpecialtyId { get; set; }
}