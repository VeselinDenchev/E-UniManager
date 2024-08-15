using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class SubjectMarksSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.SubjectMarks.AsNoTracking().AnyAsync()) return;

        Subject[] subjects = await dbContext.Subjects.Include(s => s.Students).ToArrayAsync();
        Student student = subjects.First()
                                  .Students.First();
        var marks = new SubjectMark[subjects.Length];

        for (int i = 0; i < subjects.Length; i++)
        {
            marks[i] = new SubjectMark
            {
                Student = student,
                Subject = subjects[i],
                Mark = (Mark)Math.Max(3, Math.Min(6, (i + 2) % 7)) // Always between 3 and 6 inclusive
            };
        }

        await dbContext.SubjectMarks.AddRangeAsync(marks);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Subject marks seeded successfully");
    }
}