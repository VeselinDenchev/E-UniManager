using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.PayedTaxes.Dtos;

public record UpdatePayedTaxDto : IUpdateDto
{
    public string DocumentNumber { get; set; } = null!;

    public DateOnly DocumentDate { get; set; }

    public short Amount { get; set; }

    public string Currency { get; set; } = null!;
}