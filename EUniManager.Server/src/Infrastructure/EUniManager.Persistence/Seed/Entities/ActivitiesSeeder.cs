using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class ActivitiesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Activities.AsNoTracking().AnyAsync()) return;

        Subject[] subjects = await dbContext.Subjects.Include(s => s.Students)
                                                     .Include(s => s.Lecturer)
                                                     .Include(s => s.Assistants)
                                                     .ToArrayAsync();

        Student student = subjects.First()
                                  .Students.First();
        
        List<Activity> activities = new();

        foreach (Subject subject in subjects)
        {
            Activity lecture = new()
            {
                Type = ActivityType.Lecture,
                Teacher = subject.Lecturer,
                Students = [ student ],
                Subject = subject,
                IsStopped = false
            };
            activities.Add(lecture);

            List<Activity> exercises = new();
            for (int i = 0; i < subject.Assistants.Count; i++)
            {
                Activity exercise = new()
                {
                    Type = ActivityType.Exercise,
                    Teacher = subject.Assistants[i],
                    Students = [ student ],
                    Subject = subject,
                    IsStopped = i % 2 == 0 // Alternate
                };
                exercises.Add(exercise);
            }
            activities.AddRange(exercises);
        }

        activities[activities.Count - 1].Students.Remove(student);
        
        await dbContext.Activities.AddRangeAsync(activities);
        await dbContext.SaveChangesAsync();

        Console.WriteLine("Activities seeded sucessfully");        
    }
}