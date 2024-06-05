using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.PayedTaxes.Dtos;

public sealed record PayedTaxDto : IEntityDto
{
    public required Guid Id { get; set; }

    public required long FacultyNumber { get; set; }
    
    public required int TaxNumber { get; set; }

    public required string DocumentNumber { get; set; } = null!;

    public required  string DocumentDate { get; set; } = null!;

    public required  byte Semester { get; set; }

    public required int PlanNumber { get; set; }

    public required short Amount { get; set; }

    public required string Currency { get; set; } = null!;
}