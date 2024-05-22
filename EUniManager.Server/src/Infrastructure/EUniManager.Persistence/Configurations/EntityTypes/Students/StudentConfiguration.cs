using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Entities.Students;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.SqlConstant;
using static EUniManager.Persistence.Constants.Entities.GroupConstant;
using static EUniManager.Persistence.Constants.Entities.NamesConstant;
using static EUniManager.Persistence.Constants.Entities.SemesterConstant;
using static EUniManager.Persistence.Constants.Entities.Students.StudentConstant;
using static EUniManager.Persistence.Constants.Entities.Students.CityAreaConstant;
using static EUniManager.Persistence.Constants.Entities.Students.ServiceDataConstant;
using static EUniManager.Persistence.Constants.Entities.Students.PersonalDataConstant;
using static EUniManager.Persistence.Constants.Entities.Students.UniqueIdentifierConstant;
using static EUniManager.Persistence.Constants.Entities.Students.ResidenceConstant;
using static EUniManager.Persistence.Constants.Entities.Students.EnrollmentConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes.Students;

public sealed class StudentConfiguration : BaseEntityConfiguration<Student, Guid>
{
    public override void Configure(EntityTypeBuilder<Student> entity)
    {
        base.Configure(entity);

        entity.HasOne(s => s.User);
        
        entity.HasOne(s => s.Faculty).WithMany(f => f.Students)
              .IsRequired();

        entity.OwnsOne(s => s.ServiceData, ConfigureServiceData);

        entity.OwnsOne(s => s.PersonalData, ConfigurePersonalData);

        entity.ComplexProperty(s => s.PermanentResidence, ConfigurePermanentResidence);

        entity.ComplexProperty(s => s.TemporaryResidence, ConfigureTemporaryResidence);

        entity.Property(s => s.UsualResidenceCountry).IsRequired()
                                                     .IsUnicode()
                                                     .HasMaxLength(USUAL_RESIDENCE_COUNTRY_MAX_STRING_LENGTH);

        entity.ComplexProperty(s => s.Enrollment, ConfigureEnrollment);

        entity.HasOne(s => s.DiplomaOwned).WithOne(d => d.Student)
              .HasForeignKey<Diploma>();

        entity.HasOne(st => st.Specialty).WithMany(sp => sp.Students)
              .IsRequired();

        entity.HasMany(s => s.Assignments).WithMany(a => a.Students);

        entity.HasMany(s => s.AssignmentSolutions).WithOne(s => s.Student)
              .IsRequired(false);

        entity.HasMany(st => st.Subjects)
              .WithMany(sub => sub.Students);

        entity.HasMany(s => s.PayedTaxes).WithOne(pt => pt.Student)
              .IsRequired(false);

        entity.HasMany(s => s.RequestApplications).WithOne(ra => ra.Student)
              .IsRequired(false);

        entity.HasMany(s => s.CertifiedSemesters).WithOne(cs => cs.Student)
              .IsRequired(false);

        entity.ToTable(BuildCheckConstraints);
    }

    private void ConfigureServiceData(OwnedNavigationBuilder<Student, ServiceData> serviceData)
    {
        serviceData.WithOwner();
            
        serviceData.Property(sd => sd.Pin).IsRequired()
                                          .HasColumnName(nameof(ServiceData.Pin));
        serviceData.HasIndex(sd => sd.Pin).IsUnique()
                                          .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, nameof(ServiceData.Pin)));
            
        serviceData.Property(sd => sd.Status).IsRequired()
                                             .HasConversion<string>()
                                             .IsUnicode(false)
                                             .HasColumnName(nameof(ServiceData.Status))
                                             .HasMaxLength(STATUS_MAX_STRING_LENGTH);

        serviceData.Property(sd => sd.PlanNumber).IsRequired()
                                                 .HasColumnName(nameof(ServiceData.PlanNumber));

        serviceData.Property(sd => sd.FacultyNumber).IsRequired()
                                                    .HasColumnName(nameof(ServiceData.FacultyNumber));
        serviceData.HasIndex(sd => sd.FacultyNumber)
                   .IsUnique()
                   .HasDatabaseName(string.Format(UNIQUE_INDEX_TEMPLATE, nameof(ServiceData.FacultyNumber)));

        serviceData.Property(sd => sd.GroupNumber).IsRequired()
                                                  .HasColumnName(nameof(ServiceData.GroupNumber));

        serviceData.Property(sd => sd.EnrolledInSemester).HasColumnName(nameof(ServiceData.EnrolledInSemester));
    }
    
    private void ConfigurePersonalData(OwnedNavigationBuilder<Student, PersonalData> personalData)
    {
        personalData.WithOwner();

        personalData.OwnsOne(pd => pd.CityArea, ConfigurePersonalDataCityArea);

        personalData.Property(pd => pd.FirstName).IsRequired()
                                                 .IsUnicode()
                                                 .HasColumnName(nameof(PersonalData.FirstName))
                                                 .HasMaxLength(FIRST_NAME_MAX_STRING_LENGTH);
        
        personalData.Property(pd => pd.MiddleName).IsRequired()
                                                  .IsUnicode()
                                                  .HasColumnName(nameof(PersonalData.MiddleName))
                                                  .HasMaxLength(MIDDLE_NAME_MAX_STRING_LENGTH);
        
        personalData.Property(pd => pd.LastName).IsRequired()
                                                .IsUnicode()
                                                .HasColumnName(nameof(PersonalData.LastName))
                                                .HasMaxLength(LAST_NAME_MAX_STRING_LENGTH);

        personalData.OwnsOne(pd => pd.UniqueIdentifier, ConfigureUniqueIdentifier);

        personalData.Property(pd => pd.InsuranceNumber).IsUnicode(false)
                                                      .HasColumnName(nameof(PersonalData.InsuranceNumber))
                                                      .HasMaxLength(INSURANCE_NUMBER_MAX_STRING_LENGTH)
                                                      .IsUnicode(false);

        personalData.Property(pd => pd.BirthDate).IsRequired()
                                                 .HasColumnName(nameof(PersonalData.BirthDate));
        
        personalData.Property(pd => pd.Gender).IsRequired()
                                              .IsUnicode()
                                              .HasConversion<string>()
                                              .HasColumnName(nameof(PersonalData.Gender))
                                              .HasMaxLength(GENDER_MAX_STRING_LENGTH);

        personalData.Property(pd => pd.Citizenship).IsRequired()
                                                    .IsUnicode()
                                                    .HasColumnName(nameof(PersonalData.Citizenship))
                                                    .HasMaxLength(CITIZIENSHIP_MAX_STRING_LENGTH)
                                                    .IsUnicode(false);

        personalData.HasOne(pd => pd.IdCard);

        personalData.Property(pd => pd.Email).IsRequired()
                                             .IsUnicode(false)
                                             .HasColumnName(EMAIL_COLUMN_NAME)
                                             .HasMaxLength(EMAIL_COLUMN_MAX_STRING_LENGTH)
                                             .IsUnicode(false);
    }

    private void ConfigurePersonalDataCityArea(OwnedNavigationBuilder<PersonalData, CityArea> cityArea)
    {
        cityArea.Property(ca => ca.City).IsRequired(false)
            .IsUnicode()
            .HasColumnName(PERSONAL_DATA_CITY_COLUMN_NAME)
            .HasMaxLength(CITY_MAX_STRING_LENGTH);
        
        cityArea.Property(ca => ca.Area).IsRequired(false)
            .IsUnicode()
            .HasColumnName(PERSONAL_DATA_AREA_COLUMN_NAME)
            .HasMaxLength(AREA_MAX_STRING_LENGTH);
    }

    private void ConfigureUniqueIdentifier(OwnedNavigationBuilder<PersonalData, UniqueIdentifier> uniqueIdentifier)
    {
        uniqueIdentifier.Property(ui => ui.UniqueIdentifierType).IsRequired()
                                                                .IsUnicode(false)
                                                                .HasConversion<string>()
                                                                .HasColumnName(nameof(UniqueIdentifier.UniqueIdentifierType));

        uniqueIdentifier.Property(ui => ui.Identifier).IsRequired()
                                                      .IsUnicode(false)
                                                      .HasColumnName(nameof(UniqueIdentifier))
                                                      .HasMaxLength(IDENTIFIER_COLUMN_MAX_STRING_LENGTH)
                                                      .IsUnicode(false);
        
        string[] uniqueIdentifierUniqueConstraintColumnNames =
        [
            nameof(UniqueIdentifier.UniqueIdentifierType), 
            nameof(UniqueIdentifier)
        ];
        string uniqueIdentifierUniqueConstraintName = string.Format(UNIQUE_INDEX_TEMPLATE,
            string.Join('_', uniqueIdentifierUniqueConstraintColumnNames));
        uniqueIdentifier.HasIndex(ui => new { ui.UniqueIdentifierType, ui.Identifier })
                        .IsUnique()
                        .HasDatabaseName(uniqueIdentifierUniqueConstraintName);
    }

    private void ConfigurePermanentResidence(ComplexPropertyBuilder<Residence> permanentResidence) =>
        ConfigureResidenceBase(permanentResidence, ConfigurePermanentResidenceCityArea,
            PERMANENT_RESIDENCE_STREET_COLUMN_NAME, PERMANENT_RESIDENCE_PHONE_NUMBER_COLUMN_NAME);
    
    private void ConfigureTemporaryResidence(ComplexPropertyBuilder<Residence> temporaryResidence) => 
        ConfigureResidenceBase(temporaryResidence, ConfigureTemporaryResidenceCityArea,
            TEMPORARY_RESIDENCE_STREET_COLUMN_NAME, TEMPORARY_RESIDENCE_PHONE_NUMBER_COLUMN_NAME);

    private void ConfigureResidenceBase(ComplexPropertyBuilder<Residence> residence,
                                        Action<ComplexPropertyBuilder<CityArea>> configureCityArea, 
                                        string streetColumnName, 
                                        string phoneNumberColumnName)
    {
        residence.ComplexProperty(r => r.CityArea, configureCityArea);

        residence.Property(r => r.Street).IsUnicode()
                                         .HasMaxLength(STREET_MAX_STRING_LENGTH)
                                         .HasColumnName(streetColumnName);
        
        residence.Property(r => r.PhoneNumber).IsUnicode(false)
                                              .HasMaxLength(PHONE_NUMBER_MAX_STRING_LENGTH)
                                              .IsUnicode(false)
                                              .HasColumnName(phoneNumberColumnName);
    }

    private void ConfigurePermanentResidenceCityArea(ComplexPropertyBuilder<CityArea> cityArea) =>
        ConfigureCityAreaBase(cityArea, PERMANENT_RESIDENCE_CITY_COLUMN_NAME, PERMANENT_RESIDENCE_AREA_COLUMN_NAME);
    
    private void ConfigureTemporaryResidenceCityArea(ComplexPropertyBuilder<CityArea> cityArea) =>
        ConfigureCityAreaBase(cityArea, TEMPORARY_RESIDENCE_CITY_COLUMN_NAME, TEMPORARY_RESIDENCE_AREA_COLUMN_NAME);
    
    private void ConfigureCityAreaBase(ComplexPropertyBuilder<CityArea> cityArea, string cityColumnName, string areaColumnName)
    {
        cityArea.Property(ca => ca.City).IsRequired(false)
                                        .IsUnicode()
                                        .HasColumnName(cityColumnName)
                                        .HasMaxLength(CITY_MAX_STRING_LENGTH);
        
        cityArea.Property(ca => ca.Area).IsRequired(false)
                                        .IsUnicode()
                                        .HasColumnName(areaColumnName)
                                        .HasMaxLength(AREA_MAX_STRING_LENGTH);
    }
    
    private void ConfigureEnrollment(ComplexPropertyBuilder<Enrollment> enrollment)
    {
        enrollment.Property(e => e.Date).IsRequired()
                                        .HasColumnName(ENROLLMENT_DATE_COLUMN_NAME);

        enrollment.Property(e => e.Reason).IsRequired()
                                          .IsUnicode()
                                          .HasMaxLength(ENROLLMENT_REASON_MAX_STRING_LENGTH)
                                          .HasColumnName(ENROLLMENT_REASON_COLUMN_NAME);

        string markSqlDataType =
            string.Format(DECIMAL_DATA_TYPE_TEMPLATE, ENROLLMENT_MARK_PRECISION, ENROLLMENT_MARK_SCALE);
        enrollment.Property(e => e.Mark).IsRequired()
                                        .HasPrecision(ENROLLMENT_MARK_PRECISION, ENROLLMENT_MARK_SCALE)
                                        .HasColumnType(markSqlDataType)
                                        .HasColumnName(ENROLLMENT_MARK_COLUMN_NAME);
    }

    private void BuildCheckConstraints(TableBuilder<Student> studentsTable)
    {
        string[] checkConstraintTokens = [nameof(Student), nameof(Student.ServiceData.GroupNumber)];
        string checkConstraintTableColumn = string.Join('_', checkConstraintTokens);
        studentsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(ServiceData.GroupNumber)} BETWEEN {MIN_GROUP} AND {MAX_GROUP}");
            
        checkConstraintTableColumn = checkConstraintTableColumn.Replace(nameof(ServiceData.GroupNumber), 
            nameof(ServiceData.EnrolledInSemester));
        studentsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{nameof(ServiceData.EnrolledInSemester)} BETWEEN {MIN_SEMESTER} AND {MAX_SEMESTER}");
            
        checkConstraintTableColumn = checkConstraintTableColumn.Replace(nameof(ServiceData.GroupNumber), 
            ENROLLMENT_MARK_COLUMN_NAME);
        studentsTable.HasCheckConstraint(string.Format(CHECK_CONSTRAINT_TEMPLATE, checkConstraintTableColumn), 
            $"{ENROLLMENT_MARK_COLUMN_NAME} BETWEEN {MIN_MARK} AND {MAX_MARK}");
    }
}