using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        TypeAdapterConfig.GlobalSettings.Scan(ApplicationAssemblyReference.Assembly); // Add Mapster
        service.AddMediatR(ctr => ctr.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));
        return service;
    }
}