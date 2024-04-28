using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities.Students;

public class PersonalData
{
    public CityArea CityArea { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string MiddleName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public UniqueIdentifier UniqueIdentifier { get; set; } = null!;

    public string? InsuraceNumber { get; set; }

    public DateOnly BirthDate { get; set; }

    public Gender Gender { get; set; }

    public string Citizienship { get; set; } = null!;

    public IdCard? IdCard { get; set; } = null!;

    public string Email { get; set; }
}