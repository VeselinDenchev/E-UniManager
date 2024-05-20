using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.SubjectConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class SubjectConfiguration : BaseEntityConfiguration<Subject, Guid>
{
    public override void Configure(EntityTypeBuilder<Subject> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.Course).WithMany(c => c.Subjects)
              .IsRequired(false);

        entity.HasMany(sub => sub.Students).WithMany(st => st.Subjects);
        
        entity.HasOne(s => s.Lecturer).WithMany(l => l.LecturingSubjects)
              .OnDelete(DeleteBehavior.NoAction)
              .IsRequired();
        
        entity.HasMany(s => s.Assistants).WithMany(a => a.AssistingSubjects);

        entity.HasOne(sub => sub.Specialty).WithMany(sp => sp.Subjects)
              .OnDelete(DeleteBehavior.NoAction)
              .IsRequired();
        
        entity.HasOne(s => s.Course).WithMany(c => c.Subjects)
              .IsRequired();

        entity.HasMany(s => s.Activities).WithOne(a => a.Subject)
              .IsRequired(false);

        entity.HasOne(s => s.Exam).WithOne(e => e.Subject)
              .HasForeignKey<Exam>()
              .IsRequired(false);
        
        entity.Property(c => c.Mark).IsRequired(false);
        
        entity.Property(c => c.Protocol).IsRequired()
                                        .IsUnicode(false)
                                        .HasMaxLength(PROCOTOL_MAX_STRING_LENGTH);
        entity.HasIndex(sd => sd.Protocol).IsUnique()
              .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, nameof(Subject.Protocol)));
    }
}