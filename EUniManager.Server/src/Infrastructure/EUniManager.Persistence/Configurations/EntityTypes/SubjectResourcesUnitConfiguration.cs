using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.SemesterConstant;
using static EUniManager.Persistence.Constants.SqlConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class SubjectResourcesUnitConfiguration : BaseEntityConfiguration<SubjectResourcesUnit, Guid>
{
    public override void Configure(EntityTypeBuilder<SubjectResourcesUnit> entity)
    {
        entity.Property(u => u.Semester).IsRequired();

        entity.HasOne(u => u.Subject).WithOne(s => s.ResourcesUnit)
              .IsRequired();

        entity.HasMany(u => u.Resources).WithOne(r => r.SubjectResourcesUnit);
        
        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = 
            [
                nameof(SubjectResourcesUnit), 
                nameof(SubjectResourcesUnit.Semester)
            ];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                $"{nameof(SubjectResourcesUnit.Semester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
        });
    }
}