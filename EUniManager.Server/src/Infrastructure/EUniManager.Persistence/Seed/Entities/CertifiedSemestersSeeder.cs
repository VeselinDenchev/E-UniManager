using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class CertifiedSemestersSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.CertifiedSemesters.AsNoTracking().AnyAsync()) return;

        var certifiedSemesters = new CertifiedSemester[5];
        for (int i = 0; i < certifiedSemesters.Length; i++)
        {
            certifiedSemesters[i] = new CertifiedSemester
            {
                Student = await dbContext.Students.FirstAsync(),
                Semester = (byte)(i + 1)
            };
        }

        await dbContext.CertifiedSemesters.AddRangeAsync(certifiedSemesters);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Certified semesters seeded successfully");
    }
}