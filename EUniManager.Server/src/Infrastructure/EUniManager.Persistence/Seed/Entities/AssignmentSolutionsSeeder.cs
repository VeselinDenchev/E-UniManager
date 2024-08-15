using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class AssignmentSolutionsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.AssignmentSolutions.AsNoTracking().AnyAsync()) return;

        Assignment[] assignments = await dbContext.Assignments.Include(a => a.Students)
                                                              .ToArrayAsync();
        Student student = assignments.First()
                                     .Students.First();
        CloudinaryFile wordDocumentFile = await dbContext.CloudinaryFiles.FirstAsync(f => f.Extension == ".docx"); 
        var solutions = new AssignmentSolution[assignments.Length];
        for (int i = 0; i < assignments.Length; i++)
        {
            solutions[i] = new AssignmentSolution
            {
                Assignment = assignments[i],
                Student = student,
                File = assignments[i].Type is AssignmentType.File ? wordDocumentFile : null,
                Text = assignments[i].Type is AssignmentType.Text ? "Това е решение" : null
            };
        }
        
        await dbContext.AssignmentSolutions.AddRangeAsync(solutions);
        await dbContext.SaveChangesAsync();
        
        DateTime now = DateTime.Now;
        for (int i = 0; i < solutions.Length; i++)
        {
            bool hasMark = i % 2 == 0;
            solutions[i].SeenOn = now.AddMilliseconds(1);
            solutions[i].SubmittedOn = now.AddMilliseconds(2);
            solutions[i].Mark = hasMark ? Mark.Excellent : null;
            solutions[i].MarkedOn = hasMark ? now.AddMilliseconds(3) : null;
            solutions[i].Comment = hasMark ? "Коментар" : null;
        }
        
        dbContext.AssignmentSolutions.UpdateRange(solutions);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Assignment solutions seeded successfully");
    }
}