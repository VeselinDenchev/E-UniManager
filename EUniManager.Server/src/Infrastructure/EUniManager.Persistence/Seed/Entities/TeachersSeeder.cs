using EUniManager.Application.Extensions;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class TeachersSeeder
{
    private static UserManager<IdentityUser<Guid>> _userManager;
    
    public static async Task SeedAsync(IEUniManagerDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
    {
        if (await dbContext.Teachers.AnyAsync()) return;
        
        _userManager = userManager;

        Teacher[] teachers =
        [
            await GetNewTeacherAsync("доц. д-р", "Георги", "Петров", "Желязков", "g.zhelyazkov@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("д-р", "Петър", "Георгиев", "Желязков", "p.zhelyazkov@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("гл.ас. д-р", "Радостина", "Горанова", "Иванова", "r.ivanova@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("гл.ас. д-р", "Йордан", "Пламенов", "Иванов", "y.ivanov@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("ас.", "Мая", "Георгиева", "Петрова", "m.petrova@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("доц. д-р", "Димитър", "Петров", "Цонев", "d.tsonev@uni-vt.bg", "123456789"),
            await GetNewTeacherAsync("проф. дн", "Даниел", "Мирославов", "Николов", "d.nikolov@uni-vt.bg", "123456789"),
        ];
        
        await dbContext.Teachers.AddRangeAsync(teachers);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Teachers seeded successfully");
    }

    private static async Task<Teacher> GetNewTeacherAsync(
        string rank,
        string firstName,
        string middleName,
        string lastName,
        string email,
        string password)
    {
        Teacher teacher = new()
        {
            Rank = rank,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName
        };
        
        teacher.User = new IdentityUser<Guid>
        {
            Email = email,
            UserName = email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        IdentityResult identityResult = await _userManager.CreateAsync(teacher.User, password);
        identityResult.ThrowExceptionIfFailed();

        identityResult = await _userManager.AddToRoleAsync(teacher.User, nameof(UserRole.Teacher));
        identityResult.ThrowExceptionIfFailed();

        return teacher;
    }
}