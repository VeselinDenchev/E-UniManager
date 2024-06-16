namespace EUniManager.Application.Models.Students.Dtos;

public sealed record CityAreaDto
{
    public string? City { get; set; } = null!;
    
    public string? Area { get; set; } = null!;
}