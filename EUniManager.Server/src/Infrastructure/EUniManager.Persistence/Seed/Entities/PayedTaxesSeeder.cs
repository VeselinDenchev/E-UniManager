using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class PayedTaxesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.PayedTaxes.AsNoTracking().AnyAsync()) return;

        PayedTax samplePayedTax = new()
        {
            Student = await dbContext.Students.FirstAsync(),
            TaxNumber = 123_456_789,
            DocumentDate = DateOnly.FromDateTime(DateTime.Now),
            Semester = 1,
            PlanNumber = 123_456_789,
            Amount = 123,
            Currency = Currency.Bgn
        };

        var payedTaxes = new PayedTax[5];
        for (int i = 0; i < payedTaxes.Length; i++)
        {
            payedTaxes[i] = new()
            {
                Student = samplePayedTax.Student,
                TaxNumber = samplePayedTax.TaxNumber + i,
                DocumentNumber = SeedHelper.GetRandomString(30),
                DocumentDate = DateOnly.FromDateTime(DateTime.Now).AddDays(i - payedTaxes.Length),
                Semester = (byte)(1 + i),
                PlanNumber = 123_456_789 + i,
                Amount = (byte)(123 + i),
                Currency = Currency.Bgn
            };
        }

        await dbContext.PayedTaxes.AddRangeAsync(payedTaxes);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Payed taxes seeded successfully");
        
    }
}