namespace EUniManager.Application.Models.Students.Dtos;

public sealed record ResidenceDto
{
    public CityAreaDto CityArea { get; set; } = null!;
    
    public string? Street { get; set; }

    public string? PhoneNumber { get; set; }
}