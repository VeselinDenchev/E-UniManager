namespace EUniManager.Application.Models.Specialties.Dtos;

public sealed record FacultySpecialtyDto
{
    public string Name { get; set; } = null!;
    
    public byte CurrentYear { get; set; }
}