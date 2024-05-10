using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.PayedTaxConstant;
using static EUniManager.Persistence.Constants.Entities.SemesterConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class PayedTaxConfiguration : BaseEntityConfiguration<PayedTax, Guid>
{
    public override void Configure(EntityTypeBuilder<PayedTax> entity)
    {
        base.Configure(entity);

        entity.HasOne(pt => pt.Student).WithMany(s => s.PayedTaxes)
              .IsRequired();
        
        entity.Property(pt => pt.TaxNumber).IsRequired();
        entity.HasIndex(pt => pt.TaxNumber)
              .IsUnique()
              .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, nameof(PayedTax.TaxNumber)));
        
        entity.Property(pt => pt.DocumentNumber).IsRequired()
                                                .IsUnicode(false)
                                                .HasMaxLength(DOCUMENT_NUMBER_MAX_STRING_LENGTH);

        entity.Property(pt => pt.Semester).IsRequired();

        entity.Property(pt => pt.Amount).IsRequired();

        entity.Property(pt => pt.Currency).IsRequired()
                                          .HasConversion<string>()
                                          .IsUnicode(false)
                                          .HasMaxLength(CURRENCY_MAX_STRING_LENGTH);
        
        entity.ToTable(BuildCheckConstraints);
    }
    
    private void BuildCheckConstraints(TableBuilder<PayedTax> payedTaxesTable)
    {
        string[] checkConstraintTokens = [nameof(PayedTax), nameof(PayedTax.TaxNumber)];
        string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
        payedTaxesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(PayedTax.TaxNumber)} > 0");
        
        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(PayedTax.TaxNumber), nameof(PayedTax.Semester));
        payedTaxesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(PayedTax.Semester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
        
        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(PayedTax.Semester), nameof(PayedTax.Amount));
        payedTaxesTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(PayedTax.Amount)} > 0");
    }
}