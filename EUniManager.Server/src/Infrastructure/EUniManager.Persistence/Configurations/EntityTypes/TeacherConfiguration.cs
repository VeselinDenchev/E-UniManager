using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.NamesConstant;
using static EUniManager.Persistence.Constants.Entities.TeacherConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class TeacherConfiguration : BaseEntityConfiguration<Teacher, Guid>
{
    public override void Configure(EntityTypeBuilder<Teacher> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.User);
        
        entity.Property(t => t.Rank).IsRequired(false)
                                    .IsUnicode()
                                    .HasMaxLength(RANK_MAX_STRING_LENGTH);

        entity.Property(t => t.FirstName).IsRequired()
                                         .IsUnicode()
                                         .HasMaxLength(FIRST_NAME_MAX_STRING_LENGTH);
        
        entity.Property(t => t.MiddleName).IsRequired(false)
                                          .IsUnicode()
                                          .HasMaxLength(MIDDLE_NAME_MAX_STRING_LENGTH);
        
        entity.Property(t => t.LastName).IsRequired()
                                        .IsUnicode()
                                        .HasMaxLength(LAST_NAME_MAX_STRING_LENGTH);

        entity.HasMany(t => t.LecturingSubjects).WithOne(s => s.Lecturer)
              .IsRequired(false);

        entity.HasMany(t => t.AssistingSubjects).WithMany(s => s.Assistants);
        
        entity.HasMany(t => t.Activities).WithOne(s => s.Teacher)
              .IsRequired(false);

        entity.HasMany(t => t.Assignments).WithOne(a => a.Teacher)
              .OnDelete(DeleteBehavior.ClientCascade)
              .IsRequired(false);
    }
}