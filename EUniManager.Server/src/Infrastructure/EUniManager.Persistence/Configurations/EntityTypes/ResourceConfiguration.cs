using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.ResourceConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class ResourceConfiguration : BaseEntityConfiguration<Resource, Guid>
{
    public override void Configure(EntityTypeBuilder<Resource> entity)
    {
        base.Configure(entity);

        entity.Property(r => r.Title).IsRequired()
                                     .IsUnicode()
                                     .HasMaxLength(TITLE_MAX_STRING_LENGTH);

        entity.Property(r => r.Type).IsRequired()
                                    .HasConversion<string>()
                                    .IsUnicode(false)
                                    .HasMaxLength(TYPE_MAX_STRING_LENGTH);

        entity.HasOne(r => r.CurriculumSubjectResourcesUnit).WithMany(csru => csru.Resources);

        entity.HasMany(r => r.Assignments).WithOne(a => a.Resource);
    }
}