using System.Reflection;

using EUniManager.Application.Models.Teachers.Interfaces;
using EUniManager.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace EUniManager.Application.Extensions;

public static class ApplicationLayerConfiguration
{
    public static IServiceCollection AddApplicationLayerConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ITeacherService, TeacherService>();

        return services;
    }
}