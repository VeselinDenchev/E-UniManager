using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.SpecialtyConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public class SpecialtyConfiguration : BaseEntityConfiguration<Specialty, Guid>
{
    public override void Configure(EntityTypeBuilder<Specialty> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.Faculty).WithMany(f => f.Specialties);

        entity.Property(s => s.Name).IsRequired()
                                    .IsUnicode()
                                    .HasMaxLength(NAME_MAX_STRING_LENGTH);

        entity.HasMany(sp => sp.Students).WithOne(st => st.Specialty)
              .OnDelete(DeleteBehavior.NoAction);

        entity.HasMany(sp => sp.Subjects).WithOne(sub => sub.Specialty);
    }
}