using System.ComponentModel.DataAnnotations;

namespace EUniManager.Domain.Enums;

public enum ExamType
{
    Regular = 0,
    Remedial = 1,
    Liquidation = 2,
    Written = 3,
    Oral = 4
}