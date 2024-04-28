using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Exam : BaseEntity<Guid>
{
    public Subject Subject { get; set; } = null!;

    public ExamType Type { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public SchedulePlace Place { get; set; }

    public short RoomNumber { get; set; }

    public byte? GroupNumber { get; set; }
}