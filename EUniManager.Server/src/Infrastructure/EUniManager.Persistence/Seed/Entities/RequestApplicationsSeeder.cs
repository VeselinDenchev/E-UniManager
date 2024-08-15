using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class RequestApplicationsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.RequestApplications.AsNoTracking().AnyAsync()) return;

        RequestApplication sampleRequestApplication = new()
        {
            Number = 123_456_789,
            Student = await dbContext.Students.FirstAsync(),
            Type = RequestApplicationType.Universal,
            RegistryDate = DateTime.Now.AddDays(-1),
            ResolutionDate = DateTime.Now,
            File = await dbContext.CloudinaryFiles.FirstAsync(f => f.Extension == ".docx")
        };
        
        RequestApplication[] requestApplications = new RequestApplication[5];

        for (int i = 0; i < requestApplications.Length; i++)
        {
            requestApplications[i] = new RequestApplication
            {
                Number = i + 123_456_789,
                Student = sampleRequestApplication.Student,
                Type = sampleRequestApplication.Type,
                RegistryDate = sampleRequestApplication.RegistryDate,
                ResolutionDate = sampleRequestApplication.ResolutionDate,
                File = sampleRequestApplication.File
            };
        }

        await dbContext.RequestApplications.AddRangeAsync(requestApplications);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Request/Applications seeded successfully");
    }
}