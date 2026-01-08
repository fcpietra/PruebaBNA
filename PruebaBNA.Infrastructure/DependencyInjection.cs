using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Infrastructure.Persistence;
using PruebaBNA.Infrastructure.Services;
using PruebaBNA.Infrastructure.Services.Bcra;

namespace PruebaBNA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. EF Core con SQLite
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // 2. Memory Cache (Nativo de .NET)
        services.AddMemoryCache();

        // 3. Nuestro Wrapper de Cache
        services.AddScoped<ICacheService, MemoryCacheService>();

        services.AddScoped<IApplicationDbContext>(provider =>
        provider.GetRequiredService<ApplicationDbContext>());

        services.AddHttpClient<IBcraService, BcraService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:BcraApiUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
}