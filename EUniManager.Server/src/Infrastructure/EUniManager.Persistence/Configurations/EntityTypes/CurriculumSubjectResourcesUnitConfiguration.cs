using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.SemesterConstant;
using static EUniManager.Persistence.Constants.SqlConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class CurriculumSubjectResourcesUnitConfiguration 
    : BaseEntityConfiguration<CurriculumSubjectResourcesUnit, Guid>
{
    public override void Configure(EntityTypeBuilder<CurriculumSubjectResourcesUnit> entity)
    {
        entity.Property(u => u.Semester).IsRequired();

        entity.HasOne(u => u.Subject).WithMany(s => s.CurriculumSubjectResources);

        entity.HasMany(u => u.Resources).WithOne(r => r.CurriculumSubjectResourcesUnit);
        
        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = 
            [
                nameof(CurriculumSubjectResourcesUnit), 
                nameof(CurriculumSubjectResourcesUnit.Semester)
            ];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                $"{nameof(CurriculumSubjectResourcesUnit.Semester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
        });
    }
}