using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.ActivityConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public class ActivityConfiguration : BaseEntityConfiguration<Activity, Guid>
{
    public override void Configure(EntityTypeBuilder<Activity> entity)
    {
        base.Configure(entity);

        entity.Property(a => a.Type).IsRequired()
                                    .HasConversion<string>()
                                    .IsUnicode(false)
                                    .HasMaxLength(TYPE_MAX_STRING_LENGTH);

        entity.HasOne(a => a.Teacher).WithMany(t => t.Activities)
              .IsRequired();

        entity.HasMany(a => a.Students).WithMany(s => s.Activities);
        
        entity.HasOne(a => a.Subject).WithMany(s => s.Activities)
              .IsRequired();

        entity.Property(a => a.IsStopped)
              .IsRequired();
    }
}