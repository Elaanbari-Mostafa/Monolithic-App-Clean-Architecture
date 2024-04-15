using Application.Abstractions;
using Domain.Repositories;
using Infrastructure.Authentification;
using Infrastructure.Data;
using Infrastructure.Data.Configurations;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>();
        service.RegisterAllTypes<IDbInterceptor>(ServiceLifetime.Singleton);

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IBrandRepository, BrandRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        service.AddSingleton<IJwtProvider, JwtProvider>();
        service.AddAuthorization();
        service.AddSingleton<IAuthorizationHandler,PermissionAutherizationHandler>();
        service.AddScoped<IPermissionService,PermissionService>();
        service.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return service;
    }

    public static void RegisterAllTypes<T>(this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var typesFromAssemblie = InfrastructureAssemblyReference.Assembly.
            DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T)));

        foreach (var type in typesFromAssemblie)
        {
            services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}