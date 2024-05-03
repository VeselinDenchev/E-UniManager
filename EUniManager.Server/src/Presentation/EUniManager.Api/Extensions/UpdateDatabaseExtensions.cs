using EUniManager.Persistence;
using EUniManager.Persistence.Seed;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Api.Extensions;

public static class UpdateDatabaseExtensions
{
    public static async Task UpdateDatabaseAsync(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<EUniManagerDbContext>();
        
        await dbContext.Database.MigrateAsync();

        await DatabaseSeeder.SeedAsync(serviceScope.ServiceProvider);
    }
}