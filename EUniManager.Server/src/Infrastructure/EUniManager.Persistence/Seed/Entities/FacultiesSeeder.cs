using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class FacultiesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Faculties.AsNoTracking().AnyAsync()) return;
        
        Faculty[] faculties =
        [
            new() { Name = "Филологически факултет" },
            new() { Name = "Исторически факултет" },
            new() { Name = "Педагогически факултет" },
            new() { Name = "Юридически факултет" },
            new() { Name = "Стопански факултет" },
            new() { Name = "Факултет по изобразително изкуство" },
            new() { Name = "Факултет \"Математика и информацтика\"" },
            new() { Name = "Православен богословки факултет" }
        ];

        await dbContext.Faculties.AddRangeAsync(faculties);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Faculties seeded successfully");
    }
}