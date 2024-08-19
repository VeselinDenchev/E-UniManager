namespace EUniManager.Domain.Entities.Students;

public class Enrollment
{
    public DateOnly Date { get; set; }

    public string Reason { get; set; } = null!;  // could be enum

    public float Mark { get; set; }
}