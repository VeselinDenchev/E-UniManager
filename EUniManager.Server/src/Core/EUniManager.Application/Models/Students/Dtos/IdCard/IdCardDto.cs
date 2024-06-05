namespace EUniManager.Application.Models.Students.Dtos.IdCard;

public sealed record IdCardDto
{
    public string IdNumber { get; set; } = null!;
    
    public DateOnly DateIssued { get; set; }
}