using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.RequestApplicationConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public class RequestApplicationConfiguration : BaseEntityConfiguration<RequestApplication, Guid>
{
    public override void Configure(EntityTypeBuilder<RequestApplication> entity)
    {
        base.Configure(entity);

        entity.Property(ra => ra.Number).IsRequired();
        entity.HasIndex(ra => ra.Number)
              .IsUnique()
              .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, nameof(RequestApplication.Number)));
        
        entity.HasOne(ra => ra.Student).WithMany(s => s.RequestApplications);

        entity.Property(ra => ra.Type).IsRequired()
                                      .HasConversion<string>()
                                      .IsUnicode(false)
                                      .HasMaxLength(TYPE_MAX_STRING_LENGTH);

        entity.Property(ra => ra.RegistryDate).IsRequired();
        
        entity.Property(ra => ra.ResolutionDate).IsRequired(false);

        entity.HasOne(ra => ra.File);

        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = [nameof(RequestApplication), nameof(RequestApplication.ResolutionDate)];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                $"{nameof(RequestApplication.ResolutionDate)} IS NULL OR " +
                $"{nameof(RequestApplication.ResolutionDate)} > {nameof(RequestApplication.RegistryDate)}");
        });
    }
}