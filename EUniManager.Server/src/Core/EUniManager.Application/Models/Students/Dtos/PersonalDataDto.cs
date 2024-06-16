using EUniManager.Application.Models.Students.Dtos.IdCard;

namespace EUniManager.Application.Models.Students.Dtos;

public record PersonalDataDto
{
    public CityAreaDto CityArea { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    
    public string MiddleName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public UniqueIdentifierDto UniqueIdentifier { get; set; } = null!;

    public string? InsuranceNumber { get; set; }

    public string? BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Citizenship { get; set; } = null!;

    public IdCardDto? IdCard { get; set; }

    public string Email { get; set; } = null!;
}