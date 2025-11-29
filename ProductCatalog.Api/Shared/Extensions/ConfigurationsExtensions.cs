using ProductCatalog.Api.Configurations;

namespace ProductCatalog.Api.Shared.Extensions;

public static class ConfigurationsExtensions
{
    /// <summary>
    /// Registra e vincula as configurações necessárias da aplicação.
    /// </summary>
    public static IServiceCollection AddConfigurations(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        services.AddOptions<ConnectionStrings>()
                .Bind(configuration.GetSection(nameof(ConnectionStrings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        return services;
    }
}
