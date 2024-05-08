using Carter;

using EUniManager.Api.Extensions;
using EUniManager.Application.Extensions;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

using static EUniManager.Api.Configurations.SwaggerGenConfiguration;
using static EUniManager.Api.Constants.IdentityConstant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ConfigureSwaggerGen);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<IdentityUser<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<EUniManagerDbContext>()
                .AddApiEndpoints();

builder.Services.AddDbContext<IEUniManagerDbContext, EUniManagerDbContext>();

builder.Services.AddApplicationLayerConfiguration();

builder.Services.AddAntiforgery();

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.UpdateDatabaseAsync();

app.UseAntiforgery();

app.MapIdentityApi<IdentityUser<Guid>>()
   .WithTags(IDENTITY_TAG_NAME);

app.MapCarter();

app.Run();