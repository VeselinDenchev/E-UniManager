using Microsoft.Extensions.DependencyInjection;

using static EUniManager.Application.Constants.ServiceConstant;

namespace EUniManager.Application.Extensions;

public static class ApplicationLayerConfigurationExtensions
{
    public static IServiceCollection AddApplicationLayerConfiguration(this IServiceCollection services)
    {
        services.Scan(s => s.FromCallingAssembly()
                            .AddClasses(c => c.Where(type => type.Name.EndsWith(SERVICE_SUFFIX_STRING)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

        return services;
    }
}