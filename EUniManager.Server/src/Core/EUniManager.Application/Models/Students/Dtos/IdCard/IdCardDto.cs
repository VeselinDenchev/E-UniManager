namespace EUniManager.Application.Models.Students.Dtos.IdCard;

public sealed record IdCardDto
{
    public string IdNumber { get; set; } = null!;

    public string DateIssued { get; set; } = null!;
}