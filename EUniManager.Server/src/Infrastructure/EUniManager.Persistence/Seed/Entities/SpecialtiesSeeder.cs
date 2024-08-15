using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static  class SpecialtiesSeeder
{
    private static Faculty _facultyOfMathematicsAndInformatics;
    
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Specialties.AsNoTracking().AnyAsync()) return;

        _facultyOfMathematicsAndInformatics =
            await dbContext.Faculties.FirstAsync(f => f.Name == "Факултет \"Математика и информацтика\"");

        List<Specialty> specialties = new();
        
        AddBachelorFullTimeSpecialties(specialties);
        AddBachelorPartTimeSpecialtyNames(specialties);
        AddMasterFullAndPartTimeSpecialtyNames(specialties);

        Specialty itInJudicialAndExecutivePowerRemote = GetNewSpecialty(
            _facultyOfMathematicsAndInformatics,
            "Информационни технологии в съдебната и изпълнителната власт", SpecialtyEducationType.Remote,
            EducationalAndQualificationDegree.Master);
        specialties.Add(itInJudicialAndExecutivePowerRemote);

        await dbContext.Specialties.AddRangeAsync(specialties);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Specialties seeded successfully");
    }

    private static void AddBachelorFullTimeSpecialties(List<Specialty> specialties)
    {
        string[] bachelorFullTimeSpecialtyNames =
        [
            "Педагогика на обучението по математика и информатика",
            "Приложна математика",
            "Информатика",
            "Компютърни науки",
            "Софтуерно инженерство",
            "Информационно брокерство и дигитални медии"
        ];
        
        foreach (string specialtyName in bachelorFullTimeSpecialtyNames)
        {
            Specialty specialty = GetNewSpecialty(
                _facultyOfMathematicsAndInformatics!,
                specialtyName,
                SpecialtyEducationType.FullTime,
                EducationalAndQualificationDegree.Bachelor);
            specialties.Add(specialty);
        }
    }
    
    private static void AddBachelorPartTimeSpecialtyNames(List<Specialty> specialties)
    {
        string[] bachelorPartTimeSpecialtyNames =
        [
            "Педагогика на обучението по математика и информатика",
            "Приложна математика"
        ];
        
        foreach (string specialtyName in bachelorPartTimeSpecialtyNames)
        {
            Specialty specialty = GetNewSpecialty(
                _facultyOfMathematicsAndInformatics!,
                specialtyName,
                SpecialtyEducationType.PartTime,
                EducationalAndQualificationDegree.Bachelor);
            specialties.Add(specialty);
        }
    }

    private static void AddMasterFullAndPartTimeSpecialtyNames(List<Specialty> specialties)
    {
        string[] masterFullAndPartTimeSpecialtyNames =
        [
            "Математика, информатика и информационни технологии",
            "Математика",
            "Информатика и информационни технологии",
            "Технологии за обучение по математика и информатика",
            "Математически структури в информационната сигурност",
            "Информатика. Компютърна мултимедия",
            "Информатика. Информационни системи",
            "Информатика. Защита на информацията",
            "Информатика. Корпоративни мрежови среди",
            "Компютърни науки. Приложни компютърни науки",
            "Уеб технологии и разработване на софтуер",
            "Информационни технологии в съдебната и изпълнителната власт",
            "Дигитална трансформация на бизнеса и образованието"
        ];
        
        foreach (string specialtyName in masterFullAndPartTimeSpecialtyNames)
        {
            Specialty fullTimeSpecialty = GetNewSpecialty(
                _facultyOfMathematicsAndInformatics,
                specialtyName,
                SpecialtyEducationType.FullTime,
                EducationalAndQualificationDegree.Master);
            
            Specialty partTimeSpecialty = GetNewSpecialty(
                _facultyOfMathematicsAndInformatics,
                specialtyName,
                SpecialtyEducationType.PartTime,
                EducationalAndQualificationDegree.Master);
            
            specialties.AddRange([ fullTimeSpecialty, partTimeSpecialty ]); 
        }
    }
    
    private static Specialty GetNewSpecialty(
        Faculty faculty,
        string name,
        SpecialtyEducationType educationType,
        EducationalAndQualificationDegree degree)
    {
        return new Specialty
        {
            Faculty = faculty,
            Name = name,
            FirstAcademicYearStart = DateTime.Now.Year - 1,
            CurrentYear = 1,
            HasGraduated = false,
            EducationType = educationType,
            EducationalAndQualificationDegree = degree
        };
    }
}