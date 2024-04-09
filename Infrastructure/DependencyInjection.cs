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

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        service.AddApplicationDbContext(configuration);
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddSingleton<IJwtProvider, JwtProvider>();
        service.AddAuthorization();
        service.AddSingleton<IAuthorizationHandler,PermissionAutherizationHandler>();
        service.AddScoped<IPermissionService,PermissionService>();
        service.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return service;
    }
}