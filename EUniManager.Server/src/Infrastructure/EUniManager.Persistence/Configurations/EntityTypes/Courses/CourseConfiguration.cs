using EUniManager.Domain.Entities.Courses;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.Courses.CourseConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes.Courses;

public sealed class CourseConfiguration : BaseEntityConfiguration<Course, Guid>
{
    public override void Configure(EntityTypeBuilder<Course> entity)
    {
        base.Configure(entity);

        entity.Property(c => c.Name).IsRequired()
                                    .IsUnicode()
                                    .HasMaxLength(NAME_MAX_STRING_LENGTH);

        entity.Property(c => c.LecturesCount).IsRequired();

        entity.Property(c => c.ExercisesCount).IsRequired();

        entity.Property(c => c.Mark).IsRequired(false);

        entity.Property(c => c.CreditsCount).IsRequired();

        entity.Property(c => c.Protocol).IsRequired()
                                        .IsUnicode(false)
                                        .HasMaxLength(PROCOTOL_MAX_STRING_LENGTH);

        entity.HasMany(c => c.Subjects).WithOne(s => s.Course);

        entity.ToTable(BuildCheckConstraints);
    }

    private void BuildCheckConstraints(TableBuilder<Course> coursesTable)
    {
        string[] checkConstraintTokens = [nameof(Course), nameof(Course.LecturesCount)];
        string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
        coursesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Course.LecturesCount)} >= 0");

        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(Course.LecturesCount), nameof(Course.ExercisesCount));
        coursesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Course.ExercisesCount)} >= 0");
        
        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(Course.LecturesCount), nameof(Course.CreditsCount));
        coursesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Course.CreditsCount)} BETWEEN {MIN_CREDITS_COUNT} AND {MAX_CREDITS_COUNT}");
    }
}