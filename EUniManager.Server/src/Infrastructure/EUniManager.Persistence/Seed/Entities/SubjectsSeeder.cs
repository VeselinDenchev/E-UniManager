using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class SubjectsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Subjects.AsNoTracking().AnyAsync()) return;

        Course[] courses = await dbContext.Courses.ToArrayAsync();
        Specialty softwareEngineering = await dbContext.Specialties.Include(s => s.Students)
                                                                   .FirstAsync(s => s.Name == "Софтуерно инженерство");
        Student student = softwareEngineering.Students!.First();
        Teacher[] teachers = await dbContext.Teachers.ToArrayAsync();
        var subjects = new Subject[courses.Length];

        for (int i = 0; i < courses.Length; i++)
        {
            subjects[i] = new Subject
            {
                Semester = 1,
                Course = courses[i],
                Students = [ student ],
                Lecturer = teachers[i],
                Assistants = [ teachers[i], teachers[i + 1], teachers[i + 2] ],
                Specialty = softwareEngineering,
                Protocol = $"s{1_234 + i}",
                ControlType = (SubjectControlType)(i % 2), // Alternate
            };
        }

        await dbContext.Subjects.AddRangeAsync(subjects);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Subjects seeded successfully");
    }
}