using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Identity;

using static EUniManager.Persistence.Constants.Entities.AdminCredentialsConstant;

namespace EUniManager.Persistence.Seed.Identity;

public static class AdminSeeder
{
    public static async Task SeedAsync(UserManager<IdentityUser<Guid>> userManager)
    {
        // Create the admin user if it doesn't exist
        IdentityUser<Guid>? admin = await userManager.FindByEmailAsync(ADMIN_EMAIL);

        if (admin is not null) return;
        
        admin = new IdentityUser<Guid>
        {
            UserName = ADMIN_EMAIL,
            Email = ADMIN_EMAIL,
            EmailConfirmed = true
        };
        IdentityResult createUserResult = await userManager.CreateAsync(admin, ADMIN_PASSWORD);
        if (createUserResult.Succeeded)
        {
            // Assign the admin role to the user
            await userManager.AddToRoleAsync(admin, nameof(UserRole.Admin));
        }
        
        Console.WriteLine("Admin seeded successfully");
    }
}