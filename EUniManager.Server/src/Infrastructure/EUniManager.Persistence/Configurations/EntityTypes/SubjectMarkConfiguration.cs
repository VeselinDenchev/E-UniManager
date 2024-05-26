using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class SubjectMarkConfiguration : BaseEntityConfiguration<SubjectMark, Guid>
{
    public override void Configure(EntityTypeBuilder<SubjectMark> entity)
    {
        base.Configure(entity);

        entity.HasOne(sm => sm.Student).WithMany(s => s.SubjectMarks);
        
        entity.HasOne(sm => sm.Subject).WithMany(s => s.Marks);

        entity.Property(sm => sm.Mark).IsRequired(false);
        
        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = [nameof(SubjectMark), nameof(Mark)];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                $"{nameof(SubjectMark.Mark)} BETWEEN {(int)Mark.Poor} AND {(int)Mark.Excellent}");
        });
    }
}