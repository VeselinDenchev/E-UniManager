using System.ComponentModel.DataAnnotations;

namespace EUniManager.Domain.Enums;

public enum AssignmentType
{
    [Display(Name = "Текст")]
    Text = 0,
    [Display(Name = "Файл")]
    File = 1
}