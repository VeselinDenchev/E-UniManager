using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.FacultyConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class FacultyConfiguration : BaseEntityConfiguration<Faculty, Guid>
{
    public override void Configure(EntityTypeBuilder<Faculty> entity)
    {
        base.Configure(entity);

        entity.Property(f => f.Name).IsRequired()
                                    .IsUnicode()
                                    .HasMaxLength(NAME_MAX_STRING_LENGTH);

        entity.HasMany(f => f.Students).WithOne(s => s.Faculty)
              .IsRequired(false);
    }
}