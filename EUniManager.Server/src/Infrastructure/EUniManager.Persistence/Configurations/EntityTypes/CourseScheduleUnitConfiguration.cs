using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.SemesterConstant;

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

        entity.HasOne(u => u.Subject).WithMany(s => s.CourseScheduleUnits);

        entity.Property(u => u.Semester).IsRequired();

        entity.HasMany(u => u.CourseSchedules).WithMany(cs => cs.Units);

        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = [nameof(CourseScheduleUnit), nameof(CourseScheduleUnit.Semester)];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                $"{nameof(CourseScheduleUnit.Semester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
        });
    }
}