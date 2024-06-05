using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Students.Dtos;

public sealed record StudentDetailsDto : IDetailsDto
{
    public ServiceDataDto ServiceData { get; set; } = new();

    public PersonalDataDto PersonalData { get; set; } = new();

    public ResidenceDto PermanentResidence { get; set; } = new();

    public ResidenceDto TemporaryResidence { get; set; } = new();

    public string UsualResidenceCountry { get; set; } = null!;

    public EnrollmentDto Enrollment { get; set; } = new();

    public DiplomaDto DiplomaOwned { get; set; } = new();
}