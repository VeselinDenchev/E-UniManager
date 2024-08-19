namespace EUniManager.Domain.Entities.Students;

public class Residence
{
    public CityArea CityArea { get; set; } = null!;
    
    public string? Street { get; set; }

    public string? PhoneNumber { get; set; }
}