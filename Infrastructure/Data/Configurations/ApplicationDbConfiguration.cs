using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Configurations;

internal static class ApplicationDbConfiguration
{
    private const string _connectionStringName = "DefaultConnection";

    internal static IServiceCollection AddApplicationDbContext(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>((sp, options) =>
            options
                .UseSqlServer(configuration.GetConnectionString(_connectionStringName))
                .AddCustomDbInterceptors(sp)
        );

        return service;
    }
}
