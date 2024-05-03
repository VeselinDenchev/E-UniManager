using EUniManager.Application.Models.Base.Interfaces;

using Microsoft.Extensions.DependencyInjection;


namespace EUniManager.Application.Extensions;

public static class ApplicationLayerConfiguration
{
    public static IServiceCollection AddApplicationLayerConfiguration(this IServiceCollection services)
    {
        services.Scan(s => s.FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(IBaseService<,,,>)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

        return services;
    }
}