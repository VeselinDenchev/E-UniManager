﻿using EUniManager.Persistence;
using EUniManager.Persistence.Seed;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Api.Extensions;

public static class UpdateDatabaseExtensions
{
    public static async Task UpdateDatabaseAsync(this IApplicationBuilder app)
    {
        const byte MAX_RETRIES_COUNT = 3;
        
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<EUniManagerDbContext>();
        
        byte currentRetriesCount = 0;
        while (true)
        {
            try
            {
                await dbContext.Database.MigrateAsync();
                
                break;
            }
            catch (SqlException) when (++currentRetriesCount > MAX_RETRIES_COUNT)
            {
                throw;
            }
            catch (SqlException se)
            {
                await Task.Delay(10_000);
                Console.WriteLine(se.Message);
                Console.WriteLine($"Could not connect to SQL Server. Retrying {currentRetriesCount}/{MAX_RETRIES_COUNT}.");
            }
        }
        
        await DatabaseSeeder.SeedAsync(serviceScope.ServiceProvider);
    }
}