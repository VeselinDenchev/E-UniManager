using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class CoursesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Courses.AsNoTracking().AnyAsync()) return;
        
        Course[] courses = 
        [
            new()
            {
                Name = "Уеб програмиране с Java",
                LecturesCount = 15,
                ExercisesCount = 45,
                CreditsCount = 6
            },
            new()
            {
                Name = "Технологии и разработка на приложения за виртуална реалност",
                LecturesCount = 15,
                ExercisesCount = 30,
                CreditsCount = 6
            },
            new()
            {
                Name = "Сензорни мрежи",
                LecturesCount = 15,
                ExercisesCount = 30,
                CreditsCount = 6
            },
            new()
            {
                Name = "Програмиране за Linux",
                LecturesCount = 15,
                ExercisesCount = 45,
                CreditsCount = 6
            },
            new()
            {
                Name = "Графични уеб приложения с JAVAScript, WebGL и ThreeJS",
                LecturesCount = 15,
                ExercisesCount = 30,
                CreditsCount = 5
            },
        ];
        
        await dbContext.Courses.AddRangeAsync(courses);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Courses seeded successfully");
    }
}