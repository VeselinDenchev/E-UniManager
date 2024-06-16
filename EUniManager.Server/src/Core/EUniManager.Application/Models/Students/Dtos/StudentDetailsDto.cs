using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.Students.Dtos;

public sealed record StudentDetailsDto : IDetailsDto
{
    public required ServiceDataDto ServiceData { get; set; } = new();

    public required PersonalDataDto PersonalData { get; set; } = new();

    public required ResidenceDto PermanentResidence { get; set; } = new();

    public required ResidenceDto TemporaryResidence { get; set; } = new();

    public required string UsualResidenceCountry { get; set; } = null!;

    public required EnrollmentDto Enrollment { get; set; } = new();

    public required DiplomaDto DiplomaOwned { get; set; } = new();
}