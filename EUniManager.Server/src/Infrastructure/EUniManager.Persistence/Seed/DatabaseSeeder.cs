using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EUniManager.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        await RolesSeeder.SeedAsync(serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>()!);
        await AdminSeeder.SeedAsync(serviceProvider.GetService<UserManager<IdentityUser<Guid>>>()!);
    }
}   