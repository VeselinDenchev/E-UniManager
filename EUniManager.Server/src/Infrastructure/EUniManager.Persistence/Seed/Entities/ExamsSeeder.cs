using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class ExamsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Exams.AsNoTracking().AnyAsync()) return;

        Subject[] subjects = await dbContext.Subjects.Include(s => s.Students).ToArrayAsync();
        var exams = new Exam[subjects.Length];
        for (int i = 0; i < exams.Length; i++)
        {
            exams[i] = new Exam
            {
                Subject = subjects[i],
                Type = (ExamType)(i % 5),
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(i),
                Time = new TimeOnly(8 + i, 0 + i),
                Place = SchedulePlace.Corps4,
                RoomNumber = (short)(101 + i),
                GroupNumber = 1
            };
        }

        await dbContext.Exams.AddRangeAsync(exams);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Exams seeded successfully");
    }
}