using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Abstraction.Base.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes.Base;

public class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> 
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.CreatedAt).IsRequired()
                                         .ValueGeneratedOnAdd()
                                         .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        entity.Property(e => e.ModifiedAt).IsRequired()
                                          .HasDefaultValueSql(GET_DATE_FUNCTION);

        entity.ToTable(table =>
        {
            string[] checkConstraintTokens = [typeof(TEntity).Name, nameof(IAuditable.ModifiedAt)];
            string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
            table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                                     $"{nameof(IAuditable.ModifiedAt)} >= {nameof(IAuditable.CreatedAt)}");
        });
    }
}