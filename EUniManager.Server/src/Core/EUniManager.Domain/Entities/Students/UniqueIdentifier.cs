using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities.Students;

public class UniqueIdentifier
{
    public PersonalUniqueIdentifierType UniqueIdentifierType { get; set; }

    public string Identifier { get; set; } = null!;
}