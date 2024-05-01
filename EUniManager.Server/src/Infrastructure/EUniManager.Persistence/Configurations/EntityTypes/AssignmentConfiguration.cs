using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.Assignments.AssignmentConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class AssignmentConfiguration : BaseEntityConfiguration<Assignment, Guid>
{
    public override void Configure(EntityTypeBuilder<Assignment> entity)
    {
        base.Configure(entity);

        entity.HasOne(a => a.Resource).WithMany(r => r.Assignments);

        entity.Property(а => а.Type).IsRequired()
                                        .IsUnicode(false)
                                        .HasConversion<string>()
                                        .HasMaxLength(TYPE_MAX_STRING_LENGTH);
        
        entity.HasMany(a => a.Students).WithMany(s => s.Assignments);

        entity.Property(a => a.Description).IsRequired(false)
                                           .IsUnicode()
                                           .HasMaxLength(DESCRIPTION_MAX_STRING_LENGTH);

        entity.HasMany(a => a.Solutions).WithOne(s => s.Assignment);

        entity.HasOne(a => a.Teacher).WithMany(t => t.Assignments)
              .OnDelete(DeleteBehavior.NoAction);
    }
}