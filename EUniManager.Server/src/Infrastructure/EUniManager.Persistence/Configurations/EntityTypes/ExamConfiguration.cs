using EUniManager.Domain.Abstraction.Base.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.GroupConstant;
using static EUniManager.Persistence.Constants.Entities.ExamConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class ExamConfiguration : BaseEntityConfiguration<Exam, Guid>
{
    public override void Configure(EntityTypeBuilder<Exam> entity)
    {
        base.Configure(entity);

        entity.HasOne(e => e.Subject).WithOne(s => s.Exam)
            .HasForeignKey<Subject>();

        entity.Property(e => e.Type).IsRequired()
                                  .HasConversion<string>()
                                  .IsUnicode(false)
                                  .HasMaxLength(TYPE_MAX_STRING_LENGTH);

        entity.Property(e => e.Date).IsRequired();

        entity.Property(e => e.Time).IsRequired();

        entity.Property(e => e.Place).IsRequired()
                                   .HasConversion<string>()
                                   .IsUnicode(false)
                                   .HasMaxLength(PLACE_MAX_STRING_LENGTH);

        entity.Property(e => e.RoomNumber).IsRequired();

        entity.Property(e => e.GroupNumber).IsRequired(false);

        entity.ToTable(BuildCheckConstraints);
    }
    
    private void BuildCheckConstraints(TableBuilder<Exam> examsTable)
    {
        string[] checkConstraintTokens = [nameof(Exam), nameof(Exam.Time)];
        string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
        examsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
             $"{nameof(Exam.Time)} BETWEEN {MIN_START_TIME} AND {MAX_START_TIME}");
        
        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(Exam.Time), nameof(Exam.RoomNumber));
        examsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Exam.RoomNumber)} BETWEEN {MIN_ROOM_NUMBER} AND {MAX_ROOM_NUMBER}");
        
        checkConstraintTableColumn =
            checkConstraintTableColumn.Replace(nameof(Exam.RoomNumber), nameof(Exam.GroupNumber));
        examsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(Exam.GroupNumber)} IS NULL OR {nameof(Exam.GroupNumber)} BETWEEN {MIN_GROUP} AND {MAX_GROUP}");
    }
}