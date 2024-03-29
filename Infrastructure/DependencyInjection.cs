using Application.Abstractions;
using Domain.Repositories;
using Infrastructure.Authentification;
using Infrastructure.Data;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        service.AddDbContext<ApplicationDbContext>((sp, builder) => builder.AddCustomInterceptors(sp));
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddSingleton<IJwtProvider, JwtProvider>();

        return service;
    }
}