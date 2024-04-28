using EUniManager.Domain.Abstraction.Student;

namespace EUniManager.Domain.Abstraction.Student;

public class Residence
{
    public CityArea CityArea { get; set; } = null!;
    
    public string? Street { get; set; }

    public string? PhoneNumber { get; set; }
}