using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EUniManager.Persistence.Migrations;

public static class DatabaseMigrator
{
    public static async Task MigrateUpAsync(IServiceProvider serviceProvider)
    {
        EUniManagerDbContext dbContext = serviceProvider.GetRequiredService<EUniManagerDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}