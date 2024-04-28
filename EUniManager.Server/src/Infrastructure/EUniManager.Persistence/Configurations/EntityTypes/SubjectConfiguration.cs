using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.SubjectConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class SubjectConfiguration : BaseEntityConfiguration<Subject, Guid>
{
    public override void Configure(EntityTypeBuilder<Subject> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.Course).WithMany(c => c.Subjects);

        entity.HasMany(sub => sub.Students).WithMany(st => st.Subjects);
        
        entity.HasOne(s => s.Lecturer).WithMany(l => l.LecturingSubjects)
              .OnDelete(DeleteBehavior.NoAction);
        
        entity.HasMany(s => s.Assistants).WithMany(a => a.AssistingSubjects);

        entity.Property(s => s.Type).IsRequired()
                                    .HasConversion<string>()
                                    .IsUnicode(false)
                                    .HasMaxLength(TYPE_MAX_STRING_LENGTH);

        entity.HasOne(sub => sub.Specialty).WithMany(sp => sp.Subjects)
              .OnDelete(DeleteBehavior.NoAction);
        
        entity.HasOne(s => s.Course).WithMany(c => c.Subjects);

        entity.HasMany(s => s.CourseScheduleUnits).WithOne(csu => csu.Subject);

        entity.HasMany(s => s.CurriculumSubjectResources).WithOne(csr => csr.Subject);

        entity.HasOne(s => s.Exam).WithOne(e => e.Subject)
              .HasForeignKey<Exam>();
    }
}