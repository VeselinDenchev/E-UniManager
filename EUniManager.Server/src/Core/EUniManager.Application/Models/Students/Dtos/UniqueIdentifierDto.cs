namespace EUniManager.Application.Models.Students.Dtos;

public sealed record UniqueIdentifierDto
{
    public string UniqueIdentifierType { get; set; } = null!;

    public string Identifier { get; set; } = null!;
}