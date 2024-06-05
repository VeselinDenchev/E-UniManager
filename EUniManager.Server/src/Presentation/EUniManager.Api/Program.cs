using Carter;

using EUniManager.Api.Extensions;
using EUniManager.Application.Extensions;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Enums;
using EUniManager.Persistence;

using Microsoft.AspNetCore.Identity;

using static EUniManager.Api.Configurations.SwaggerGenConfiguration;
using static EUniManager.Api.Constants.IdentityConstant;
using static EUniManager.Api.Constants.PoliciesConstant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ConfigureSwaggerGen);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ADMIN_POLICY_NAME, policy => policy.RequireRole(nameof(UserRole.Admin)));
    options.AddPolicy(STUDENT_POLICY_NAME, policy => policy.RequireRole(nameof(UserRole.Student)));
    options.AddPolicy(TEACHER_POLICY_NAME, policy => policy.RequireRole(nameof(UserRole.Teacher)));
    options.AddPolicy(ACADEMIC_POLICY_NAME,
         policy => policy.RequireAssertion(context => context.User.IsInRole(nameof(UserRole.Student)) || 
                                                                  context.User.IsInRole(nameof(UserRole.Teacher))));
});
builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<IdentityUser<Guid>>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
               .AddRoles<IdentityRole<Guid>>()
               .AddEntityFrameworkStores<EUniManagerDbContext>()
               .AddApiEndpoints();

builder.Services.AddDbContext<IEUniManagerDbContext, EUniManagerDbContext>();

builder.Services.AddApplicationLayerConfiguration();

builder.Services.AddHttpContextAccessor();

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

app.MapGroup(IDENTITY_GROUP_NAME)
   .MapIdentityApi<IdentityUser<Guid>>()
   .WithTags(IDENTITY_TAG_NAME);

app.MapCarter();

app.Run();