using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Entities.Students;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.Students.DiplomaConstant;
using static EUniManager.Persistence.Constants.Entities.Students.CityAreaConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes.Students;

public sealed class DiplomaConfiguration : BaseEntityConfiguration<Diploma, Guid>
{
    public override void Configure(EntityTypeBuilder<Diploma> entity)
    {
        base.Configure(entity);

        entity.HasOne(d => d.Student).WithOne(s => s.DiplomaOwned)
               .HasForeignKey<Student>();
        
        entity.Property(d => d.EducationalAndQualificationalDegree).IsRequired()
                                                                    .HasConversion<string>()
                                                                    .IsUnicode(false)
                                                                    .HasMaxLength(EDUCATIONAL_AND_QUALIFICATIONAL_DEGREE_MAX_STRING_LENGTH);
        
        entity.Property(d => d.Series).IsRequired()
                                       .IsUnicode(false)
                                       .HasMaxLength(SERIES_MAX_STRING_LENGTH);
        
        entity.Property(d => d.Number).IsRequired()
                                       .IsUnicode(false)
                                       .HasMaxLength(NUMBER_MAX_STRING_LENGTH);
        
        entity.Property(d => d.RegistrationNumber).IsRequired(false)
                                                   .IsUnicode(false)
                                                   .HasMaxLength(REGISTRATION_NUMBER_MAX_STRING_LENGTH);

        entity.Property(d => d.Date).IsRequired();

        entity.Property(d => d.Year).IsRequired();
        
        entity.Property(d => d.IssuedByInstitutionType).IsRequired()
                                                        .HasConversion<string>()
                                                        .HasMaxLength(ISSUED_BY_INSTITUTION_TYPE_MAX_STRING_LENGTH);

        entity.Property(d => d.InstitutionName).IsRequired()
                                                .IsUnicode()
                                                .HasMaxLength(INSTITUTION_NAME_MAX_STRING_LENGTH);

        entity.OwnsOne(d => d.CityArea, ConfigureCityArea);

        entity.Property(d => d.Specialty).IsRequired()
                                          .IsUnicode()
                                          .HasMaxLength(SPECIALTY_MAX_STRING_LENGTH);

        entity.Property(d => d.ProfessionalQualification).IsRequired(false)
                                                          .IsUnicode()
                                                          .HasMaxLength(PROFESSIONAL_QUALIFICATION_MAX_STRING_LENGTH);
    }

    private void ConfigureCityArea(OwnedNavigationBuilder<Diploma, CityArea> cityArea)
    {
        cityArea.Property(ca => ca.City).IsRequired(false)
                                        .IsUnicode()
                                        .HasColumnName(DIPLOMA_CITY_COLUMN_NAME)
                                        .HasMaxLength(CITY_MAX_STRING_LENGTH);
        
        cityArea.Property(ca => ca.Area).IsRequired(false)
                                        .IsUnicode()
                                        .HasColumnName(DIPLOMA_AREA_COLUMN_NAME)
                                        .HasMaxLength(AREA_MAX_STRING_LENGTH);
    }
}