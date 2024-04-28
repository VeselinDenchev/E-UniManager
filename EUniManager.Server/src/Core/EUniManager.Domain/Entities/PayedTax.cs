using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class PayedTax : BaseEntity<Guid>
{
    public Student Student { get; set; } = null!;
    
    public int TaxNumber { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateOnly DocumentDate { get; set; }

    public byte Semester { get; set; }

    public int PlanNumber { get; set; }

    public short Amount { get; set; }

    public Currency Currency { get; set; }
}