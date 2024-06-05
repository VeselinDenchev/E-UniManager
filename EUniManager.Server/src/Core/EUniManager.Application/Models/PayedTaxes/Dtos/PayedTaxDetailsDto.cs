using EUniManager.Application.Models.Base.Interfaces;

namespace EUniManager.Application.Models.PayedTaxes.Dtos;

public sealed record PayedTaxDetailsDto : IDetailsDto
{
    public long FacultyNumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int TaxNumber { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateOnly DocumentDate { get; set; }

    public byte Semester { get; set; }

    public int PlanNumber { get; set; }

    public short Amount { get; set; }

    public string Currency { get; set; } = null!;
}