using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class CourseScheduleUnitConfiguration : BaseEntityConfiguration<CourseScheduleUnit, Guid>
{
    public override void Configure(EntityTypeBuilder<CourseScheduleUnit> entity)
    {
        base.Configure(entity);

        entity.Property(u => u.DayOfWeek).IsRequired(false);
        
        entity.Property(u => u.ExactDate).IsRequired(false);

        entity.Property(u => u.StartTime).IsRequired();

        entity.Property(u => u.MonthlyFrequency).IsRequired(false);

        entity.Property(u => u.GroupType).IsRequired(false);

        entity.Property(u => u.GroupNumber).IsRequired(false);

        entity.Property(u => u.Place).IsRequired();
    }
}