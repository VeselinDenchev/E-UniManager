using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class IndividualProtocolsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.IndividualProtocols.AsNoTracking().AnyAsync()) return;

        Subject[] subjects = await dbContext.Subjects.Include(s => s.Students)
                                                     .ToArrayAsync();
        
        Student student = subjects.First()
                                  .Students.First();

        var individualProtocols = new IndividualProtocol[subjects.Length];
        for (int i = 0; i < individualProtocols.Length; i++)
        {
            individualProtocols[i] = new IndividualProtocol
            {
                Subject = subjects[i],
                Student = student,
                Status = (IndividualProtocolStatus)(i % 4)
            };
        }

        await dbContext.IndividualProtocols.AddRangeAsync(individualProtocols);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Individual protocols seeded successfully");
    }
}