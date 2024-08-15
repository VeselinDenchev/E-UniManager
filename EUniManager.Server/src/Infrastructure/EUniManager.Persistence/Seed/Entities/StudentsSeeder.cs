using EUniManager.Application.Extensions;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public class StudentsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
    {
        if (await dbContext.Students.AsNoTracking().AnyAsync()) return;
        
        Student student = new()
        {
            ServiceData = new ServiceData
            {
                Pin = 2009011464,
                Status = StudentStatus.Studying,
                PlanNumber = 123_456_789,
                FacultyNumber = 2009011464,
                GroupNumber = 1,
                EnrolledInSemester = 1
            },
            PersonalData = new PersonalData
            {
                CityArea = new CityArea
                {
                    City = "Дряново",
                    Area = "Габрово"
                },
                Gender = Gender.Male,
                FirstName = "Веселин",
                MiddleName = "Мирославов",
                LastName = "Денчев",
                UniqueIdentifier = new UniqueIdentifier
                {
                    UniqueIdentifierType = PersonalUniqueIdentifierType.Egn,
                    Identifier = "0141071234"
                },
                InsuranceNumber = SeedHelper.GetRandomString(10),
                BirthDate = new DateOnly(2001, 1, 7),
                Citizenship = "Българско",
                IdCard = new IdCard
                {
                    IdNumber = SeedHelper.GetRandomString(9),
                    DateIssued = new DateOnly(2019, 1, 7)
                },
                Email = "s2009011464@uni-vt.bg",
            },
            PermanentResidence = new Residence
            {
                CityArea = new CityArea
                {
                    City = "Велико Търново",
                    Area = "Велико Търново",
                },
                Street = "ул. Ниш №1",
                PhoneNumber = "0876559291"
            },
            TemporaryResidence = new Residence
            {
                CityArea = new CityArea
                {
                    City = "Велико Търново",
                    Area = "Велико Търново",
                },
                Street = "ул. Ниш №1",
                PhoneNumber = "0876559291"
            },
            UsualResidenceCountry = "България",
            Enrollment = new Enrollment
            {
                Date = new DateOnly(2024, 7, 15),
                Reason = "Успех",
                Mark = 6
            },
            DiplomaOwned = new Diploma
            {
                EducationalAndQualificationDegree = EducationalAndQualificationDegree.HighSchool,
                Series = SeedHelper.GetRandomString(4),
                Number = SeedHelper.GetRandomString(6),
                RegistrationNumber = SeedHelper.GetRandomString(7),
                Date = new DateOnly(2024, 6, 30),
                Year = 2024,
                IssuedByInstitutionType = InstitutionIssuerType.HighSchool,
                InstitutionName = "ПМГ Акад. Иван Гюзелев",
                CityArea = new CityArea
                {
                    City = "Габрово",
                    Area = "Габрово"
                },
                Specialty = "Информационни технологии"
            },
            Specialty =  await dbContext.Specialties.FirstAsync(s => s.Name == "Софтуерно инженерство")
        };

        student.User = new IdentityUser<Guid>()
        {
            Email = student.PersonalData.Email,
            UserName = student.ServiceData.Pin.ToString(),
            PhoneNumber = student.PermanentResidence.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        string password = student.PersonalData.UniqueIdentifier.Identifier;
        IdentityResult result = await userManager.CreateAsync(student.User, password);
        result.ThrowExceptionIfFailed();
        
        result = await userManager.AddToRoleAsync(student.User, nameof(UserRole.Student));
        result.ThrowExceptionIfFailed();
        
        if (student.PersonalData.IdCard is not null)
        {
            await dbContext.IdCards.AddAsync(student.PersonalData.IdCard);
        }

        await dbContext.Diplomas.AddAsync(student.DiplomaOwned);
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();

        Console.WriteLine("Students seeded successfully");
    }
}