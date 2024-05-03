using EUniManager.Domain.Abstraction.Base.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.Assignments.AssignmentConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class AssignmentConfiguration : BaseEntityConfiguration<Assignment, Guid>
{
    public override void Configure(EntityTypeBuilder<Assignment> entity)
    {
        base.Configure(entity);

        entity.HasOne(a => a.Resource).WithMany(r => r.Assignments);

        entity.Property(a => a.Title).IsRequired()
                                     .IsUnicode()
                                     .HasMaxLength(TITLE_MAX_STRING_LENGTH);

        entity.Property(a => a.StartDate).IsRequired();
        
        entity.Property(a => a.DueDate).IsRequired();

        entity.Property(а => а.Type).IsRequired()
                                    .IsUnicode(false)
                                    .HasConversion<string>()
                                    .HasMaxLength(TYPE_MAX_STRING_LENGTH);
        
        entity.HasMany(a => a.Students).WithMany(s => s.Assignments);

        entity.Property(a => a.Description).IsRequired(false)
                                           .IsUnicode()
                                           .HasMaxLength(DESCRIPTION_MAX_STRING_LENGTH);

        entity.HasMany(a => a.Solutions).WithOne(s => s.Assignment);

        entity.HasOne(a => a.Teacher).WithMany(t => t.Assignments)
              .OnDelete(DeleteBehavior.NoAction);
        
        entity.ToTable(BuildCheckConstraints);
    }
    
    private void BuildCheckConstraints(TableBuilder<Assignment> assignmentsTable)
    {
        string[] checkConstraintTokens = [nameof(Assignment), nameof(Assignment.StartDate)];
        string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
        assignmentsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Assignment.StartDate)} >= {nameof(IAuditable.CreatedAt)}");

        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(Assignment.StartDate), nameof(Assignment.DueDate));
        assignmentsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Assignment.DueDate)} > {nameof(Assignment.StartDate)}");
    }
}