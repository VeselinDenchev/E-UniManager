using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.SpecialtyConstant;
using static EUniManager.Persistence.Constants.Entities.YearConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class SpecialtyConfiguration : BaseEntityConfiguration<Specialty, Guid>
{
    public override void Configure(EntityTypeBuilder<Specialty> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.Faculty).WithMany(f => f.Specialties)
              .IsRequired(false);

        entity.Property(s => s.Name).IsRequired()
                                    .IsUnicode()
                                    .HasMaxLength(NAME_MAX_STRING_LENGTH);

        entity.Property(s => s.FirstAcademicYearStart).IsRequired();
        
        entity.Property(s => s.CurrentYear).IsRequired().HasDefaultValue(CURRENT_YEAR_DEFAULT_VALUE);
        
        entity.Property(s => s.Name).IsRequired()
                                    .IsUnicode()
                                    .HasMaxLength(NAME_MAX_STRING_LENGTH);

        string uniqueIndexColumnsJoined = string.Join('_', nameof(Specialty.Name), nameof(Specialty.FirstAcademicYearStart));
        entity.HasIndex(s => new { s.Name, s.FirstAcademicYearStart })
              .IsUnique()
              .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, uniqueIndexColumnsJoined));

        entity.HasMany(sp => sp.Students).WithOne(st => st.Specialty)
              .OnDelete(DeleteBehavior.NoAction)
              .IsRequired(false);

        entity.HasMany(sp => sp.Subjects).WithOne(sub => sub.Specialty)
              .IsRequired();

        entity.Property(s => s.HasGraduated).IsRequired()
                                            .HasDefaultValue(false);
        
        entity.Property(s => s.EducationType).IsRequired()
                                             .HasConversion<string>()
                                             .IsUnicode(false)
                                             .HasMaxLength(EDUCATION_TYPE_MAX_STRING_LENGTH);

        entity.ToTable(table =>
        {
              List<string> checkConstraintTokens = [nameof(Specialty), nameof(Specialty.FirstAcademicYearStart)];
              string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
              table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                    $"{nameof(Specialty.FirstAcademicYearStart)} BETWEEN {MIN_YEAR} AND {MAX_YEAR}");
              
              checkConstraintTokens.Add(nameof(Specialty.CurrentYear));
              checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
              table.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
                    $"{nameof(Specialty.FirstAcademicYearStart)} + {nameof(Specialty.CurrentYear)} <= " +
                    $"{string.Format(YEAR_FUNCTION_TEMPLATE, GET_DATE_FUNCTION)} + 1");
        });
    }
}