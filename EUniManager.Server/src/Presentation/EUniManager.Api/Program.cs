using Carter;

using EUniManager.Application.Extensions;
using EUniManager.Persistence;
using EUniManager.Persistence.Migrations;
using EUniManager.Persistence.Seed;

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EUniManagerDbContext>();
builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<EUniManagerDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddApplicationLayerConfiguration();

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Update database
using (IServiceScope scope = app.Services.CreateScope())
{
    await DatabaseMigrator.MigrateUpAsync(scope.ServiceProvider);
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.MapCarter();

app.Run();