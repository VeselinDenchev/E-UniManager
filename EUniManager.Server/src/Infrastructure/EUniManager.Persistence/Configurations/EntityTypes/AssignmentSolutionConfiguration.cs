using EUniManager.Domain.Abstraction.Base.Interfaces;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.Assignments.AssignmentSolutionConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class AssignmentSolutionConfiguration : BaseEntityConfiguration<AssignmentSolution, Guid>
{
    public override void Configure(EntityTypeBuilder<AssignmentSolution> entity)
    {
        base.Configure(entity);

        entity.HasOne(asol => asol.Assignment).WithMany(a => a.Solutions)
              .IsRequired();

        entity.HasOne(asol => asol.Student).WithMany(s => s.AssignmentSolutions)
              .IsRequired();

        entity.HasOne(asol => asol.File);

        entity.Property(asol => asol.SeenOn).IsRequired(false);
        
        entity.Property(asol => asol.UploadedOn).IsRequired(false);

        entity.Property(asol => asol.Mark).IsRequired(false);

        entity.Property(asol => asol.MarkedOn).IsRequired(false);

        entity.Property(asol => asol.Comment).IsRequired(false)
                                             .IsUnicode()
                                             .HasMaxLength(COMMENT_MAX_STRING_LENGTH);

        entity.ToTable(BuildCheckConstraints);
    }
    
    private void BuildCheckConstraints(TableBuilder<AssignmentSolution> assignmentSolutionsTable)
    {
        List<string> checkConstraintTokens = 
        [
            nameof(AssignmentSolution), 
            nameof(AssignmentSolution.SeenOn), 
            nameof(IAuditable.CreatedAt)
        ];
        string checkConstraintTableColumns = string.Join('_', checkConstraintTokens);
        assignmentSolutionsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumns), 
            $"{nameof(AssignmentSolution.SeenOn)} IS NULL OR " +
            $"{nameof(AssignmentSolution.SeenOn)} > {nameof(IAuditable.CreatedAt)}");

        checkConstraintTableColumns = checkConstraintTableColumns.Replace(nameof(AssignmentSolution.SeenOn), 
                                                                          nameof(AssignmentSolution.UploadedOn))
                                                                 .Replace(nameof(IAuditable.CreatedAt),
                                                                          nameof(AssignmentSolution.SeenOn));
        assignmentSolutionsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumns), 
            $"{nameof(AssignmentSolution.UploadedOn)} IS NULL OR " +
            $"{nameof(AssignmentSolution.UploadedOn)} > {nameof(AssignmentSolution.SeenOn)}");
        
        checkConstraintTableColumns = checkConstraintTableColumns.Replace(nameof(AssignmentSolution.UploadedOn), 
                                                                          nameof(AssignmentSolution.MarkedOn))
                                                                 .Replace(nameof(AssignmentSolution.SeenOn),
                                                                          nameof(AssignmentSolution.UploadedOn));
        assignmentSolutionsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumns), 
            $"{nameof(AssignmentSolution.MarkedOn)} IS NULL OR " +
            $"{nameof(AssignmentSolution.MarkedOn)} > {nameof(AssignmentSolution.UploadedOn)}");
        
        checkConstraintTokens.RemoveAt(2);
        checkConstraintTokens[1] = nameof(AssignmentSolution.Mark);
        checkConstraintTableColumns = string.Join('_', checkConstraintTokens);
        assignmentSolutionsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumns), 
            $"{nameof(AssignmentSolution.Mark)} IS NULL OR " +
            $"{nameof(AssignmentSolution.Mark)} BETWEEN {(byte)Mark.Poor} AND {(byte)Mark.Excellent}");
    }
}