using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Persistence.Seed.Entities;
using EUniManager.Persistence.Seed.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EUniManager.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        UserManager<IdentityUser<Guid>> userManager = serviceProvider.GetService<UserManager<IdentityUser<Guid>>>()!;
        IEUniManagerDbContext dbContext = serviceProvider.GetService<IEUniManagerDbContext>()!;

        Console.WriteLine();       
        Console.WriteLine("Seeding data...");
        Console.WriteLine();
        
        await CloudinaryFilesSeeder.SeedAsync(dbContext, serviceProvider.GetService<ICloudinaryService>()!);
        await RolesSeeder.SeedAsync(serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>()!);
        await AdminSeeder.SeedAsync(userManager);
        await FacultiesSeeder.SeedAsync(dbContext);
        await SpecialtiesSeeder.SeedAsync(dbContext);
        await StudentsSeeder.SeedAsync(dbContext, userManager);
        await RequestApplicationsSeeder.SeedAsync(dbContext);
        await PayedTaxesSeeder.SeedAsync(dbContext);
        await CertifiedSemestersSeeder.SeedAsync(dbContext);
        await TeachersSeeder.SeedAsync(dbContext, userManager);
        await CoursesSeeder.SeedAsync(dbContext);
        await SubjectsSeeder.SeedAsync(dbContext);
        await SubjectMarksSeeder.SeedAsync(dbContext);
        await ExamsSeeder.SeedAsync(dbContext);
        await IndividualProtocolsSeeder.SeedAsync(dbContext);
        await ActivitiesSeeder.SeedAsync(dbContext);
        await CourseScheduleUnitsSeeder.SeedAsync(dbContext);
        await ResourcesSeeder.SeedAsync(dbContext);
        await AssignmentsSeeder.SeedAsync(dbContext);
        await AssignmentSolutionsSeeder.SeedAsync(dbContext);
        
        Console.WriteLine();
        Console.WriteLine("All required data seeded successfully");
        Console.WriteLine();
    }
}   