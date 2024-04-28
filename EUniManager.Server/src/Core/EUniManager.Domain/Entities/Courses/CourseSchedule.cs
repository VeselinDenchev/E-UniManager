using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Domain.Entities.Courses;

public class CourseSchedule : BaseEntity<Guid>
{
    public List<CourseScheduleUnit> Units { get; set; } = null!;

    public List<Student> Students { get; set; } = null!;
}