using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Identity;

public static class RolesSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;
        
        IdentityRole<Guid>[] roles =
        [
            new() { Name = nameof(UserRole.Admin), NormalizedName = nameof(UserRole.Admin).ToUpper() },
            new() { Name = nameof(UserRole.Student), NormalizedName = nameof(UserRole.Student).ToUpper() },
            new() { Name = nameof(UserRole.Teacher), NormalizedName = nameof(UserRole.Teacher).ToUpper() }
        ];

        foreach (IdentityRole<Guid> role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        
        Console.WriteLine("Roles seeded successfully");
    }
}