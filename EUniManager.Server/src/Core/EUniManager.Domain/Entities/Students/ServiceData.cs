using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities.Students;

public class ServiceData
{
    public long Pin { get; set; }

    public StudentStatus Status { get; set; }

    public int PlanNumber { get; set; }

    public int FacultyNumber { get; set; }

    public byte GroupNumber { get; set; }

    public byte? EnrolledInSemester { get; set; }
}