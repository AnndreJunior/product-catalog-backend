using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductCatalog.Api.Configurations;
using ProductCatalog.Api.Data;

namespace ProductCatalog.Api.Shared.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Configura tudo que é necessário para a conexão com o banco de dados.
    /// </summary>
    /// <remarks>
    /// Este método registra o DbContext, carrega a connection string das configurações
    /// e define o provedor PostgreSQL para acesso ao banco.
    /// </remarks>
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((sp, opts) =>
        {
            ConnectionStrings connectionStrings =
                sp.GetRequiredService<IOptions<ConnectionStrings>>().Value;

            opts.UseNpgsql(connectionStrings.Postgres);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Aplica automaticamente as migrações pendentes do banco de dados durante a inicialização da aplicação.
    /// </summary>
    /// <remarks>
    /// Usado apenas em ambiente de desenvolvimento.
    /// </remarks>
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        return app;
    }
}
