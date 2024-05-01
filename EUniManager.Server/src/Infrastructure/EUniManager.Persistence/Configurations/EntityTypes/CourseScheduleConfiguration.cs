using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class CourseScheduleConfiguration : BaseEntityConfiguration<CourseSchedule, Guid>
{
    public override void Configure(EntityTypeBuilder<CourseSchedule> entity)
    {
        base.Configure(entity);

        entity.HasMany(cs => cs.Units).WithMany(u => u.CourseSchedules);
        
        entity.HasMany(cs => cs.Students).WithOne(s => s.CourseSchedule);
    }
}