using System.Runtime.ConstrainedExecution;

using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.SemesterConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class CertifiedSemesterConfiguration : BaseEntityConfiguration<CertifiedSemester, Guid>
{
    public override void Configure(EntityTypeBuilder<CertifiedSemester> entity)
    {
        base.Configure(entity);

        entity.HasOne(cs => cs.Student).WithMany(s => s.CertifiedSemesters)
              .IsRequired();

        entity.Property(cs => cs.Semester).IsRequired();
        
        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = [nameof(CertifiedSemester), nameof(CertifiedSemester.Semester)];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                                     $"{nameof(CertifiedSemester.Semester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
        });
    }
}