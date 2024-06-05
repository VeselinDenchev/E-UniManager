using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.PayedTaxes.Dtos;

public sealed record CreatePayedTaxDto : ICreateDto
{
    public Guid StudentId { get; set; }
    
    public int TaxNumber { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateOnly DocumentDate { get; set; }

    public byte Semester { get; set; }

    public int PlanNumber { get; set; }

    public short Amount { get; set; }

    public string Currency { get; set; } = null!;
}