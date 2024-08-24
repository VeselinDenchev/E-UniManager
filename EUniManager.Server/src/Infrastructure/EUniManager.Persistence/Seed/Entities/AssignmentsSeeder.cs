using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class AssignmentsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Assignments.AsNoTracking().AnyAsync()) return;

        Resource[] assignmentResources = await dbContext.Resources.Where(r => r.Type == ResourceType.Assignment)
                                                                  .Include(r => r.Activity).ThenInclude(a => a.Students)
                                                                  .Include(r => r.Activity).ThenInclude(a => a.Teacher)
                                                                  .ToArrayAsync();
        Student student = assignmentResources.First()
                                             .Activity.Students.First();
        
        var assignments = new Assignment[assignmentResources.Length];
        for (int i = 0; i < assignmentResources.Length; i++)
        {
            bool isText = i % 2 == 0;
            string assignmentTypeString = i % 2 == 0 ? "текст" : "файл";
            assignments[i] = new Assignment
            {
                Resource = assignmentResources[i],
                ResourceId = assignmentResources[i].Id,
                Title = $"Задача {assignmentTypeString}",
                StartDate = DateTime.Now.AddMinutes(1),
                DueDate = DateTime.Now.AddDays(30),
                Type = (AssignmentType)(i % 2),
                Students = [ student ],
                Description = $"Тази задача е от тип {assignmentTypeString}",
                Teacher = assignmentResources[i].Activity.Teacher
            };
        }

        await dbContext.Assignments.AddRangeAsync(assignments);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Assignments seeded successfully");
    }
}