


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection service)
    {
        AddDb(ref service);
        AddRepositorys(ref service);
        service.AddHttpContextAccessor();
        AddAuthentification(ref service);
        AddAuthorization(ref service);
        return service;
    }

    private static void AddAuthentification(ref IServiceCollection service)
    {
        service.AddSingleton<IJwtProvider, JwtProvider>();
    }

    private static void AddDb(ref IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>();
        service.RegisterAllTypes<IDbInterceptor>(ServiceLifetime.Singleton);
    }

    private static void AddAuthorization(ref IServiceCollection service)
    {
        service.AddAuthorization();
        service.AddSingleton<IAuthorizationHandler, PermissionAutherizationHandler>();
        service.AddScoped<IPermissionService, PermissionService>();
        service.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddRepositorys(ref IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IPaymentRepository, PaymentRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IBrandRepository, BrandRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterAllTypes<T>(this IServiceCollection services,
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